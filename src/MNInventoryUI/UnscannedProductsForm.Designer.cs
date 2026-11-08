namespace MNInventoryUI
{
    partial class UnscannedProductsForm
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
            btnExport = new Button();
            panelGrid = new Panel();
            dgvUnscanned = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colSKU = new DataGridViewTextBoxColumn();
            colQR = new DataGridViewTextBoxColumn();
            colSystemInventory = new DataGridViewTextBoxColumn();
            colCounted = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            panelBottom = new Panel();
            panelCountCard = new Panel();
            lblCountTitle = new Label();
            lblCountValue = new Label();
            btnClose = new Button();
            panelToolbar.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUnscanned).BeginInit();
            panelBottom.SuspendLayout();
            panelCountCard.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblTitle.Location = new Point(36, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(392, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Unscanned Products";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10.5F);
            lblSubtitle.Location = new Point(39, 72);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(339, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Products in source inventory (qty > 1) not scanned";
            //
            // panelToolbar
            //
            panelToolbar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelToolbar.Controls.Add(txtSearch);
            panelToolbar.Controls.Add(btnExport);
            panelToolbar.Location = new Point(36, 112);
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Size = new Size(1310, 86);
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
            // btnExport
            //
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnExport.Location = new Point(1130, 22);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(150, 38);
            btnExport.TabIndex = 1;
            btnExport.Text = "Export Excel";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            //
            // panelGrid
            //
            panelGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelGrid.Controls.Add(dgvUnscanned);
            panelGrid.Location = new Point(36, 214);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1310, 450);
            panelGrid.TabIndex = 3;
            //
            // dgvUnscanned
            //
            dgvUnscanned.AllowUserToAddRows = false;
            dgvUnscanned.AllowUserToDeleteRows = false;
            dgvUnscanned.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUnscanned.ColumnHeadersHeight = 29;
            dgvUnscanned.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUnscanned.Columns.AddRange(new DataGridViewColumn[] { colName, colSKU, colQR, colSystemInventory, colCounted, colStatus });
            dgvUnscanned.Dock = DockStyle.Fill;
            dgvUnscanned.Location = new Point(0, 0);
            dgvUnscanned.MultiSelect = false;
            dgvUnscanned.Name = "dgvUnscanned";
            dgvUnscanned.ReadOnly = true;
            dgvUnscanned.RowHeadersVisible = false;
            dgvUnscanned.RowHeadersWidth = 51;
            dgvUnscanned.RowTemplate.Height = 35;
            dgvUnscanned.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnscanned.Size = new Size(1310, 450);
            dgvUnscanned.TabIndex = 0;
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
            // colSystemInventory
            //
            colSystemInventory.DataPropertyName = "SystemInventory";
            colSystemInventory.FillWeight = 110F;
            colSystemInventory.HeaderText = "System Inventory";
            colSystemInventory.MinimumWidth = 6;
            colSystemInventory.Name = "colSystemInventory";
            colSystemInventory.ReadOnly = true;
            //
            // colCounted
            //
            colCounted.DataPropertyName = "Counted";
            colCounted.FillWeight = 80F;
            colCounted.HeaderText = "Counted";
            colCounted.MinimumWidth = 6;
            colCounted.Name = "colCounted";
            colCounted.ReadOnly = true;
            //
            // colStatus
            //
            colStatus.DataPropertyName = "Status";
            colStatus.FillWeight = 110F;
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            //
            // panelBottom
            //
            panelBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.Controls.Add(panelCountCard);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Location = new Point(36, 680);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1310, 132);
            panelBottom.TabIndex = 4;
            //
            // panelCountCard
            //
            panelCountCard.Controls.Add(lblCountTitle);
            panelCountCard.Controls.Add(lblCountValue);
            panelCountCard.Location = new Point(22, 24);
            panelCountCard.Name = "panelCountCard";
            panelCountCard.Size = new Size(250, 82);
            panelCountCard.TabIndex = 0;
            //
            // lblCountTitle
            //
            lblCountTitle.AutoSize = true;
            lblCountTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblCountTitle.Location = new Point(16, 12);
            lblCountTitle.Name = "lblCountTitle";
            lblCountTitle.Size = new Size(152, 25);
            lblCountTitle.TabIndex = 0;
            lblCountTitle.Text = "Total Unscanned";
            //
            // lblCountValue
            //
            lblCountValue.AutoSize = true;
            lblCountValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCountValue.Location = new Point(16, 32);
            lblCountValue.Name = "lblCountValue";
            lblCountValue.Size = new Size(40, 46);
            lblCountValue.TabIndex = 1;
            lblCountValue.Text = "0";
            //
            // btnClose
            //
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnClose.Location = new Point(1140, 35);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(140, 44);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            //
            // UnscannedProductsForm
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
            Name = "UnscannedProductsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Unscanned Products Report";
            Load += UnscannedProductsForm_Load;
            panelToolbar.ResumeLayout(false);
            panelToolbar.PerformLayout();
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUnscanned).EndInit();
            panelBottom.ResumeLayout(false);
            panelCountCard.ResumeLayout(false);
            panelCountCard.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelToolbar;
        private TextBox txtSearch;
        private Button btnExport;
        private Panel panelGrid;
        private DataGridView dgvUnscanned;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colSKU;
        private DataGridViewTextBoxColumn colQR;
        private DataGridViewTextBoxColumn colSystemInventory;
        private DataGridViewTextBoxColumn colCounted;
        private DataGridViewTextBoxColumn colStatus;
        private Panel panelBottom;
        private Panel panelCountCard;
        private Label lblCountTitle;
        private Label lblCountValue;
        private Button btnClose;
    }
}
