using MNInventoryApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace MNInventoryUI
{
    public partial class UnscannedProductsForm : Form
    {
        private readonly List<UnscannedProductReportItem> allItems;

        public UnscannedProductsForm(List<UnscannedProductReportItem> items)
        {
            InitializeComponent();
            allItems = items ?? new List<UnscannedProductReportItem>();
        }

        private void UnscannedProductsForm_Load(object sender, EventArgs e)
        {
            ApplyModernTheme();
            LoadFilteredData();
        }

        private void ApplyModernTheme()
        {
            BackColor = UiTheme.Background;

            UiTheme.StyleCard(panelToolbar, UiTheme.Card);
            UiTheme.StyleCard(panelGrid, UiTheme.Card);
            UiTheme.StyleCard(panelBottom, UiTheme.Card);
            panelCountCard.BackColor = Color.FromArgb(254, 242, 242);

            lblTitle.ForeColor = UiTheme.TextPrimary;
            lblSubtitle.ForeColor = UiTheme.TextSecondary;

            UiTheme.StyleTextInput(txtSearch);
            txtSearch.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            UiTheme.StylePrimaryButton(btnExport);
            UiTheme.StyleSecondaryButton(btnClose);
            StyleGrid();
        }

        private void StylePrimaryButton(Button button)
        {
            UiTheme.StylePrimaryButton(button);
        }

        private void StyleGrid()
        {
            UiTheme.StyleGrid(dgvUnscanned);
        }

        private void LoadFilteredData()
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            List<UnscannedProductReportItem> filtered = allItems
                .Where(x =>
                    string.IsNullOrWhiteSpace(searchText) ||
                    (x.Name != null && x.Name.ToLower().Contains(searchText)) ||
                    (x.SKU != null && x.SKU.ToLower().Contains(searchText)) ||
                    (x.QR != null && x.QR.ToLower().Contains(searchText)) ||
                    (x.Status != null && x.Status.ToLower().Contains(searchText)))
                .OrderBy(x => x.Name)
                .ToList();

            dgvUnscanned.DataSource = null;
            dgvUnscanned.DataSource = filtered;

            lblCountValue.Text = filtered.Count.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadFilteredData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExportToExcel()
        {
            try
            {
                if (dgvUnscanned.DataSource is not List<UnscannedProductReportItem> currentItems || currentItems.Count == 0)
                {
                    MessageBox.Show("No unscanned products to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"MNInventory_Unscanned_Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string fullPath = Path.Combine(desktop, fileName);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Unscanned");
                    worksheet.Cell(1, 1).Value = "Name";
                    worksheet.Cell(1, 2).Value = "SKU";
                    worksheet.Cell(1, 3).Value = "QR";
                    worksheet.Cell(1, 4).Value = "System Inventory";
                    worksheet.Cell(1, 5).Value = "Counted";
                    worksheet.Cell(1, 6).Value = "Status";

                    var headerRange = worksheet.Range(1, 1, 1, 6);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    int row = 2;
                    foreach (UnscannedProductReportItem item in currentItems)
                    {
                        worksheet.Cell(row, 1).Value = item.Name;
                        worksheet.Cell(row, 2).Value = item.SKU;
                        worksheet.Cell(row, 3).Value = item.QR;
                        worksheet.Cell(row, 4).Value = item.SystemInventory;
                        worksheet.Cell(row, 5).Value = item.Counted;
                        worksheet.Cell(row, 6).Value = item.Status;

                        var lineRange = worksheet.Range(row, 1, row, 6);
                        lineRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        lineRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(row, 6).Style.Font.FontColor = XLColor.Red;
                        row++;
                    }

                    worksheet.Range(1, 1, row - 1, 6).SetAutoFilter();
                    worksheet.SheetView.FreezeRows(1);
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(fullPath);
                }

                MessageBox.Show(
                    $"Unscanned report exported successfully (Excel):\n{fileName}",
                    "Export Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while exporting:\n" + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvUnscanned_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string columnName = dgvUnscanned.Columns[e.ColumnIndex].Name;

            if (columnName == "colStatus" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                e.CellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            }

            if (columnName == "colSystemInventory" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
            }
        }
    }
}
