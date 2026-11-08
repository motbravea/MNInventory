using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using ClosedXML.Excel;
using MNInventoryApp.Models;
using MNInventoryApp.Services;

namespace MNInventoryUI
{
    public partial class Form1 : Form
    {
        private readonly ExcelService excelService = new ExcelService();
        private readonly InventoryService inventoryService = new InventoryService();
        private readonly MySqlConnectionTestService mySqlTestService = new MySqlConnectionTestService();
        private readonly MySqlInventoryService mySqlService = new MySqlInventoryService();

        private List<Product> sourceProducts = new List<Product>();
        private List<Product> scannedProducts = new List<Product>();

        private Dictionary<string, Product> sourceDictionary = new Dictionary<string, Product>();
        private Dictionary<string, Product> scannedDictionary = new Dictionary<string, Product>();

        // Session-only undo stack
        private readonly Stack<ScanAction> scanHistory = new Stack<ScanAction>();

        private readonly ToolTip quickScanToolTip = new ToolTip();

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            quickScanToolTip.Dispose();
            base.OnFormClosed(e);
        }

        private class ScanAction
        {
            public int ProductId { get; set; }
            public string QR { get; set; }
            public int QuantityAdded { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private enum StatusType
        {
            Info,
            Success,
            Error
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool connected = mySqlTestService.CanConnect();

            if (!connected)
            {
                MessageBox.Show(
                    "Cannot connect to the inventory database. Configure MNINVENTORY_CONNECTION_STRING " +
                    "and apply the database schema, then restart MNInventory.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                ReloadAllData();

                ApplyModernTheme();
                quickScanToolTip.SetToolTip(
                    chkQuickScan,
                    "ON: Scan barcode, press Enter — adds 1 unit and returns here for the next item.\n" +
                    "OFF: Enter moves to Quantity; type amount then Enter (normal mode).");
                ApplyQuickScanUiState();
                RefreshGrid();
                UpdateLastScannedLabels(null);
                ClearInputs();
                ShowStatus(
                    sourceProducts.Count == 0
                        ? "No inventory loaded. Choose Import New Inventory to select an Excel workbook."
                        : "Ready to scan products.",
                    StatusType.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading inventory:\n" + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ReloadAllData()
        {
            sourceProducts = mySqlService.LoadSourceProducts();
            scannedProducts = mySqlService.LoadScannedProducts();

            sourceDictionary = inventoryService.BuildBarcodeDictionary(sourceProducts);
            scannedDictionary = inventoryService.BuildBarcodeDictionary(scannedProducts);
        }

        private Product FindSourceProductById(int id)
        {
            foreach (Product product in sourceProducts)
            {
                if (product.Id == id)
                    return product;
            }

            return null;
        }

        private Product FindScannedProductById(int id)
        {
            foreach (Product product in scannedProducts)
            {
                if (product.Id == id)
                    return product;
            }

            return null;
        }

        private void ApplyModernTheme()
        {
            BackColor = UiTheme.Background;

            StylePanel(panelHeader, UiTheme.Card);
            StylePanel(panelTop, UiTheme.Card);
            StylePanel(panelStatus, Color.FromArgb(248, 250, 252));
            StylePanel(panelLastScan, UiTheme.Card);
            StylePanel(panelGridSection, UiTheme.Card);

            StyleMetricCard(panelProductsCard, UiTheme.Card);
            StyleMetricCard(panelMissingCard, Color.FromArgb(254, 242, 242));
            StyleMetricCard(panelExtraCard, Color.FromArgb(240, 253, 244));
            StyleMetricCard(panelHistoryCard, UiTheme.Card);

            StylePrimaryButton(btnAdd);

            StyleSecondaryAccentButton(btnDiscrepancies);
            StyleSecondaryAccentButton(btnHistory);
            StyleSecondaryAccentButton(btnUnscanned);
            StyleSecondaryAccentButton(btnBarcodeLabels);
            StyleSecondaryAccentButton(btnAddMissingProduct);
            StyleSecondaryAccentButton(btnImportScannedExcel);
            btnAddMissingProduct.BringToFront();
            btnImportScannedExcel.BringToFront();
            btnBarcodeLabels.BringToFront();
            StyleSecondaryAccentButton(btnExport);
            StyleDangerButton(btnStartNewCount);

            StyleSecondaryButton(btnClear);
            StyleSecondaryButton(btnUndo);

            StyleTextBox(txtQR);
            StyleTextBox(txtQuantity);

            lblTitle.ForeColor = UiTheme.TextPrimary;
            lblSubtitle.ForeColor = UiTheme.TextSecondary;
            lblGridTitle.ForeColor = UiTheme.TextPrimary;
            lblLastScanCaption.ForeColor = UiTheme.TextSecondary;

            chkQuickScan.BackColor = Color.White;
            chkQuickScan.FlatStyle = FlatStyle.Standard;

            StyleGrid();
        }

        /// <summary>
        /// Updates quantity field and labels for Quick Scan vs manual entry (retail gun workflow).
        /// </summary>
        private void ApplyQuickScanUiState()
        {
            bool quick = chkQuickScan.Checked;

            txtQuantity.Enabled = !quick;
            txtQuantity.ReadOnly = quick;

            if (quick)
            {
                txtQuantity.Text = "1";
                txtQuantity.BackColor = Color.FromArgb(248, 250, 252);
                lblQuantity.ForeColor = Color.FromArgb(148, 163, 184);
            }
            else
            {
                lblQuantity.ForeColor = Color.FromArgb(51, 65, 85);
                StyleTextBox(txtQuantity);
            }
        }

        private void chkQuickScan_CheckedChanged(object sender, EventArgs e)
        {
            ApplyQuickScanUiState();

            if (chkQuickScan.Checked)
            {
                ShowStatus("Quick Scan ON — Enter after each barcode adds 1 unit. Barcode field stays active.", StatusType.Info);
            }
            else
            {
                ShowStatus("Quick Scan OFF — Enter moves to quantity; type amount then Enter to add.", StatusType.Info);
            }

            if (IsHandleCreated && txtQR.CanFocus)
                txtQR.Focus();
        }

        private void StylePanel(Panel panel, Color backColor)
        {
            panel.BackColor = backColor;
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StyleMetricCard(Panel panel, Color backColor)
        {
            panel.BackColor = backColor;
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StylePrimaryButton(Button button)
        {
            UiTheme.StylePrimaryButton(button);
        }

        private void StyleSecondaryAccentButton(Button button)
        {
            UiTheme.StyleTintButton(button);
        }

        private void StyleSecondaryButton(Button button)
        {
            UiTheme.StyleSecondaryButton(button);
        }

        private void StyleDangerButton(Button button)
        {
            UiTheme.StyleDangerButton(button);
        }

        private void StyleTextBox(TextBox textBox)
        {
            UiTheme.StyleTextInput(textBox);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOrUpdateProduct();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            UpdateLastScannedLabels(null);
            ShowStatus("Fields cleared.", StatusType.Info);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            UndoLastScan();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportScannedProductsToExcel();
        }

        private void btnStartNewCount_Click(object sender, EventArgs e)
        {
            StartNewCount();
        }

        private void btnDiscrepancies_Click(object sender, EventArgs e)
        {
            try
            {
                scannedProducts = mySqlService.LoadScannedProducts();

                DiscrepancyForm discrepancyForm = new DiscrepancyForm(scannedProducts);
                discrepancyForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowStatus("Error opening discrepancies: " + ex.Message, StatusType.Error);
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            try
            {
                using var historyForm = new HistoryForm(mySqlService);
                historyForm.ShowDialog(this);
                ReloadAllData();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                ShowStatus("Error opening history: " + ex.Message, StatusType.Error);
            }
        }

        private void btnBarcodeLabels_Click(object sender, EventArgs e)
        {
            try
            {
                using var form = new BarcodeLabelsForm(mySqlService);
                form.ShowDialog(this);
            }
            catch (Exception ex)
            {
                ShowStatus("Error opening barcode labels: " + ex.Message, StatusType.Error);
            }
        }

        private void btnAddMissingProduct_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new AddProductForm(mySqlService))
                {
                    if (form.ShowDialog(this) != DialogResult.OK)
                        return;
                }

                ReloadAllData();
                RefreshGrid();
                ClearInputs();
                ShowStatus("Product added to master inventory. You can scan it now.", StatusType.Success);
            }
            catch (Exception ex)
            {
                ShowStatus("Error adding product: " + ex.Message, StatusType.Error);
            }
        }

        private void btnImportScannedExcel_Click(object sender, EventArgs e)
        {
            ImportScannedProductsFromExcel();
        }

        private void btnUnscanned_Click(object sender, EventArgs e)
        {
            try
            {
                sourceProducts = mySqlService.LoadSourceProducts();
                scannedProducts = mySqlService.LoadScannedProducts();

                List<UnscannedProductReportItem> reportItems =
                    inventoryService.GetUnscannedProductsReport(sourceProducts, scannedProducts);

                UnscannedProductsForm reportForm = new UnscannedProductsForm(reportItems);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowStatus("Error opening unscanned report: " + ex.Message, StatusType.Error);
            }
        }

        private void txtQR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            if (chkQuickScan.Checked)
            {
                txtQuantity.Text = "1";
                AddOrUpdateProduct();
                return;
            }

            txtQuantity.Focus();
            txtQuantity.SelectAll();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AddOrUpdateProduct();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                e.SuppressKeyPress = true;
                UndoLastScan();
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
                ExportScannedProductsToExcel();
            }
        }

        private void AddOrUpdateProduct()
        {
            string inputQR = txtQR.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputQR))
            {
                ShowStatus("Please enter a QR/barcode.", StatusType.Error);
                txtQR.Focus();
                return;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity) || quantity <= 0)
            {
                ShowStatus("Please enter a valid positive quantity.", StatusType.Error);
                if (chkQuickScan.Checked)
                {
                    txtQuantity.Text = "1";
                    txtQR.Focus();
                    txtQR.SelectAll();
                }
                else
                {
                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                }

                return;
            }

            try
            {
                // Always find product from source inventory, not from stale scanned list
                Product sourceProduct = inventoryService.FindProductByQR(sourceDictionary, inputQR);

                if (sourceProduct == null)
                {
                    ShowStatus("Product not found in source inventory.", StatusType.Error);
                    txtQR.Focus();
                    txtQR.SelectAll();
                    return;
                }

                mySqlService.AddOrUpdateScannedProduct(sourceProduct, quantity, inputQR);

                scanHistory.Push(new ScanAction
                {
                    ProductId = sourceProduct.Id,
                    QR = sourceProduct.QR,
                    QuantityAdded = quantity,
                    Timestamp = DateTime.Now
                });

                // Reload fresh DB state after write
                scannedProducts = mySqlService.LoadScannedProducts();
                scannedDictionary = inventoryService.BuildBarcodeDictionary(scannedProducts);

                Product updatedProduct = FindScannedProductById(sourceProduct.Id);

                RefreshGrid();
                UpdateLastScannedLabels(updatedProduct);

                try
                {
                    HighlightRow(updatedProduct);
                }
                catch
                {
                    // Ignore UI highlight issues
                }

                ClearInputs();
                SystemSounds.Beep.Play();
                ShowStatus($"Added {quantity} unit(s) for \"{sourceProduct.Name}\".", StatusType.Success);

                if (chkQuickScan.Checked)
                {
                    BeginInvoke(new Action(() =>
                    {
                        txtQR.Focus();
                        txtQR.SelectAll();
                    }));
                }
            }
            catch (Exception ex)
            {
                ShowStatus("Error while updating product: " + ex.Message, StatusType.Error);
            }
        }

        private void UndoLastScan()
        {
            if (scanHistory.Count == 0)
            {
                ShowStatus("Nothing to undo.", StatusType.Info);
                return;
            }

            try
            {
                ScanAction lastAction = scanHistory.Pop();

                Product sourceProduct = FindSourceProductById(lastAction.ProductId);

                if (sourceProduct == null)
                {
                    // Fallback if IDs changed or source list is stale
                    sourceProducts = mySqlService.LoadSourceProducts();
                    sourceDictionary = inventoryService.BuildBarcodeDictionary(sourceProducts);

                    sourceProduct = FindSourceProductById(lastAction.ProductId);

                    if (sourceProduct == null)
                    {
                        sourceProduct = inventoryService.FindProductByQR(sourceDictionary, lastAction.QR);
                    }
                }

                if (sourceProduct == null)
                {
                    ShowStatus("Unable to undo: product not found.", StatusType.Error);
                    return;
                }

                mySqlService.UndoQuantity(sourceProduct, lastAction.QuantityAdded);

                scannedProducts = mySqlService.LoadScannedProducts();
                scannedDictionary = inventoryService.BuildBarcodeDictionary(scannedProducts);

                RefreshGrid();
                UpdateLastScannedLabels(null);
                ClearInputs();

                ShowStatus($"Last scan undone ({lastAction.QuantityAdded} unit(s)).", StatusType.Info);
            }
            catch (Exception ex)
            {
                ShowStatus("Error while undoing scan: " + ex.Message, StatusType.Error);
            }
        }

        private void ExportScannedProductsToExcel()
        {
            try
            {
                List<Product> freshData = mySqlService.LoadScannedProducts();

                if (freshData == null || freshData.Count == 0)
                {
                    ShowStatus("No scanned products to export.", StatusType.Info);
                    return;
                }

                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"MNInventory_Scanned_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string fullPath = Path.Combine(desktop, fileName);

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Scanned Products");

                    // Headers
                    worksheet.Cell(1, 1).Value = "Name";
                    worksheet.Cell(1, 2).Value = "SKU";
                    worksheet.Cell(1, 3).Value = "QR";
                    worksheet.Cell(1, 4).Value = "System Inventory";
                    worksheet.Cell(1, 5).Value = "Counted";
                    worksheet.Cell(1, 6).Value = "Adjustment";

                    // Header style
                    var headerRange = worksheet.Range(1, 1, 1, 6);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                    headerRange.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    headerRange.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;

                    int row = 2;

                    foreach (Product product in freshData)
                    {
                        worksheet.Cell(row, 1).Value = product.Name;
                        worksheet.Cell(row, 2).Value = product.SKU;
                        worksheet.Cell(row, 3).Value = product.QR;
                        worksheet.Cell(row, 4).Value = product.SystemInventory;
                        worksheet.Cell(row, 5).Value = product.Counted;
                        worksheet.Cell(row, 6).Value = product.Adjustment;

                        var dataRange = worksheet.Range(row, 1, row, 6);
                        dataRange.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                        dataRange.Style.Border.InsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;

                        worksheet.Cell(row, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(row, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(row, 6).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                        if (product.Adjustment < 0)
                        {
                            worksheet.Cell(row, 6).Style.Font.FontColor = ClosedXML.Excel.XLColor.Red;
                        }
                        else if (product.Adjustment > 0)
                        {
                            worksheet.Cell(row, 6).Style.Font.FontColor = ClosedXML.Excel.XLColor.Green;
                        }

                        row++;
                    }

                    worksheet.Range(1, 1, row - 1, 6).SetAutoFilter();
                    worksheet.SheetView.FreezeRows(1);
                    worksheet.Columns().AdjustToContents();

                    workbook.SaveAs(fullPath);
                }

                ShowStatus($"Exported successfully: {fileName}", StatusType.Success);

                // Automatically open the new exported file
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowStatus("Export failed: " + ex.Message, StatusType.Error);
            }
        }

        private void ImportScannedProductsFromExcel()
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "This will replace current scanned counts with data from the selected Excel file and reset scan history.\n\n" +
                    "Use this to restore a previously exported scanned file.\n\n" +
                    "Do you want to continue?",
                    "Import Scanned Excel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select scanned products export file";
                    openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    List<Product> importedScanned = excelService.LoadScannedProductsFromExcel(openFileDialog.FileName);
                    if (importedScanned == null || importedScanned.Count == 0)
                    {
                        ShowStatus("No valid scanned products found in the selected file.", StatusType.Error);
                        return;
                    }

                    mySqlService.ReplaceScannedProductsFromExcel(importedScanned);

                    scanHistory.Clear();
                    ReloadAllData();
                    RefreshGrid();
                    UpdateLastScannedLabels(null);
                    ClearInputs();
                    ShowStatus("Scanned products imported successfully from Excel.", StatusType.Success);
                }
            }
            catch (Exception ex)
            {
                ShowStatus("Error while importing scanned products: " + ex.Message, StatusType.Error);
            }
        }

        private void RefreshGrid()
        {
            dgvProducts.DataSource = null;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.DataSource = scannedProducts;

            InventorySummary summary = inventoryService.GetSummary(scannedProducts);

            lblProductsValue.Text = summary.TotalProducts.ToString();
            lblMissingValue.Text = summary.TotalMissing.ToString();
            lblExtraValue.Text = "+" + summary.TotalExtra.ToString();

            lblProductsValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblMissingValue.ForeColor = Color.FromArgb(220, 38, 38);
            lblExtraValue.ForeColor = Color.FromArgb(22, 163, 74);
            lblHistoryValue.ForeColor = Color.FromArgb(37, 99, 235);

            btnUndo.Enabled = scanHistory.Count > 0;

            try
            {
                lblHistoryValue.Text = mySqlService.LoadScanHistory(500).Count.ToString();
            }
            catch
            {
                lblHistoryValue.Text = "0";
            }
        }

        private void StyleGrid()
        {
            UiTheme.StyleGrid(dgvProducts);
            dgvProducts.RowTemplate.Height = 42;
        }

        private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProducts.Columns[e.ColumnIndex].Name == "colAdjustment" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int adjustment))
                {
                    e.CellStyle.Font = new Font("Segoe UI Semibold", 10.5F);

                    if (adjustment < 0)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    }
                    else if (adjustment > 0)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                        e.Value = "+" + adjustment;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                    }
                }
            }
        }

        private void UpdateLastScannedLabels(Product product)
        {
            if (product == null)
            {
                lblLastProduct.Text = "Last scanned product";
                lblLastDetails.Text = "SKU: -   |   System: -   |   Counted: -   |   Adjustment: -";
                lblLastTimestamp.Text = "No recent scan";
                lblLastDetails.ForeColor = Color.FromArgb(100, 116, 139);
                return;
            }

            lblLastProduct.Text = product.Name;
            lblLastDetails.Text =
                $"SKU: {product.SKU}   |   System: {product.SystemInventory}   |   Counted: {product.Counted}   |   Adjustment: {(product.Adjustment > 0 ? "+" : "")}{product.Adjustment}";
            lblLastTimestamp.Text = "Updated at " + DateTime.Now.ToString("HH:mm:ss");

            if (product.Adjustment < 0)
                lblLastDetails.ForeColor = Color.FromArgb(220, 38, 38);
            else if (product.Adjustment > 0)
                lblLastDetails.ForeColor = Color.FromArgb(22, 163, 74);
            else
                lblLastDetails.ForeColor = Color.FromArgb(71, 85, 105);
        }

        private void HighlightRow(Product product)
        {
            if (product == null || dgvProducts.Rows.Count == 0)
                return;

            dgvProducts.ClearSelection();

            int rowIndexToSelect = -1;

            for (int i = 0; i < dgvProducts.Rows.Count; i++)
            {
                if (dgvProducts.Rows[i].DataBoundItem is Product p && p.Id == product.Id)
                {
                    rowIndexToSelect = i;
                    break;
                }
            }

            if (rowIndexToSelect < 0 || rowIndexToSelect >= dgvProducts.Rows.Count)
                return;

            DataGridViewRow targetRow = dgvProducts.Rows[rowIndexToSelect];
            targetRow.Selected = true;

            try
            {
                if (targetRow.Cells.Count > 0 && targetRow.Cells[0].Visible)
                {
                    dgvProducts.CurrentCell = targetRow.Cells[0];
                }
            }
            catch
            {
                // Ignore current cell errors
            }

            try
            {
                if (rowIndexToSelect >= 0 && rowIndexToSelect < dgvProducts.RowCount)
                {
                    dgvProducts.FirstDisplayedScrollingRowIndex = rowIndexToSelect;
                }
            }
            catch
            {
                // Ignore scroll timing/index errors
            }
        }

        private void ShowStatus(string message, StatusType statusType)
        {
            lblStatus.Text = message;

            switch (statusType)
            {
                case StatusType.Success:
                    panelStatus.BackColor = Color.FromArgb(240, 253, 244);
                    lblStatus.ForeColor = UiTheme.Success;
                    break;

                case StatusType.Error:
                    panelStatus.BackColor = Color.FromArgb(254, 242, 242);
                    lblStatus.ForeColor = UiTheme.Error;
                    break;

                default:
                    panelStatus.BackColor = Color.FromArgb(248, 250, 252);
                    lblStatus.ForeColor = UiTheme.TextSecondary;
                    break;
            }
        }

        private void ClearInputs()
        {
            txtQR.Clear();
            txtQuantity.Text = "1";
            txtQR.Focus();
            txtQR.SelectAll();
            if (chkQuickScan.Checked)
                ApplyQuickScanUiState();
        }

        private void ImportNewInventoryPreserveCounts()
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Importing a new inventory file will update the source inventory in MySQL while keeping your counted quantities. Do you want to continue?",
                    "Import New Inventory",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select the new MNInventory inventory file";
                    openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    List<Product> newProducts = excelService.LoadProductsFromExcel(openFileDialog.FileName);

                    if (newProducts == null || newProducts.Count == 0)
                    {
                        ShowStatus("The selected file does not contain valid products.", StatusType.Error);
                        return;
                    }

                    mySqlService.SyncSourceProductsPreserveCounts(newProducts);

                    ReloadAllData();
                    RefreshGrid();
                    UpdateLastScannedLabels(null);
                    ClearInputs();

                    ShowStatus("New inventory imported successfully. Counted quantities were preserved.", StatusType.Success);
                }
            }
            catch (Exception ex)
            {
                ShowStatus("Error while importing new inventory: " + ex.Message, StatusType.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportNewInventoryPreserveCounts();
        }

        private void StartNewCount()
        {
            DialogResult result = MessageBox.Show(
                "This will permanently clear all counted data and scan history for the current session across all connected computers.\n\n" +
                "Source inventory will NOT be changed.\n\n" +
                "Do you want to continue?",
                "Start New Count",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                UseWaitCursor = true;
                mySqlService.ClearCurrentCountSession();

                scanHistory.Clear();
                ReloadAllData();
                RefreshGrid();
                UpdateLastScannedLabels(null);
                ClearInputs();
                ShowStatus("New count started. Previous scanned data and history were cleared.", StatusType.Success);
            }
            catch (Exception ex)
            {
                ShowStatus("Failed to start a new count: " + ex.Message, StatusType.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
