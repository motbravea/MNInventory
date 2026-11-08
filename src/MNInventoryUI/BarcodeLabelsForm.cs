using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MNInventoryApp.Models;
using MNInventoryApp.Services;

namespace MNInventoryUI
{
    public partial class BarcodeLabelsForm : Form
    {
        private sealed class MachineCountOption
        {
            public MachineCountOption(string display, string? machineName)
            {
                Display = display;
                MachineName = machineName;
            }

            /// <summary>Null = combined totals from <c>scanned_products</c>; otherwise net SCAN−UNDO for that PC.</summary>
            public string? MachineName { get; }

            private string Display { get; }

            public override string ToString() => Display;
        }

        private readonly MySqlInventoryService _mySqlService;
        private List<BarcodeLabelGridRow> _rows = new List<BarcodeLabelGridRow>();
        private bool _suppressCountSourceChanged;

        public BarcodeLabelsForm(MySqlInventoryService mySqlService)
        {
            _mySqlService = mySqlService ?? throw new ArgumentNullException(nameof(mySqlService));
            InitializeComponent();
        }

        private void BarcodeLabelsForm_Load(object sender, EventArgs e)
        {
            cmbPageSize.SelectedIndex = 0;
            ApplyTheme();
            PopulateCountSourceCombo();
            ReloadFromDatabase();
        }

        private void PopulateCountSourceCombo()
        {
            _suppressCountSourceChanged = true;
            try
            {
                cmbCountSource.Items.Clear();
                cmbCountSource.Items.Add(new MachineCountOption(
                    "All computers — full counted totals (shared inventory)",
                    null));

                string thisPc = Environment.MachineName;
                cmbCountSource.Items.Add(new MachineCountOption(
                    $"This PC ({thisPc}) — net scanned here only (SCAN − UNDO)",
                    thisPc));

                foreach (string name in _mySqlService.GetDistinctScanHistoryMachineNames())
                {
                    if (string.Equals(name, thisPc, StringComparison.OrdinalIgnoreCase))
                        continue;
                    cmbCountSource.Items.Add(new MachineCountOption($"{name} — net scanned on that PC", name));
                }

                cmbCountSource.SelectedIndex = 0;
            }
            finally
            {
                _suppressCountSourceChanged = false;
            }
        }

        private void cmbCountSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressCountSourceChanged)
                return;

            ReloadFromDatabase();
        }

        private void ApplyTheme()
        {
            BackColor = UiTheme.Background;
            UiTheme.StyleCard(panelTop, UiTheme.Card);
            UiTheme.StyleCard(panelGrid, UiTheme.Card);
            UiTheme.StyleCard(panelPreview, UiTheme.Card);
            UiTheme.StyleCard(panelBottom, UiTheme.Card);

            lblTitle.ForeColor = UiTheme.TextPrimary;
            lblSubtitle.ForeColor = UiTheme.TextSecondary;
            lblSearch.ForeColor = UiTheme.TextPrimary;
            lblPreviewCaption.ForeColor = UiTheme.TextSecondary;
            lblSelectionHint.ForeColor = UiTheme.TextSecondary;

            UiTheme.StyleTextInput(txtSearch);
            UiTheme.StyleDropdown(cmbPageSize);
            UiTheme.StyleDropdown(cmbCountSource);

            UiTheme.StylePrimaryButton(btnExportPdf);
            UiTheme.StyleTintButton(btnExportPng);
            UiTheme.StyleSecondaryButton(btnSelectAll);
            UiTheme.StyleSecondaryButton(btnSelectNone);
            UiTheme.StyleSecondaryButton(btnRefresh);
            UiTheme.StyleSecondaryButton(btnClose);

            StyleGrid();
        }

        private void StyleGrid()
        {
            UiTheme.StyleGrid(dgvProducts);
            dgvProducts.MultiSelect = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.ReadOnly = false;
            colSelect.ReadOnly = false;
            colName.ReadOnly = true;
            colSku.ReadOnly = true;
            colQty.ReadOnly = true;
            colBarcode.ReadOnly = true;
        }

        private void ReloadFromDatabase()
        {
            try
            {
                string? machineFilter = null;
                if (cmbCountSource.SelectedItem is MachineCountOption opt)
                    machineFilter = opt.MachineName;

                List<Product> scanned = string.IsNullOrEmpty(machineFilter)
                    ? _mySqlService.LoadScannedProducts()
                    : _mySqlService.LoadScannedProductsNetByMachine(machineFilter);

                _rows = scanned.Select(p => new BarcodeLabelGridRow(p)).ToList();
                ApplyFilter();
                string scope = string.IsNullOrEmpty(machineFilter)
                    ? "all PCs (combined counts)"
                    : $"PC “{machineFilter}” (net from history)";
                lblStatus.Text = $"{_rows.Count} product(s) — {scope}.";
                UpdatePreview();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Failed to load: " + ex.Message;
                MessageBox.Show(this, ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            string q = txtSearch.Text.Trim().ToLowerInvariant();
            IEnumerable<BarcodeLabelGridRow> query = _rows;

            if (!string.IsNullOrEmpty(q))
            {
                query = _rows.Where(r =>
                    r.Name.ToLowerInvariant().Contains(q) ||
                    r.Sku.ToLowerInvariant().Contains(q) ||
                    r.BarcodeRaw.ToLowerInvariant().Contains(q));
            }

            dgvProducts.DataSource = null;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.DataSource = query.ToList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (BarcodeLabelGridRow row in _rows)
                row.Selected = true;
            ApplyFilter();
            UpdatePreview();
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (BarcodeLabelGridRow row in _rows)
                row.Selected = false;
            ApplyFilter();
            UpdatePreview();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadFromDatabase();
        }

        private void dgvProducts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProducts.IsCurrentCellDirty)
                dgvProducts.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colSelect.Index)
                return;

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem is BarcodeLabelGridRow vm)
            {
                if (dgvProducts.Rows[e.RowIndex].Cells[colSelect.Index].Value is bool b)
                    vm.Selected = b;
            }

            UpdatePreview();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private List<Product> GetSelectedProducts()
        {
            return _rows.Where(r => r.Selected).Select(r => r.Product).ToList();
        }

        private void UpdatePreview()
        {
            DisposePreviewImage();

            Product? previewProduct = _rows.FirstOrDefault(r => r.Selected)?.Product;
            if (previewProduct == null && dgvProducts.CurrentRow?.DataBoundItem is BarcodeLabelGridRow curRow)
                previewProduct = curRow.Product;

            if (previewProduct == null)
            {
                picturePreview.Image = null;
                lblPreviewCaption.Text = "Preview — select a row or check “Print”";
                return;
            }

            try
            {
                Bitmap bmp = BarcodeLabelExportService.RenderLabelBitmap(previewProduct, 320, 200);
                picturePreview.Image = bmp;
                bool isChecked = _rows.FirstOrDefault(r => r.Product.Id == previewProduct.Id)?.Selected == true;
                lblPreviewCaption.Text = isChecked
                    ? "Preview (included in export)"
                    : "Preview (check “Print” to include)";
            }
            catch (Exception ex)
            {
                lblPreviewCaption.Text = "Preview error: " + ex.Message;
            }
        }

        private void DisposePreviewImage()
        {
            Image? old = picturePreview.Image;
            picturePreview.Image = null;
            old?.Dispose();
        }

        private BarcodeLabelExportService.LayoutOptions BuildLayoutOptions()
        {
            return new BarcodeLabelExportService.LayoutOptions
            {
                PageKind = cmbPageSize.SelectedIndex <= 0
                    ? BarcodeLabelExportService.PageKind.A4
                    : BarcodeLabelExportService.PageKind.Letter
            };
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            List<Product> selected = GetSelectedProducts();
            if (selected.Count == 0)
            {
                MessageBox.Show(this, "Select at least one product using the checkboxes.", "Nothing selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new SaveFileDialog
            {
                Title = "Save barcode label sheet (PDF)",
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"MNInventory_BarcodeLabels_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                DefaultExt = "pdf",
                AddExtension = true
            };

            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                UseWaitCursor = true;
                BarcodeLabelExportService.ExportToPdf(selected, dlg.FileName, BuildLayoutOptions());
                lblStatus.Text = $"Exported PDF ({selected.Count} label(s)): {Path.GetFileName(dlg.FileName)}";

                if (MessageBox.Show(this, "Open the PDF now?", "Export complete", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = dlg.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Export failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void btnExportPng_Click(object sender, EventArgs e)
        {
            List<Product> selected = GetSelectedProducts();
            if (selected.Count == 0)
            {
                MessageBox.Show(this, "Select at least one product using the checkboxes.", "Nothing selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new SaveFileDialog
            {
                Title = "Choose base file name for PNG sheet(s)",
                Filter = "PNG image (*.png)|*.png",
                FileName = $"MNInventory_BarcodeLabels_{DateTime.Now:yyyyMMdd_HHmmss}.png",
                DefaultExt = "png",
                AddExtension = true
            };

            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                UseWaitCursor = true;
                string dir = Path.GetDirectoryName(dlg.FileName) ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string prefix = Path.GetFileNameWithoutExtension(dlg.FileName);

                IReadOnlyList<string> files = BarcodeLabelExportService.ExportToPngPages(
                    selected,
                    dir,
                    prefix,
                    BuildLayoutOptions());

                lblStatus.Text = $"Exported {files.Count} PNG page(s).";

                if (files.Count > 0 &&
                    MessageBox.Show(this, "Open the first PNG now?", "Export complete", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = files[0],
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Export failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposePreviewImage();
            base.OnFormClosed(e);
        }
    }
}
