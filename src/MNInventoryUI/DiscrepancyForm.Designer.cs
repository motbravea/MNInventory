namespace MNInventoryUI
{
    partial class DiscrepancyForm
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
            panelTop = new Panel();
            cmbFilter = new ComboBox();
            panelGrid = new Panel();
            dgvDiscrepancies = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colSKU = new DataGridViewTextBoxColumn();
            colQR = new DataGridViewTextBoxColumn();
            colSystemInventory = new DataGridViewTextBoxColumn();
            colCounted = new DataGridViewTextBoxColumn();
            colAdjustment = new DataGridViewTextBoxColumn();
            panelBottom = new Panel();
            lblTotal = new Label();
            btnClose = new Button();
            panelTop.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiscrepancies).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(36, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(378, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Discrepancy Review";
            //
            // panelTop
            //
            panelTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(cmbFilter);
            panelTop.Location = new Point(36, 95);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1310, 86);
            panelTop.TabIndex = 1;
            //
            // cmbFilter
            //
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Font = new Font("Segoe UI", 11F);
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(22, 24);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(260, 33);
            cmbFilter.TabIndex = 0;
            cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;
            //
            // panelGrid
            //
            panelGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelGrid.BackColor = Color.White;
            panelGrid.Controls.Add(dgvDiscrepancies);
            panelGrid.Location = new Point(36, 198);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1310, 520);
            panelGrid.TabIndex = 2;
            //
            // dgvDiscrepancies
            //
            dgvDiscrepancies.AllowUserToAddRows = false;
            dgvDiscrepancies.AllowUserToDeleteRows = false;
            dgvDiscrepancies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiscrepancies.BackgroundColor = Color.White;
            dgvDiscrepancies.BorderStyle = BorderStyle.None;
            dgvDiscrepancies.ColumnHeadersHeight = 29;
            dgvDiscrepancies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDiscrepancies.Columns.AddRange(new DataGridViewColumn[] { colName, colSKU, colQR, colSystemInventory, colCounted, colAdjustment });
            dgvDiscrepancies.Dock = DockStyle.Fill;
            dgvDiscrepancies.Location = new Point(0, 0);
            dgvDiscrepancies.Name = "dgvDiscrepancies";
            dgvDiscrepancies.ReadOnly = true;
            dgvDiscrepancies.RowHeadersVisible = false;
            dgvDiscrepancies.RowHeadersWidth = 51;
            dgvDiscrepancies.RowTemplate.Height = 35;
            dgvDiscrepancies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiscrepancies.Size = new Size(1310, 520);
            dgvDiscrepancies.TabIndex = 0;
            dgvDiscrepancies.CellFormatting += dgvDiscrepancies_CellFormatting;
            //
            // colName
            //
            colName.DataPropertyName = "Name";
            colName.FillWeight = 180F;
            colName.HeaderText = "Name";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            //
            // colSKU
            //
            colSKU.DataPropertyName = "SKU";
            colSKU.FillWeight = 110F;
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
            // colSystemInventory
            //
            colSystemInventory.DataPropertyName = "SystemInventory";
            colSystemInventory.FillWeight = 120F;
            colSystemInventory.HeaderText = "System Inventory";
            colSystemInventory.MinimumWidth = 6;
            colSystemInventory.Name = "colSystemInventory";
            colSystemInventory.ReadOnly = true;
            //
            // colCounted
            //
            colCounted.DataPropertyName = "Counted";
            colCounted.FillWeight = 90F;
            colCounted.HeaderText = "Counted";
            colCounted.MinimumWidth = 6;
            colCounted.Name = "colCounted";
            colCounted.ReadOnly = true;
            //
            // colAdjustment
            //
            colAdjustment.DataPropertyName = "Adjustment";
            colAdjustment.HeaderText = "Adjustment";
            colAdjustment.MinimumWidth = 6;
            colAdjustment.Name = "colAdjustment";
            colAdjustment.ReadOnly = true;
            //
            // panelBottom
            //
            panelBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(lblTotal);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Location = new Point(36, 735);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1310, 92);
            panelBottom.TabIndex = 3;
            //
            // lblTotal
            //
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(51, 65, 85);
            lblTotal.Location = new Point(22, 31);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(206, 28);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total Discrepancies: 0";
            //
            // btnClose
            //
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnClose.Location = new Point(1105, 20);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(170, 48);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            //
            // DiscrepancyForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1382, 853);
            Controls.Add(panelBottom);
            Controls.Add(panelGrid);
            Controls.Add(panelTop);
            Controls.Add(lblTitle);
            MinimumSize = new Size(1200, 760);
            Name = "DiscrepancyForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Discrepancy Review";
            Load += DiscrepancyForm_Load;
            panelTop.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDiscrepancies).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Panel panelTop;
        private ComboBox cmbFilter;
        private Panel panelGrid;
        private DataGridView dgvDiscrepancies;
        private Panel panelBottom;
        private Label lblTotal;
        private Button btnClose;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colSKU;
        private DataGridViewTextBoxColumn colQR;
        private DataGridViewTextBoxColumn colSystemInventory;
        private DataGridViewTextBoxColumn colCounted;
        private DataGridViewTextBoxColumn colAdjustment;
    }
}
