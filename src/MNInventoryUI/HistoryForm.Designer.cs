namespace MNInventoryUI
{
    partial class HistoryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblSubtitle = new Label();
            panelToolbar = new Panel();
            txtSearch = new TextBox();
            cmbFilter = new ComboBox();
            lblBulkUndo = new Label();
            cmbUndoMachine = new ComboBox();
            btnUndoSelectedScans = new Button();
            btnUndoAllForMachine = new Button();
            btnExport = new Button();
            panelGrid = new Panel();
            dgvHistory = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colSKU = new DataGridViewTextBoxColumn();
            colQR = new DataGridViewTextBoxColumn();
            colActionType = new DataGridViewTextBoxColumn();
            colMachineName = new DataGridViewTextBoxColumn();
            colQuantityAdded = new DataGridViewTextBoxColumn();
            colScanTime = new DataGridViewTextBoxColumn();
            panelBottom = new Panel();
            panelActionsCard = new Panel();
            lblActionsTitle = new Label();
            lblActionsValue = new Label();
            panelUnitsCard = new Panel();
            lblUnitsTitle = new Label();
            lblUnitsValue = new Label();
            panelLastScanCard = new Panel();
            lblLastScanTitle = new Label();
            lblLastScanValue = new Label();
            btnClose = new Button();
            panelToolbar.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            panelBottom.SuspendLayout();
            panelActionsCard.SuspendLayout();
            panelUnitsCard.SuspendLayout();
            panelLastScanCard.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(36, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(250, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Scan History";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(39, 72);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(209, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "All scan actions — multi-select SCAN rows to undo, or undo all scans from one PC";
            //
            // panelToolbar
            //
            panelToolbar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelToolbar.BackColor = Color.White;
            panelToolbar.Controls.Add(txtSearch);
            panelToolbar.Controls.Add(cmbFilter);
            panelToolbar.Controls.Add(btnExport);
            panelToolbar.Controls.Add(btnUndoAllForMachine);
            panelToolbar.Controls.Add(btnUndoSelectedScans);
            panelToolbar.Controls.Add(cmbUndoMachine);
            panelToolbar.Controls.Add(lblBulkUndo);
            panelToolbar.Location = new Point(36, 112);
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Size = new Size(1310, 124);
            panelToolbar.TabIndex = 2;
            //
            // txtSearch
            //
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(22, 24);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by name, SKU, or QR...";
            txtSearch.Size = new Size(420, 32);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            //
            // cmbFilter
            //
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Font = new Font("Segoe UI", 11F);
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(460, 24);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(220, 33);
            cmbFilter.TabIndex = 1;
            cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;
            //
            // btnExport
            //
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnExport.Location = new Point(1130, 22);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(150, 38);
            btnExport.TabIndex = 2;
            btnExport.Text = "Export History";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            //
            // lblBulkUndo
            //
            lblBulkUndo.AutoSize = true;
            lblBulkUndo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblBulkUndo.ForeColor = Color.FromArgb(71, 85, 105);
            lblBulkUndo.Location = new Point(22, 62);
            lblBulkUndo.Name = "lblBulkUndo";
            lblBulkUndo.Size = new Size(260, 23);
            lblBulkUndo.TabIndex = 3;
            lblBulkUndo.Text = "Undo SCAN rows (multi-select grid)";
            //
            // cmbUndoMachine
            //
            cmbUndoMachine.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUndoMachine.Font = new Font("Segoe UI", 10.5F);
            cmbUndoMachine.FormattingEnabled = true;
            cmbUndoMachine.Location = new Point(300, 58);
            cmbUndoMachine.Name = "cmbUndoMachine";
            cmbUndoMachine.Size = new Size(280, 31);
            cmbUndoMachine.TabIndex = 4;
            //
            // btnUndoSelectedScans
            //
            btnUndoSelectedScans.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnUndoSelectedScans.Location = new Point(596, 56);
            btnUndoSelectedScans.Name = "btnUndoSelectedScans";
            btnUndoSelectedScans.Size = new Size(220, 36);
            btnUndoSelectedScans.TabIndex = 5;
            btnUndoSelectedScans.Text = "Undo selected scan(s)";
            btnUndoSelectedScans.UseVisualStyleBackColor = true;
            btnUndoSelectedScans.Click += btnUndoSelectedScans_Click;
            //
            // btnUndoAllForMachine
            //
            btnUndoAllForMachine.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnUndoAllForMachine.Location = new Point(828, 56);
            btnUndoAllForMachine.Name = "btnUndoAllForMachine";
            btnUndoAllForMachine.Size = new Size(280, 36);
            btnUndoAllForMachine.TabIndex = 6;
            btnUndoAllForMachine.Text = "Undo ALL scans from chosen PC";
            btnUndoAllForMachine.UseVisualStyleBackColor = true;
            btnUndoAllForMachine.Click += btnUndoAllForMachine_Click;
            //
            // panelGrid
            //
            panelGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelGrid.BackColor = Color.White;
            panelGrid.Controls.Add(dgvHistory);
            panelGrid.Location = new Point(36, 246);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1310, 418);
            panelGrid.TabIndex = 3;
            //
            // dgvHistory
            //
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.ColumnHeadersHeight = 29;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistory.Columns.AddRange(new DataGridViewColumn[] { colName, colSKU, colQR, colActionType, colMachineName, colQuantityAdded, colScanTime });
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.Location = new Point(0, 0);
            dgvHistory.MultiSelect = true;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.RowTemplate.Height = 35;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(1310, 418);
            dgvHistory.TabIndex = 0;
            dgvHistory.CellFormatting += dgvHistory_CellFormatting;
            //
            // colName
            //
            colName.DataPropertyName = "Name";
            colName.FillWeight = 190F;
            colName.HeaderText = "Product Name";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            //
            // colSKU
            //
            colSKU.DataPropertyName = "SKU";
            colSKU.FillWeight = 120F;
            colSKU.HeaderText = "SKU";
            colSKU.MinimumWidth = 6;
            colSKU.Name = "colSKU";
            colSKU.ReadOnly = true;
            //
            // colQR
            //
            colQR.DataPropertyName = "QR";
            colQR.FillWeight = 170F;
            colQR.HeaderText = "QR";
            colQR.MinimumWidth = 6;
            colQR.Name = "colQR";
            colQR.ReadOnly = true;
            //
            // colQuantityAdded
            //
            colQuantityAdded.DataPropertyName = "QuantityAdded";
            colQuantityAdded.FillWeight = 80F;
            colQuantityAdded.HeaderText = "Quantity";
            colQuantityAdded.MinimumWidth = 6;
            colQuantityAdded.Name = "colQuantityAdded";
            colQuantityAdded.ReadOnly = true;
            //
            // colActionType
            //
            colActionType.DataPropertyName = "ActionType";
            colActionType.FillWeight = 80F;
            colActionType.HeaderText = "Action";
            colActionType.MinimumWidth = 6;
            colActionType.Name = "colActionType";
            colActionType.ReadOnly = true;
            //
            // colMachineName
            //
            colMachineName.DataPropertyName = "MachineName";
            colMachineName.FillWeight = 120F;
            colMachineName.HeaderText = "Machine";
            colMachineName.MinimumWidth = 6;
            colMachineName.Name = "colMachineName";
            colMachineName.ReadOnly = true;
            //
            // colScanTime
            //
            colScanTime.DataPropertyName = "ScanTime";
            colScanTime.FillWeight = 120F;
            colScanTime.HeaderText = "Scan Time";
            colScanTime.MinimumWidth = 6;
            colScanTime.Name = "colScanTime";
            colScanTime.ReadOnly = true;
            //
            // panelBottom
            //
            panelBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(panelActionsCard);
            panelBottom.Controls.Add(panelUnitsCard);
            panelBottom.Controls.Add(panelLastScanCard);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Location = new Point(36, 680);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1310, 132);
            panelBottom.TabIndex = 4;
            //
            // panelActionsCard
            //
            panelActionsCard.BackColor = Color.White;
            panelActionsCard.Controls.Add(lblActionsTitle);
            panelActionsCard.Controls.Add(lblActionsValue);
            panelActionsCard.Location = new Point(22, 24);
            panelActionsCard.Name = "panelActionsCard";
            panelActionsCard.Size = new Size(220, 82);
            panelActionsCard.TabIndex = 0;
            //
            // lblActionsTitle
            //
            lblActionsTitle.AutoSize = true;
            lblActionsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblActionsTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblActionsTitle.Location = new Point(16, 12);
            lblActionsTitle.Name = "lblActionsTitle";
            lblActionsTitle.Size = new Size(122, 25);
            lblActionsTitle.TabIndex = 0;
            lblActionsTitle.Text = "Total Actions";
            //
            // lblActionsValue
            //
            lblActionsValue.AutoSize = true;
            lblActionsValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblActionsValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblActionsValue.Location = new Point(16, 32);
            lblActionsValue.Name = "lblActionsValue";
            lblActionsValue.Size = new Size(40, 46);
            lblActionsValue.TabIndex = 1;
            lblActionsValue.Text = "0";
            //
            // panelUnitsCard
            //
            panelUnitsCard.BackColor = Color.FromArgb(239, 246, 255);
            panelUnitsCard.Controls.Add(lblUnitsTitle);
            panelUnitsCard.Controls.Add(lblUnitsValue);
            panelUnitsCard.Location = new Point(260, 24);
            panelUnitsCard.Name = "panelUnitsCard";
            panelUnitsCard.Size = new Size(220, 82);
            panelUnitsCard.TabIndex = 1;
            //
            // lblUnitsTitle
            //
            lblUnitsTitle.AutoSize = true;
            lblUnitsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblUnitsTitle.ForeColor = Color.FromArgb(37, 99, 235);
            lblUnitsTitle.Location = new Point(16, 12);
            lblUnitsTitle.Name = "lblUnitsTitle";
            lblUnitsTitle.Size = new Size(102, 25);
            lblUnitsTitle.TabIndex = 0;
            lblUnitsTitle.Text = "Total Units";
            //
            // lblUnitsValue
            //
            lblUnitsValue.AutoSize = true;
            lblUnitsValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblUnitsValue.ForeColor = Color.FromArgb(37, 99, 235);
            lblUnitsValue.Location = new Point(16, 32);
            lblUnitsValue.Name = "lblUnitsValue";
            lblUnitsValue.Size = new Size(40, 46);
            lblUnitsValue.TabIndex = 1;
            lblUnitsValue.Text = "0";
            //
            // panelLastScanCard
            //
            panelLastScanCard.BackColor = Color.FromArgb(240, 253, 244);
            panelLastScanCard.Controls.Add(lblLastScanTitle);
            panelLastScanCard.Controls.Add(lblLastScanValue);
            panelLastScanCard.Location = new Point(498, 24);
            panelLastScanCard.Name = "panelLastScanCard";
            panelLastScanCard.Size = new Size(360, 82);
            panelLastScanCard.TabIndex = 2;
            //
            // lblLastScanTitle
            //
            lblLastScanTitle.AutoSize = true;
            lblLastScanTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblLastScanTitle.ForeColor = Color.FromArgb(22, 163, 74);
            lblLastScanTitle.Location = new Point(16, 12);
            lblLastScanTitle.Name = "lblLastScanTitle";
            lblLastScanTitle.Size = new Size(91, 25);
            lblLastScanTitle.TabIndex = 0;
            lblLastScanTitle.Text = "Last Scan";
            //
            // lblLastScanValue
            //
            lblLastScanValue.AutoSize = true;
            lblLastScanValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLastScanValue.ForeColor = Color.FromArgb(22, 163, 74);
            lblLastScanValue.Location = new Point(16, 39);
            lblLastScanValue.Name = "lblLastScanValue";
            lblLastScanValue.Size = new Size(24, 32);
            lblLastScanValue.TabIndex = 1;
            lblLastScanValue.Text = "-";
            //
            // btnClose
            //
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnClose.Location = new Point(1140, 35);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(140, 44);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            //
            // HistoryForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1382, 833);
            Controls.Add(panelBottom);
            Controls.Add(panelGrid);
            Controls.Add(panelToolbar);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            MinimumSize = new Size(1200, 760);
            Name = "HistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Scan History";
            Load += HistoryForm_Load;
            panelToolbar.ResumeLayout(false);
            panelToolbar.PerformLayout();
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            panelBottom.ResumeLayout(false);
            panelActionsCard.ResumeLayout(false);
            panelActionsCard.PerformLayout();
            panelUnitsCard.ResumeLayout(false);
            panelUnitsCard.PerformLayout();
            panelLastScanCard.ResumeLayout(false);
            panelLastScanCard.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelToolbar;
        private TextBox txtSearch;
        private ComboBox cmbFilter;
        private Label lblBulkUndo;
        private ComboBox cmbUndoMachine;
        private Button btnUndoSelectedScans;
        private Button btnUndoAllForMachine;
        private Button btnExport;
        private Panel panelGrid;
        private DataGridView dgvHistory;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colSKU;
        private DataGridViewTextBoxColumn colQR;
        private DataGridViewTextBoxColumn colActionType;
        private DataGridViewTextBoxColumn colMachineName;
        private DataGridViewTextBoxColumn colQuantityAdded;
        private DataGridViewTextBoxColumn colScanTime;
        private Panel panelBottom;
        private Panel panelActionsCard;
        private Label lblActionsTitle;
        private Label lblActionsValue;
        private Panel panelUnitsCard;
        private Label lblUnitsTitle;
        private Label lblUnitsValue;
        private Panel panelLastScanCard;
        private Label lblLastScanTitle;
        private Label lblLastScanValue;
        private Button btnClose;
    }
}
