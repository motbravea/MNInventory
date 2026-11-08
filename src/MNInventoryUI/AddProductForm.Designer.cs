namespace MNInventoryUI
{
    partial class AddProductForm
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
            panelMain = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblName = new Label();
            txtName = new TextBox();
            lblSku = new Label();
            txtSku = new TextBox();
            lblSkuHint = new Label();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblSystemInv = new Label();
            txtSystemInventory = new TextBox();
            panelButtons = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            panelMain.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // panelMain
            //
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(lblSystemInv);
            panelMain.Controls.Add(txtSystemInventory);
            panelMain.Controls.Add(lblBarcode);
            panelMain.Controls.Add(txtBarcode);
            panelMain.Controls.Add(lblSkuHint);
            panelMain.Controls.Add(lblSku);
            panelMain.Controls.Add(txtSku);
            panelMain.Controls.Add(lblName);
            panelMain.Controls.Add(txtName);
            panelMain.Controls.Add(lblSubtitle);
            panelMain.Controls.Add(lblTitle);
            panelMain.Location = new Point(16, 16);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(488, 408);
            panelMain.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(18, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add missing product";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(20, 50);
            lblSubtitle.MaximumSize = new Size(450, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(444, 42);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Use when an item is not in the imported file. It is added to master inventory and can be scanned immediately.";
            //
            // lblName
            //
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblName.Location = new Point(20, 104);
            lblName.Name = "lblName";
            lblName.Size = new Size(121, 23);
            lblName.TabIndex = 2;
            lblName.Text = "Product name *";
            //
            // txtName
            //
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.Location = new Point(20, 130);
            txtName.MaxLength = 500;
            txtName.Name = "txtName";
            txtName.Size = new Size(448, 32);
            txtName.TabIndex = 0;
            //
            // lblSku
            //
            lblSku.AutoSize = true;
            lblSku.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSku.Location = new Point(20, 172);
            lblSku.Name = "lblSku";
            lblSku.Size = new Size(42, 23);
            lblSku.TabIndex = 4;
            lblSku.Text = "SKU";
            //
            // txtSku
            //
            txtSku.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSku.Font = new Font("Segoe UI", 11F);
            txtSku.Location = new Point(20, 198);
            txtSku.MaxLength = 200;
            txtSku.Name = "txtSku";
            txtSku.Size = new Size(448, 32);
            txtSku.TabIndex = 1;
            //
            // lblSkuHint
            //
            lblSkuHint.AutoSize = true;
            lblSkuHint.Font = new Font("Segoe UI", 9F);
            lblSkuHint.ForeColor = Color.FromArgb(100, 116, 139);
            lblSkuHint.Location = new Point(20, 232);
            lblSkuHint.Name = "lblSkuHint";
            lblSkuHint.Size = new Size(280, 20);
            lblSkuHint.TabIndex = 6;
            lblSkuHint.Text = "Optional — recommended for labels and reports";
            //
            // lblBarcode
            //
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblBarcode.Location = new Point(20, 258);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(140, 23);
            lblBarcode.TabIndex = 7;
            lblBarcode.Text = "Barcode / QR *";
            //
            // txtBarcode
            //
            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.Font = new Font("Segoe UI", 14F);
            txtBarcode.Location = new Point(20, 284);
            txtBarcode.MaxLength = 500;
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(448, 39);
            txtBarcode.TabIndex = 2;
            //
            // lblSystemInv
            //
            lblSystemInv.AutoSize = true;
            lblSystemInv.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSystemInv.Location = new Point(20, 330);
            lblSystemInv.Name = "lblSystemInv";
            lblSystemInv.Size = new Size(136, 23);
            lblSystemInv.TabIndex = 9;
            lblSystemInv.Text = "System inventory";
            //
            // txtSystemInventory
            //
            txtSystemInventory.Font = new Font("Segoe UI", 11F);
            txtSystemInventory.Location = new Point(20, 356);
            txtSystemInventory.Name = "txtSystemInventory";
            txtSystemInventory.Size = new Size(120, 32);
            txtSystemInventory.TabIndex = 3;
            txtSystemInventory.Text = "0";
            //
            // panelButtons
            //
            panelButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelButtons.BackColor = Color.White;
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Location = new Point(16, 432);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(488, 56);
            panelButtons.TabIndex = 1;
            //
            // btnSave
            //
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSave.Location = new Point(248, 8);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 40);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            //
            // btnCancel
            //
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Location = new Point(368, 8);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            //
            // AddProductForm
            //
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            CancelButton = btnCancel;
            ClientSize = new Size(520, 508);
            Controls.Add(panelButtons);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(520, 508);
            Name = "AddProductForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add missing product";
            Load += AddProductForm_Load;
            KeyDown += AddProductForm_KeyDown;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel panelMain;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblSku;
        private TextBox txtSku;
        private Label lblSkuHint;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblSystemInv;
        private TextBox txtSystemInventory;
        private Panel panelButtons;
        private Button btnSave;
        private Button btnCancel;
    }
}
