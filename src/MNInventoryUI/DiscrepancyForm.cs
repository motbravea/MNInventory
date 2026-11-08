using MNInventoryApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MNInventoryUI
{
    public partial class DiscrepancyForm : Form
    {
        private readonly List<Product> allProducts;

        public DiscrepancyForm(List<Product> scannedProducts)
        {
            InitializeComponent();
            allProducts = scannedProducts ?? new List<Product>();
        }

        private void DiscrepancyForm_Load(object sender, EventArgs e)
        {
            SetupFilterCombo();
            ApplyModernTheme();
            LoadFilteredData();
        }

        private void SetupFilterCombo()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All");
            cmbFilter.Items.Add("Missing");
            cmbFilter.Items.Add("Extra");
            cmbFilter.Items.Add("Balanced");
            cmbFilter.Items.Add("Only Discrepancies");
            cmbFilter.SelectedIndex = 0;
        }

        private void ApplyModernTheme()
        {
            BackColor = UiTheme.Background;

            UiTheme.StyleCard(panelTop, UiTheme.Card);
            UiTheme.StyleCard(panelGrid, UiTheme.Card);
            UiTheme.StyleCard(panelBottom, UiTheme.Card);

            lblTitle.ForeColor = UiTheme.TextPrimary;
            lblTotal.ForeColor = UiTheme.TextSecondary;

            UiTheme.StyleDropdown(cmbFilter);
            cmbFilter.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            UiTheme.StyleSecondaryButton(btnClose);

            StyleGrid();
        }

        private void StyleGrid()
        {
            UiTheme.StyleGrid(dgvDiscrepancies);
        }

        private void LoadFilteredData()
        {
            string selectedFilter = cmbFilter.SelectedItem?.ToString() ?? "All";

            List<Product> filteredProducts = selectedFilter switch
            {
                "Missing" => allProducts.Where(p => p.Adjustment < 0).OrderBy(p => p.Adjustment).ToList(),
                "Extra" => allProducts.Where(p => p.Adjustment > 0).OrderByDescending(p => p.Adjustment).ToList(),
                "Balanced" => allProducts.Where(p => p.Adjustment == 0).OrderBy(p => p.Name).ToList(),
                "Only Discrepancies" => allProducts.Where(p => p.Adjustment != 0).OrderBy(p => p.Adjustment).ToList(),
                _ => allProducts.OrderBy(p => p.Name).ToList()
            };

            dgvDiscrepancies.DataSource = null;
            dgvDiscrepancies.DataSource = filteredProducts;

            int missingProducts = filteredProducts.Count(p => p.Adjustment < 0);
            int extraProducts = filteredProducts.Count(p => p.Adjustment > 0);
            int balancedProducts = filteredProducts.Count(p => p.Adjustment == 0);

            lblTotal.Text = $"Rows: {filteredProducts.Count}   |   Missing: {missingProducts}   |   Extra: {extraProducts}   |   Balanced: {balancedProducts}";
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilteredData();
        }

        private void dgvDiscrepancies_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDiscrepancies.Columns[e.ColumnIndex].Name == "colAdjustment" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int adjustment))
                {
                    e.CellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);

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

            if (dgvDiscrepancies.Columns[e.ColumnIndex].Name == "colCounted" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
