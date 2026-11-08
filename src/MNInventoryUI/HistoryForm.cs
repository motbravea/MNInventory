using MNInventoryApp.Models;
using MNInventoryApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MNInventoryUI
{
    public partial class HistoryForm : Form
    {
        private readonly MySqlInventoryService _mySqlService;
        private List<ScanHistoryItem> _allHistoryItems = new List<ScanHistoryItem>();

        public HistoryForm(MySqlInventoryService mySqlService)
        {
            _mySqlService = mySqlService ?? throw new ArgumentNullException(nameof(mySqlService));
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            SetupFilterCombo();
            ApplyModernTheme();
            ReloadHistoryFromDatabase();
        }

        private void ReloadHistoryFromDatabase()
        {
            try
            {
                _allHistoryItems = _mySqlService.LoadScanHistory();
                RepopulateUndoMachineCombo();
                LoadFilteredHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Could not load history:\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RepopulateUndoMachineCombo()
        {
            string? previous = cmbUndoMachine.SelectedIndex > 0
                ? cmbUndoMachine.SelectedItem?.ToString()
                : null;

            cmbUndoMachine.Items.Clear();
            cmbUndoMachine.Items.Add("(Choose computer for \"Undo ALL\")");

            foreach (string name in _mySqlService.GetDistinctScanHistoryMachineNames())
                cmbUndoMachine.Items.Add(name);

            if (!string.IsNullOrEmpty(previous))
            {
                int idx = -1;
                for (int i = 0; i < cmbUndoMachine.Items.Count; i++)
                {
                    if (string.Equals(cmbUndoMachine.Items[i]?.ToString(), previous, StringComparison.Ordinal))
                    {
                        idx = i;
                        break;
                    }
                }

                cmbUndoMachine.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                cmbUndoMachine.SelectedIndex = 0;
            }
        }

        private void SetupFilterCombo()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All");
            cmbFilter.Items.Add("Today");
            cmbFilter.Items.Add("Last 24 Hours");
            cmbFilter.Items.Add("Last 7 Days");
            cmbFilter.Items.Add("Scans Only");
            cmbFilter.Items.Add("Undos Only");
            cmbFilter.Items.Add("Imports Only");
            cmbFilter.SelectedIndex = 0;
        }

        private void ApplyModernTheme()
        {
            BackColor = UiTheme.Background;

            UiTheme.StyleCard(panelToolbar, UiTheme.Card);
            UiTheme.StyleCard(panelGrid, UiTheme.Card);
            UiTheme.StyleCard(panelBottom, UiTheme.Card);

            UiTheme.StyleCard(panelActionsCard, UiTheme.Card);
            panelUnitsCard.BackColor = Color.FromArgb(239, 246, 255);
            panelLastScanCard.BackColor = Color.FromArgb(240, 253, 244);

            lblTitle.ForeColor = UiTheme.TextPrimary;
            lblSubtitle.ForeColor = UiTheme.TextSecondary;

            UiTheme.StyleTextInput(txtSearch);
            txtSearch.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            UiTheme.StyleDropdown(cmbFilter);
            cmbFilter.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            UiTheme.StylePrimaryButton(btnExport);
            UiTheme.StyleSecondaryButton(btnClose);
            UiTheme.StyleDropdown(cmbUndoMachine);
            UiTheme.StyleSecondaryButton(btnUndoSelectedScans);
            UiTheme.StyleTintButton(btnUndoAllForMachine);
            StyleGrid();
        }

        private void StylePrimaryButton(Button button)
        {
            UiTheme.StylePrimaryButton(button);
        }

        private void StyleGrid()
        {
            UiTheme.StyleGrid(dgvHistory);
        }

        private void LoadFilteredHistory()
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            string selectedFilter = cmbFilter.SelectedItem?.ToString() ?? "All";

            IEnumerable<ScanHistoryItem> filtered = _allHistoryItems;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filtered = filtered.Where(h =>
                    (h.Name != null && h.Name.ToLower().Contains(searchText)) ||
                    (h.SKU != null && h.SKU.ToLower().Contains(searchText)) ||
                    (h.QR != null && h.QR.ToLower().Contains(searchText)) ||
                    (h.ScannedCode != null && h.ScannedCode.ToLower().Contains(searchText)) ||
                    (h.ActionType != null && h.ActionType.ToLower().Contains(searchText)) ||
                    (h.MachineName != null && h.MachineName.ToLower().Contains(searchText)));
            }

            DateTime now = DateTime.Now;

            switch (selectedFilter)
            {
                case "Today":
                    filtered = filtered.Where(h => h.ScanTime.Date == now.Date);
                    break;

                case "Last 24 Hours":
                    filtered = filtered.Where(h => h.ScanTime >= now.AddHours(-24));
                    break;

                case "Last 7 Days":
                    filtered = filtered.Where(h => h.ScanTime >= now.AddDays(-7));
                    break;

                case "Scans Only":
                    filtered = filtered.Where(h => string.Equals(h.ActionType, "SCAN", StringComparison.OrdinalIgnoreCase));
                    break;

                case "Undos Only":
                    filtered = filtered.Where(h => string.Equals(h.ActionType, "UNDO", StringComparison.OrdinalIgnoreCase));
                    break;

                case "Imports Only":
                    filtered = filtered.Where(h => string.Equals(h.ActionType, "IMPORT", StringComparison.OrdinalIgnoreCase));
                    break;
            }

            List<ScanHistoryItem> result = filtered
                .OrderByDescending(h => h.ScanTime)
                .ToList();

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = result;

            UpdateSummary(result);
        }

        private void UpdateSummary(List<ScanHistoryItem> items)
        {
            lblActionsValue.Text = items.Count.ToString();

            int signedUnits = items.Sum(GetSignedQuantity);
            lblUnitsValue.Text = signedUnits > 0 ? "+" + signedUnits : signedUnits.ToString();

            if (items.Count > 0)
                lblLastScanValue.Text = items.Max(x => x.ScanTime).ToString("yyyy-MM-dd HH:mm:ss");
            else
                lblLastScanValue.Text = "-";
        }

        private int GetSignedQuantity(ScanHistoryItem item)
        {
            if (item == null)
                return 0;

            if (string.Equals(item.ActionType, "UNDO", StringComparison.OrdinalIgnoreCase))
                return -item.QuantityAdded;

            return item.QuantityAdded;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadFilteredHistory();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilteredHistory();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportHistoryToCsv();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUndoSelectedScans_Click(object sender, EventArgs e)
        {
            List<ScanHistoryItem> scans = new List<ScanHistoryItem>();
            foreach (DataGridViewRow row in dgvHistory.SelectedRows)
            {
                if (row.DataBoundItem is ScanHistoryItem h &&
                    string.Equals(h.ActionType, "SCAN", StringComparison.OrdinalIgnoreCase))
                {
                    scans.Add(h);
                }
            }

            if (scans.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "Select one or more rows with Action = SCAN. (UNDO / IMPORT rows cannot be reversed this way.)",
                    "Nothing to undo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int skipped = dgvHistory.SelectedRows.Count - scans.Count;
            int totalUnits = scans.Sum(s => s.QuantityAdded);
            string msg =
                $"Undo {scans.Count} scan line(s) totaling {totalUnits} unit(s)? Counted inventory will be reduced.";
            if (skipped > 0)
                msg += $"\n\n({skipped} non-SCAN row(s) in selection will be skipped.)";

            if (MessageBox.Show(this, msg, "Confirm undo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) !=
                DialogResult.Yes)
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                ApplyScanUndos(scans);
                ReloadHistoryFromDatabase();
                MessageBox.Show(this, "Undo completed.", "History", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Undo failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnUndoAllForMachine_Click(object sender, EventArgs e)
        {
            if (cmbUndoMachine.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    this,
                    "Choose a computer name in the drop-down, then use \"Undo ALL scans from chosen PC\".",
                    "Choose computer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string machine = cmbUndoMachine.SelectedItem?.ToString()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(machine))
                return;

            List<(int SourceProductId, int Quantity, string Sku, string Name, string Qr)> plan =
                _mySqlService.GetAggregatedScanUndosForMachine(machine);

            if (plan.Count == 0)
            {
                MessageBox.Show(
                    this,
                    $"No SCAN history found for \"{machine}\" in the database.",
                    "Nothing to undo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int totalUnits = plan.Sum(x => x.Quantity);
            if (MessageBox.Show(
                    this,
                    $"Undo every SCAN ever recorded for \"{machine}\"?\n\n" +
                    $"Products affected: {plan.Count}\n" +
                    $"Total units to subtract: {totalUnits}\n\n" +
                    "This reads the full database (not only the rows visible in the grid).",
                    "Confirm undo all for PC",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                foreach ((int sourceProductId, int quantity, string sku, string name, string qr) in plan)
                {
                    if (quantity <= 0)
                        continue;

                    var stub = new Product
                    {
                        Id = sourceProductId,
                        SKU = sku,
                        Name = name,
                        QR = qr
                    };

                    _mySqlService.UndoQuantity(stub, quantity);
                }

                ReloadHistoryFromDatabase();
                MessageBox.Show(this, "Undo completed.", "History", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Undo failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Groups SCAN rows by product and calls <see cref="MySqlInventoryService.UndoQuantity"/> for each total.
        /// </summary>
        private void ApplyScanUndos(IEnumerable<ScanHistoryItem> scanRows)
        {
            Dictionary<int, List<ScanHistoryItem>> byProduct = scanRows
                .GroupBy(s => s.SourceProductId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (KeyValuePair<int, List<ScanHistoryItem>> kv in byProduct)
            {
                int qty = kv.Value.Sum(x => x.QuantityAdded);
                if (qty <= 0)
                    continue;

                ScanHistoryItem sample = kv.Value[0];
                var stub = new Product
                {
                    Id = sample.SourceProductId,
                    SKU = sample.SKU ?? string.Empty,
                    Name = sample.Name ?? string.Empty,
                    QR = sample.QR ?? string.Empty
                };

                _mySqlService.UndoQuantity(stub, qty);
            }
        }

        private void ExportHistoryToCsv()
        {
            try
            {
                if (dgvHistory.DataSource is not List<ScanHistoryItem> currentItems || currentItems.Count == 0)
                {
                    MessageBox.Show("No history to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"MNInventory_History_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string fullPath = Path.Combine(desktop, fileName);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Name,SKU,QR,SourceProductId,ScannedCode,ActionType,MachineName,Quantity,ScanTime");

                foreach (ScanHistoryItem item in currentItems)
                {
                    int signedQuantity = GetSignedQuantity(item);

                    sb.AppendLine(
                        $"{EscapeCsv(item.Name)},{EscapeCsv(item.SKU)},{EscapeCsv(item.QR)},{item.SourceProductId},{EscapeCsv(item.ScannedCode)},{EscapeCsv(item.ActionType)},{EscapeCsv(item.MachineName)},{signedQuantity},{item.ScanTime:yyyy-MM-dd HH:mm:ss}");
                }

                File.WriteAllText(fullPath, sb.ToString(), Encoding.UTF8);

                MessageBox.Show(
                    $"History exported successfully:\n{fileName}",
                    "Export Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while exporting history:\n" + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "\"\"";

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string columnName = dgvHistory.Columns[e.ColumnIndex].Name;

            if (columnName == "colQuantityAdded" && e.Value != null)
            {
                DataGridViewRow row = dgvHistory.Rows[e.RowIndex];

                if (row.DataBoundItem is ScanHistoryItem item)
                {
                    int signedQuantity = GetSignedQuantity(item);

                    if (signedQuantity > 0)
                    {
                        e.Value = "+" + signedQuantity;
                        e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    }
                    else if (signedQuantity < 0)
                    {
                        e.Value = signedQuantity.ToString();
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    }
                    else
                    {
                        e.Value = "0";
                        e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                    }

                    e.CellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
                }
            }

            if (columnName == "colActionType" && e.Value != null)
            {
                string actionType = e.Value.ToString();

                e.CellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);

                if (string.Equals(actionType, "SCAN", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else if (string.Equals(actionType, "UNDO", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                }
                else if (string.Equals(actionType, "IMPORT", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                }
            }

            if (columnName == "colScanTime" && e.Value != null)
            {
                if (DateTime.TryParse(e.Value.ToString(), out DateTime scanTime))
                {
                    e.Value = scanTime.ToString("yyyy-MM-dd HH:mm:ss");
                    e.CellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                }
            }
        }
    }
}
