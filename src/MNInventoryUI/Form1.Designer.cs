namespace MNInventoryUI
{
    partial class Form1
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
            panelHeader = new Panel();
            btnStartNewCount = new Button();
            btnImportInventory = new Button();
            btnExport = new Button();
            btnUnscanned = new Button();
            btnHistory = new Button();
            btnDiscrepancies = new Button();
            panelTop = new Panel();
            btnUndo = new Button();
            btnClear = new Button();
            btnAdd = new Button();
            chkQuickScan = new CheckBox();
            txtQuantity = new TextBox();
            lblQuantity = new Label();
            txtQR = new TextBox();
            lblQR = new Label();
            panelStatus = new Panel();
            lblStatus = new Label();
            panelLastScan = new Panel();
            lblLastScanCaption = new Label();
            lblLastTimestamp = new Label();
            lblLastProduct = new Label();
            lblLastDetails = new Label();
            panelGridSection = new Panel();
            lblGridTitle = new Label();
            btnImportScannedExcel = new Button();
            btnAddMissingProduct = new Button();
            btnBarcodeLabels = new Button();
            dgvProducts = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colSKU = new DataGridViewTextBoxColumn();
            colQR = new DataGridViewTextBoxColumn();
            colSystemInventory = new DataGridViewTextBoxColumn();
            colCounted = new DataGridViewTextBoxColumn();
            colAdjustment = new DataGridViewTextBoxColumn();
            panelProductsCard = new Panel();
            lblProductsTitle = new Label();
            lblProductsValue = new Label();
            panelMissingCard = new Panel();
            lblMissingTitle = new Label();
            lblMissingValue = new Label();
            panelExtraCard = new Panel();
            lblExtraTitle = new Label();
            lblExtraValue = new Label();
            panelHistoryCard = new Panel();
            lblHistoryTitle = new Label();
            lblHistoryValue = new Label();
            panelHeader.SuspendLayout();
            panelTop.SuspendLayout();
            panelStatus.SuspendLayout();
            panelLastScan.SuspendLayout();
            panelGridSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panelProductsCard.SuspendLayout();
            panelMissingCard.SuspendLayout();
            panelExtraCard.SuspendLayout();
            panelHistoryCard.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(20, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(421, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "MNInventory";
            lblTitle.Click += lblTitle_Click;
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(24, 60);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(341, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Fast, accurate multi-device stock counting";
            //
            // panelHeader
            //
            panelHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(btnStartNewCount);
            panelHeader.Controls.Add(btnImportInventory);
            panelHeader.Controls.Add(btnExport);
            panelHeader.Controls.Add(btnUnscanned);
            panelHeader.Controls.Add(btnHistory);
            panelHeader.Controls.Add(btnDiscrepancies);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Location = new Point(24, 18);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1240, 100);
            panelHeader.TabIndex = 0;
            //
            // btnStartNewCount
            //
            btnStartNewCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStartNewCount.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnStartNewCount.Location = new Point(464, 24);
            btnStartNewCount.Name = "btnStartNewCount";
            btnStartNewCount.Size = new Size(114, 66);
            btnStartNewCount.TabIndex = 7;
            btnStartNewCount.Text = "Start New Count";
            btnStartNewCount.UseVisualStyleBackColor = true;
            btnStartNewCount.Click += btnStartNewCount_Click;
            //
            // btnImportInventory
            //
            btnImportInventory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImportInventory.Location = new Point(985, 31);
            btnImportInventory.Name = "btnImportInventory";
            btnImportInventory.Size = new Size(116, 55);
            btnImportInventory.TabIndex = 8;
            btnImportInventory.Text = "Import New Inventory";
            btnImportInventory.UseVisualStyleBackColor = true;
            btnImportInventory.Click += button1_Click;
            //
            // btnExport
            //
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnExport.Location = new Point(1122, 30);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(98, 55);
            btnExport.TabIndex = 5;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            //
            // btnUnscanned
            //
            btnUnscanned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUnscanned.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnUnscanned.Location = new Point(599, 24);
            btnUnscanned.Name = "btnUnscanned";
            btnUnscanned.Size = new Size(111, 67);
            btnUnscanned.TabIndex = 4;
            btnUnscanned.Text = "Check Unscanned";
            btnUnscanned.UseVisualStyleBackColor = true;
            btnUnscanned.Click += btnUnscanned_Click;
            //
            // btnHistory
            //
            btnHistory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHistory.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnHistory.Location = new Point(864, 30);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(110, 55);
            btnHistory.TabIndex = 3;
            btnHistory.Text = "History";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            //
            // btnDiscrepancies
            //
            btnDiscrepancies.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDiscrepancies.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnDiscrepancies.Location = new Point(730, 30);
            btnDiscrepancies.Name = "btnDiscrepancies";
            btnDiscrepancies.Size = new Size(121, 55);
            btnDiscrepancies.TabIndex = 2;
            btnDiscrepancies.Text = "Discrepancies";
            btnDiscrepancies.UseVisualStyleBackColor = true;
            btnDiscrepancies.Click += btnDiscrepancies_Click;
            //
            // panelTop
            //
            panelTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(btnUndo);
            panelTop.Controls.Add(btnClear);
            panelTop.Controls.Add(btnAdd);
            panelTop.Controls.Add(txtQuantity);
            panelTop.Controls.Add(lblQuantity);
            panelTop.Controls.Add(chkQuickScan);
            panelTop.Controls.Add(txtQR);
            panelTop.Controls.Add(lblQR);
            panelTop.Location = new Point(24, 130);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1240, 108);
            panelTop.TabIndex = 1;
            //
            // btnUndo
            //
            btnUndo.Font = new Font("Segoe UI", 10.5F);
            btnUndo.Location = new Point(1092, 50);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(108, 42);
            btnUndo.TabIndex = 6;
            btnUndo.Text = "Undo";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            //
            // btnClear
            //
            btnClear.Font = new Font("Segoe UI", 10.5F);
            btnClear.Location = new Point(968, 50);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(108, 42);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            //
            // btnAdd
            //
            btnAdd.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnAdd.Location = new Point(780, 50);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(170, 42);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add Product";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            //
            // txtQuantity
            //
            txtQuantity.Font = new Font("Segoe UI", 14F);
            txtQuantity.Location = new Point(662, 53);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(95, 39);
            txtQuantity.TabIndex = 3;
            txtQuantity.Text = "1";
            txtQuantity.KeyDown += txtQuantity_KeyDown;
            //
            // lblQuantity
            //
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblQuantity.ForeColor = Color.FromArgb(51, 65, 85);
            lblQuantity.Location = new Point(662, 22);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(87, 25);
            lblQuantity.TabIndex = 2;
            lblQuantity.Text = "Quantity";
            //
            // txtQR
            //
            txtQR.Font = new Font("Segoe UI", 14F);
            txtQR.Location = new Point(20, 53);
            txtQR.Name = "txtQR";
            txtQR.Size = new Size(620, 39);
            txtQR.TabIndex = 1;
            txtQR.KeyDown += txtQR_KeyDown;
            //
            // lblQR
            //
            lblQR.AutoSize = true;
            lblQR.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblQR.ForeColor = Color.FromArgb(51, 65, 85);
            lblQR.Location = new Point(20, 22);
            lblQR.Name = "lblQR";
            lblQR.Size = new Size(125, 25);
            lblQR.TabIndex = 0;
            lblQR.Text = "QR / Barcode";
            //
            // chkQuickScan
            //
            chkQuickScan.AutoSize = true;
            chkQuickScan.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            chkQuickScan.ForeColor = Color.FromArgb(51, 65, 85);
            chkQuickScan.Location = new Point(200, 22);
            chkQuickScan.Name = "chkQuickScan";
            chkQuickScan.Size = new Size(260, 27);
            chkQuickScan.TabIndex = 7;
            chkQuickScan.Text = "Quick Scan Mode (+1 auto)";
            chkQuickScan.UseVisualStyleBackColor = true;
            chkQuickScan.CheckedChanged += chkQuickScan_CheckedChanged;
            //
            // panelStatus
            //
            panelStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelStatus.BackColor = Color.FromArgb(239, 246, 255);
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Location = new Point(24, 250);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(1240, 46);
            panelStatus.TabIndex = 2;
            //
            // lblStatus
            //
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblStatus.Location = new Point(18, 11);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(62, 25);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Ready";
            //
            // panelLastScan
            //
            panelLastScan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelLastScan.BackColor = Color.White;
            panelLastScan.Controls.Add(lblLastScanCaption);
            panelLastScan.Controls.Add(lblLastTimestamp);
            panelLastScan.Controls.Add(lblLastProduct);
            panelLastScan.Controls.Add(lblLastDetails);
            panelLastScan.Location = new Point(24, 308);
            panelLastScan.Name = "panelLastScan";
            panelLastScan.Size = new Size(1240, 124);
            panelLastScan.TabIndex = 3;
            //
            // lblLastScanCaption
            //
            lblLastScanCaption.AutoSize = true;
            lblLastScanCaption.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblLastScanCaption.ForeColor = Color.FromArgb(100, 116, 139);
            lblLastScanCaption.Location = new Point(20, 14);
            lblLastScanCaption.Name = "lblLastScanCaption";
            lblLastScanCaption.Size = new Size(161, 25);
            lblLastScanCaption.TabIndex = 0;
            lblLastScanCaption.Text = "Last scanned item";
            //
            // lblLastTimestamp
            //
            lblLastTimestamp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLastTimestamp.AutoSize = true;
            lblLastTimestamp.Font = new Font("Segoe UI", 9.5F);
            lblLastTimestamp.ForeColor = Color.FromArgb(100, 116, 139);
            lblLastTimestamp.Location = new Point(1100, 17);
            lblLastTimestamp.Name = "lblLastTimestamp";
            lblLastTimestamp.Size = new Size(113, 21);
            lblLastTimestamp.TabIndex = 3;
            lblLastTimestamp.Text = "No recent scan";
            //
            // lblLastProduct
            //
            lblLastProduct.AutoSize = true;
            lblLastProduct.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblLastProduct.ForeColor = Color.FromArgb(15, 23, 42);
            lblLastProduct.Location = new Point(20, 42);
            lblLastProduct.Name = "lblLastProduct";
            lblLastProduct.Size = new Size(278, 37);
            lblLastProduct.TabIndex = 1;
            lblLastProduct.Text = "Last scanned product";
            //
            // lblLastDetails
            //
            lblLastDetails.AutoSize = true;
            lblLastDetails.Font = new Font("Segoe UI", 11F);
            lblLastDetails.ForeColor = Color.FromArgb(71, 85, 105);
            lblLastDetails.Location = new Point(20, 82);
            lblLastDetails.Name = "lblLastDetails";
            lblLastDetails.Size = new Size(445, 25);
            lblLastDetails.TabIndex = 2;
            lblLastDetails.Text = "SKU: -   |   System: -   |   Counted: -   |   Adjustment: -";
            //
            // panelGridSection
            //
            panelGridSection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelGridSection.BackColor = Color.White;
            panelGridSection.Controls.Add(lblGridTitle);
            panelGridSection.Controls.Add(dgvProducts);
            panelGridSection.Controls.Add(btnBarcodeLabels);
            panelGridSection.Controls.Add(btnImportScannedExcel);
            panelGridSection.Controls.Add(btnAddMissingProduct);
            panelGridSection.Location = new Point(24, 444);
            panelGridSection.Name = "panelGridSection";
            panelGridSection.Size = new Size(1240, 254);
            panelGridSection.TabIndex = 4;
            //
            // lblGridTitle
            //
            lblGridTitle.AutoSize = true;
            lblGridTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblGridTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblGridTitle.Location = new Point(20, 14);
            lblGridTitle.Name = "lblGridTitle";
            lblGridTitle.Size = new Size(146, 28);
            lblGridTitle.TabIndex = 0;
            lblGridTitle.Text = "Scanned Items";
            //
            // btnAddMissingProduct
            //
            btnAddMissingProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnAddMissingProduct.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAddMissingProduct.Location = new Point(188, 8);
            btnAddMissingProduct.Name = "btnAddMissingProduct";
            btnAddMissingProduct.Size = new Size(210, 36);
            btnAddMissingProduct.TabIndex = 3;
            btnAddMissingProduct.Text = "Add Missing Product";
            btnAddMissingProduct.UseVisualStyleBackColor = true;
            btnAddMissingProduct.Click += btnAddMissingProduct_Click;
            //
            // btnImportScannedExcel
            //
            btnImportScannedExcel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnImportScannedExcel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnImportScannedExcel.Location = new Point(404, 8);
            btnImportScannedExcel.Name = "btnImportScannedExcel";
            btnImportScannedExcel.Size = new Size(200, 36);
            btnImportScannedExcel.TabIndex = 4;
            btnImportScannedExcel.Text = "Import Scanned Excel";
            btnImportScannedExcel.UseVisualStyleBackColor = true;
            btnImportScannedExcel.Click += btnImportScannedExcel_Click;
            //
            // btnBarcodeLabels
            //
            btnBarcodeLabels.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBarcodeLabels.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnBarcodeLabels.Location = new Point(1090, 8);
            btnBarcodeLabels.Name = "btnBarcodeLabels";
            btnBarcodeLabels.Size = new Size(132, 36);
            btnBarcodeLabels.TabIndex = 2;
            btnBarcodeLabels.Text = "Barcode Labels";
            btnBarcodeLabels.UseVisualStyleBackColor = true;
            btnBarcodeLabels.Click += btnBarcodeLabels_Click;
            //
            // dgvProducts
            //
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvProducts.ColumnHeadersHeight = 29;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colName, colSKU, colQR, colSystemInventory, colCounted, colAdjustment });
            dgvProducts.Location = new Point(20, 54);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.RowTemplate.Height = 35;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1198, 178);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellFormatting += dgvProducts_CellFormatting;
            //
            // colName
            //
            colName.DataPropertyName = "Name";
            colName.FillWeight = 190F;
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
            // colAdjustment
            //
            colAdjustment.DataPropertyName = "Adjustment";
            colAdjustment.FillWeight = 90F;
            colAdjustment.HeaderText = "Adjustment";
            colAdjustment.MinimumWidth = 6;
            colAdjustment.Name = "colAdjustment";
            colAdjustment.ReadOnly = true;
            //
            // panelProductsCard
            //
            panelProductsCard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panelProductsCard.BackColor = Color.White;
            panelProductsCard.Controls.Add(lblProductsTitle);
            panelProductsCard.Controls.Add(lblProductsValue);
            panelProductsCard.Location = new Point(24, 714);
            panelProductsCard.Name = "panelProductsCard";
            panelProductsCard.Size = new Size(290, 90);
            panelProductsCard.TabIndex = 5;
            //
            // lblProductsTitle
            //
            lblProductsTitle.AutoSize = true;
            lblProductsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblProductsTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblProductsTitle.Location = new Point(18, 14);
            lblProductsTitle.Name = "lblProductsTitle";
            lblProductsTitle.Size = new Size(133, 25);
            lblProductsTitle.TabIndex = 0;
            lblProductsTitle.Text = "Total Products";
            //
            // lblProductsValue
            //
            lblProductsValue.AutoSize = true;
            lblProductsValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblProductsValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblProductsValue.Location = new Point(18, 34);
            lblProductsValue.Name = "lblProductsValue";
            lblProductsValue.Size = new Size(43, 50);
            lblProductsValue.TabIndex = 1;
            lblProductsValue.Text = "0";
            //
            // panelMissingCard
            //
            panelMissingCard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panelMissingCard.BackColor = Color.FromArgb(254, 242, 242);
            panelMissingCard.Controls.Add(lblMissingTitle);
            panelMissingCard.Controls.Add(lblMissingValue);
            panelMissingCard.Location = new Point(342, 714);
            panelMissingCard.Name = "panelMissingCard";
            panelMissingCard.Size = new Size(290, 90);
            panelMissingCard.TabIndex = 6;
            //
            // lblMissingTitle
            //
            lblMissingTitle.AutoSize = true;
            lblMissingTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblMissingTitle.ForeColor = Color.FromArgb(127, 29, 29);
            lblMissingTitle.Location = new Point(18, 14);
            lblMissingTitle.Name = "lblMissingTitle";
            lblMissingTitle.Size = new Size(78, 25);
            lblMissingTitle.TabIndex = 0;
            lblMissingTitle.Text = "Missing";
            //
            // lblMissingValue
            //
            lblMissingValue.AutoSize = true;
            lblMissingValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblMissingValue.ForeColor = Color.FromArgb(220, 38, 38);
            lblMissingValue.Location = new Point(18, 34);
            lblMissingValue.Name = "lblMissingValue";
            lblMissingValue.Size = new Size(43, 50);
            lblMissingValue.TabIndex = 1;
            lblMissingValue.Text = "0";
            //
            // panelExtraCard
            //
            panelExtraCard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panelExtraCard.BackColor = Color.FromArgb(240, 253, 244);
            panelExtraCard.Controls.Add(lblExtraTitle);
            panelExtraCard.Controls.Add(lblExtraValue);
            panelExtraCard.Location = new Point(660, 714);
            panelExtraCard.Name = "panelExtraCard";
            panelExtraCard.Size = new Size(290, 90);
            panelExtraCard.TabIndex = 7;
            //
            // lblExtraTitle
            //
            lblExtraTitle.AutoSize = true;
            lblExtraTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblExtraTitle.ForeColor = Color.FromArgb(20, 83, 45);
            lblExtraTitle.Location = new Point(18, 14);
            lblExtraTitle.Name = "lblExtraTitle";
            lblExtraTitle.Size = new Size(56, 25);
            lblExtraTitle.TabIndex = 0;
            lblExtraTitle.Text = "Extra";
            //
            // lblExtraValue
            //
            lblExtraValue.AutoSize = true;
            lblExtraValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblExtraValue.ForeColor = Color.FromArgb(22, 163, 74);
            lblExtraValue.Location = new Point(18, 34);
            lblExtraValue.Name = "lblExtraValue";
            lblExtraValue.Size = new Size(69, 50);
            lblExtraValue.TabIndex = 1;
            lblExtraValue.Text = "+0";
            //
            // panelHistoryCard
            //
            panelHistoryCard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panelHistoryCard.BackColor = Color.White;
            panelHistoryCard.Controls.Add(lblHistoryTitle);
            panelHistoryCard.Controls.Add(lblHistoryValue);
            panelHistoryCard.Location = new Point(978, 714);
            panelHistoryCard.Name = "panelHistoryCard";
            panelHistoryCard.Size = new Size(286, 90);
            panelHistoryCard.TabIndex = 8;
            //
            // lblHistoryTitle
            //
            lblHistoryTitle.AutoSize = true;
            lblHistoryTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblHistoryTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblHistoryTitle.Location = new Point(18, 14);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Size = new Size(75, 25);
            lblHistoryTitle.TabIndex = 0;
            lblHistoryTitle.Text = "History";
            //
            // lblHistoryValue
            //
            lblHistoryValue.AutoSize = true;
            lblHistoryValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblHistoryValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblHistoryValue.Location = new Point(18, 34);
            lblHistoryValue.Name = "lblHistoryValue";
            lblHistoryValue.Size = new Size(43, 50);
            lblHistoryValue.TabIndex = 1;
            lblHistoryValue.Text = "0";
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1288, 824);
            Controls.Add(panelHistoryCard);
            Controls.Add(panelExtraCard);
            Controls.Add(panelMissingCard);
            Controls.Add(panelProductsCard);
            Controls.Add(panelGridSection);
            Controls.Add(panelLastScan);
            Controls.Add(panelStatus);
            Controls.Add(panelTop);
            Controls.Add(panelHeader);
            MinimumSize = new Size(1180, 860);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MNInventory";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            panelLastScan.ResumeLayout(false);
            panelLastScan.PerformLayout();
            panelGridSection.ResumeLayout(false);
            panelGridSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panelProductsCard.ResumeLayout(false);
            panelProductsCard.PerformLayout();
            panelMissingCard.ResumeLayout(false);
            panelMissingCard.PerformLayout();
            panelExtraCard.ResumeLayout(false);
            panelExtraCard.PerformLayout();
            panelHistoryCard.ResumeLayout(false);
            panelHistoryCard.PerformLayout();
            ResumeLayout(false);
        }

        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelHeader;
        private Button btnExport;
        private Button btnUnscanned;
        private Button btnHistory;
        private Button btnDiscrepancies;

        private Panel panelTop;
        private Label lblQR;
        private TextBox txtQR;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private CheckBox chkQuickScan;
        private Button btnAdd;
        private Button btnClear;
        private Button btnUndo;

        private Panel panelStatus;
        private Label lblStatus;

        private Panel panelLastScan;
        private Label lblLastScanCaption;
        private Label lblLastProduct;
        private Label lblLastDetails;
        private Label lblLastTimestamp;

        private Panel panelGridSection;
        private Label lblGridTitle;
        private Button btnImportScannedExcel;
        private Button btnAddMissingProduct;
        private Button btnBarcodeLabels;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colSKU;
        private DataGridViewTextBoxColumn colQR;
        private DataGridViewTextBoxColumn colSystemInventory;
        private DataGridViewTextBoxColumn colCounted;
        private DataGridViewTextBoxColumn colAdjustment;

        private Panel panelProductsCard;
        private Label lblProductsTitle;
        private Label lblProductsValue;

        private Panel panelMissingCard;
        private Label lblMissingTitle;
        private Label lblMissingValue;

        private Panel panelExtraCard;
        private Label lblExtraTitle;
        private Label lblExtraValue;

        private Panel panelHistoryCard;
        private Label lblHistoryTitle;
        private Label lblHistoryValue;
        private Button btnImportInventory;
        private Button btnStartNewCount;
    }
}
