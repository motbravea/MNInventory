namespace MNInventoryUI
{
    partial class BarcodeLabelsForm
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
            panelTop = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblCountSource = new Label();
            cmbCountSource = new ComboBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSelectAll = new Button();
            btnSelectNone = new Button();
            btnRefresh = new Button();
            lblStatus = new Label();
            lblSelectionHint = new Label();
            splitMain = new SplitContainer();
            panelGrid = new Panel();
            dgvProducts = new DataGridView();
            colSelect = new DataGridViewCheckBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colSku = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colBarcode = new DataGridViewTextBoxColumn();
            panelPreview = new Panel();
            lblPreviewCaption = new Label();
            picturePreview = new PictureBox();
            panelBottom = new Panel();
            lblPageSize = new Label();
            cmbPageSize = new ComboBox();
            btnExportPdf = new Button();
            btnExportPng = new Button();
            btnClose = new Button();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            //
            // panelTop
            //
            panelTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblSelectionHint);
            panelTop.Controls.Add(lblStatus);
            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(btnSelectNone);
            panelTop.Controls.Add(btnSelectAll);
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(lblSearch);
            panelTop.Controls.Add(cmbCountSource);
            panelTop.Controls.Add(lblCountSource);
            panelTop.Controls.Add(lblSubtitle);
            panelTop.Controls.Add(lblTitle);
            panelTop.Location = new Point(16, 16);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1048, 150);
            panelTop.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(18, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(234, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Print Barcode Labels";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(22, 52);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(458, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Select scanned products. Each label shows counted quantity (Code128).";
            //
            // lblCountSource
            //
            lblCountSource.AutoSize = true;
            lblCountSource.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCountSource.Location = new Point(22, 82);
            lblCountSource.Name = "lblCountSource";
            lblCountSource.Size = new Size(120, 23);
            lblCountSource.TabIndex = 9;
            lblCountSource.Text = "Quantity source";
            //
            // cmbCountSource
            //
            cmbCountSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCountSource.Font = new Font("Segoe UI", 10F);
            cmbCountSource.FormattingEnabled = true;
            cmbCountSource.Location = new Point(160, 78);
            cmbCountSource.Name = "cmbCountSource";
            cmbCountSource.Size = new Size(560, 31);
            cmbCountSource.TabIndex = 10;
            cmbCountSource.SelectedIndexChanged += cmbCountSource_SelectedIndexChanged;
            //
            // lblSearch
            //
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSearch.Location = new Point(22, 114);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(62, 23);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Search";
            //
            // txtSearch
            //
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(96, 110);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Filter by name, SKU, or barcode…";
            txtSearch.Size = new Size(420, 30);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            //
            // btnSelectAll
            //
            btnSelectAll.Font = new Font("Segoe UI", 9.5F);
            btnSelectAll.Location = new Point(532, 106);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(110, 36);
            btnSelectAll.TabIndex = 4;
            btnSelectAll.Text = "Select All";
            btnSelectAll.UseVisualStyleBackColor = true;
            btnSelectAll.Click += btnSelectAll_Click;
            //
            // btnSelectNone
            //
            btnSelectNone.Font = new Font("Segoe UI", 9.5F);
            btnSelectNone.Location = new Point(648, 106);
            btnSelectNone.Name = "btnSelectNone";
            btnSelectNone.Size = new Size(120, 36);
            btnSelectNone.TabIndex = 5;
            btnSelectNone.Text = "Clear Selection";
            btnSelectNone.UseVisualStyleBackColor = true;
            btnSelectNone.Click += btnSelectNone_Click;
            //
            // btnRefresh
            //
            btnRefresh.Font = new Font("Segoe UI", 9.5F);
            btnRefresh.Location = new Point(778, 106);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 36);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Reload from DB";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            //
            // lblStatus
            //
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(71, 85, 105);
            lblStatus.Location = new Point(530, 14);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(500, 52);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "Loading…";
            lblStatus.TextAlign = ContentAlignment.TopRight;
            //
            // lblSelectionHint
            //
            lblSelectionHint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSelectionHint.Font = new Font("Segoe UI", 9F);
            lblSelectionHint.ForeColor = Color.FromArgb(100, 116, 139);
            lblSelectionHint.Location = new Point(910, 114);
            lblSelectionHint.Name = "lblSelectionHint";
            lblSelectionHint.Size = new Size(120, 23);
            lblSelectionHint.TabIndex = 8;
            lblSelectionHint.Text = "Checkbox = print";
            lblSelectionHint.TextAlign = ContentAlignment.TopRight;
            //
            // splitMain
            //
            splitMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitMain.Location = new Point(16, 176);
            splitMain.Name = "splitMain";
            //
            // splitMain.Panel1
            //
            splitMain.Panel1.Controls.Add(panelGrid);
            //
            // splitMain.Panel2
            //
            splitMain.Panel2.Controls.Add(panelPreview);
            splitMain.Size = new Size(1048, 428);
            splitMain.SplitterDistance = 640;
            splitMain.TabIndex = 1;
            //
            // panelGrid
            //
            panelGrid.BackColor = Color.White;
            panelGrid.Controls.Add(dgvProducts);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 0);
            panelGrid.Name = "panelGrid";
            panelGrid.Padding = new Padding(12);
            panelGrid.Size = new Size(640, 428);
            panelGrid.TabIndex = 0;
            //
            // dgvProducts
            //
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colSelect, colName, colSku, colQty, colBarcode });
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(12, 12);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowTemplate.Height = 36;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(616, 404);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellValueChanged += dgvProducts_CellValueChanged;
            dgvProducts.CurrentCellDirtyStateChanged += dgvProducts_CurrentCellDirtyStateChanged;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            //
            // colSelect
            //
            colSelect.DataPropertyName = "Selected";
            colSelect.FillWeight = 60F;
            colSelect.HeaderText = "Print";
            colSelect.MinimumWidth = 8;
            colSelect.Name = "colSelect";
            //
            // colName
            //
            colName.DataPropertyName = "Name";
            colName.FillWeight = 200F;
            colName.HeaderText = "Product Name";
            colName.MinimumWidth = 8;
            colName.Name = "colName";
            //
            // colSku
            //
            colSku.DataPropertyName = "Sku";
            colSku.FillWeight = 90F;
            colSku.HeaderText = "SKU";
            colSku.MinimumWidth = 8;
            colSku.Name = "colSku";
            //
            // colQty
            //
            colQty.DataPropertyName = "Qty";
            colQty.FillWeight = 60F;
            colQty.HeaderText = "Qty";
            colQty.MinimumWidth = 8;
            colQty.Name = "colQty";
            //
            // colBarcode
            //
            colBarcode.DataPropertyName = "BarcodeRaw";
            colBarcode.FillWeight = 140F;
            colBarcode.HeaderText = "Barcode value";
            colBarcode.MinimumWidth = 8;
            colBarcode.Name = "colBarcode";
            //
            // panelPreview
            //
            panelPreview.BackColor = Color.White;
            panelPreview.Controls.Add(picturePreview);
            panelPreview.Controls.Add(lblPreviewCaption);
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Location = new Point(0, 0);
            panelPreview.Name = "panelPreview";
            panelPreview.Padding = new Padding(12);
            panelPreview.Size = new Size(404, 428);
            panelPreview.TabIndex = 0;
            //
            // lblPreviewCaption
            //
            lblPreviewCaption.AutoSize = true;
            lblPreviewCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPreviewCaption.ForeColor = Color.FromArgb(71, 85, 105);
            lblPreviewCaption.Location = new Point(16, 14);
            lblPreviewCaption.Name = "lblPreviewCaption";
            lblPreviewCaption.Size = new Size(74, 23);
            lblPreviewCaption.TabIndex = 0;
            lblPreviewCaption.Text = "Preview";
            //
            // picturePreview
            //
            picturePreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picturePreview.BackColor = Color.FromArgb(248, 250, 252);
            picturePreview.BorderStyle = BorderStyle.FixedSingle;
            picturePreview.Location = new Point(16, 44);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(368, 360);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            picturePreview.TabIndex = 1;
            picturePreview.TabStop = false;
            //
            // panelBottom
            //
            panelBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnClose);
            panelBottom.Controls.Add(btnExportPng);
            panelBottom.Controls.Add(btnExportPdf);
            panelBottom.Controls.Add(cmbPageSize);
            panelBottom.Controls.Add(lblPageSize);
            panelBottom.Location = new Point(16, 616);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1048, 72);
            panelBottom.TabIndex = 2;
            //
            // lblPageSize
            //
            lblPageSize.AutoSize = true;
            lblPageSize.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPageSize.Location = new Point(18, 24);
            lblPageSize.Name = "lblPageSize";
            lblPageSize.Size = new Size(85, 23);
            lblPageSize.TabIndex = 0;
            lblPageSize.Text = "Page size";
            //
            // cmbPageSize
            //
            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Font = new Font("Segoe UI", 10F);
            cmbPageSize.FormattingEnabled = true;
            cmbPageSize.Items.AddRange(new object[] { "A4", "US Letter" });
            cmbPageSize.Location = new Point(112, 20);
            cmbPageSize.Name = "cmbPageSize";
            cmbPageSize.Size = new Size(160, 31);
            cmbPageSize.TabIndex = 1;
            //
            // btnExportPdf
            //
            btnExportPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportPdf.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnExportPdf.Location = new Point(520, 16);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(170, 42);
            btnExportPdf.TabIndex = 2;
            btnExportPdf.Text = "Export PDF";
            btnExportPdf.UseVisualStyleBackColor = true;
            btnExportPdf.Click += btnExportPdf_Click;
            //
            // btnExportPng
            //
            btnExportPng.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportPng.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnExportPng.Location = new Point(700, 16);
            btnExportPng.Name = "btnExportPng";
            btnExportPng.Size = new Size(190, 42);
            btnExportPng.TabIndex = 3;
            btnExportPng.Text = "Export PNG sheets";
            btnExportPng.UseVisualStyleBackColor = true;
            btnExportPng.Click += btnExportPng_Click;
            //
            // btnClose
            //
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.Location = new Point(930, 16);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 42);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            //
            // BarcodeLabelsForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1082, 704);
            Controls.Add(panelBottom);
            Controls.Add(splitMain);
            Controls.Add(panelTop);
            MinimumSize = new Size(900, 600);
            Name = "BarcodeLabelsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Barcode Labels";
            Load += BarcodeLabelsForm_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panelPreview.ResumeLayout(false);
            panelPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelTop;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblCountSource;
        private ComboBox cmbCountSource;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSelectAll;
        private Button btnSelectNone;
        private Button btnRefresh;
        private Label lblStatus;
        private Label lblSelectionHint;
        private SplitContainer splitMain;
        private Panel panelGrid;
        private DataGridView dgvProducts;
        private DataGridViewCheckBoxColumn colSelect;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colSku;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colBarcode;
        private Panel panelPreview;
        private Label lblPreviewCaption;
        private PictureBox picturePreview;
        private Panel panelBottom;
        private Label lblPageSize;
        private ComboBox cmbPageSize;
        private Button btnExportPdf;
        private Button btnExportPng;
        private Button btnClose;
    }
}
