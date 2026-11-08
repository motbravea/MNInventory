using System;
using System.Drawing;
using System.Windows.Forms;
using MNInventoryApp.Services;

namespace MNInventoryUI
{
    public partial class AddProductForm : Form
    {
        private readonly MySqlInventoryService _mySqlService;

        public AddProductForm(MySqlInventoryService mySqlService)
        {
            _mySqlService = mySqlService ?? throw new ArgumentNullException(nameof(mySqlService));
            InitializeComponent();
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            txtSystemInventory.Text = "0";
            BeginInvoke(new Action(() =>
            {
                txtBarcode.Focus();
                txtBarcode.SelectAll();
            }));
        }

        private void ApplyTheme()
        {
            BackColor = UiTheme.Background;
            UiTheme.StyleCard(panelMain, UiTheme.Card);
            UiTheme.StyleCard(panelButtons, UiTheme.Card);

            lblTitle.ForeColor = UiTheme.TextPrimary;
            lblSubtitle.ForeColor = UiTheme.TextSecondary;
            lblName.ForeColor = UiTheme.TextPrimary;
            lblSku.ForeColor = UiTheme.TextPrimary;
            lblBarcode.ForeColor = UiTheme.TextPrimary;
            lblSystemInv.ForeColor = UiTheme.TextPrimary;
            lblSkuHint.ForeColor = UiTheme.TextSecondary;

            UiTheme.StyleTextInput(txtName);
            UiTheme.StyleTextInput(txtSku);
            UiTheme.StyleTextInput(txtBarcode);
            UiTheme.StyleTextInput(txtSystemInventory);

            UiTheme.StylePrimaryButton(btnSave);
            UiTheme.StyleSecondaryButton(btnCancel);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TrySave();
        }

        private void TrySave()
        {
            string name = txtName.Text.Trim();
            string sku = txtSku.Text.Trim();
            string barcode = txtBarcode.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowFieldError("Please enter the product name.", txtName);
                return;
            }

            if (string.IsNullOrWhiteSpace(barcode))
            {
                ShowFieldError("Please enter the barcode / QR code.", txtBarcode);
                return;
            }

            if (!int.TryParse(txtSystemInventory.Text.Trim(), out int systemInv) || systemInv < 0)
            {
                ShowFieldError("System inventory must be a whole number (0 or more).", txtSystemInventory);
                return;
            }

            try
            {
                if (_mySqlService.SourceProductBarcodeExists(barcode))
                {
                    ShowFieldError(
                        "This barcode is already in the master inventory. Use a different code or check the existing product.",
                        txtBarcode);
                    return;
                }

                _mySqlService.AddManualSourceProduct(name, sku, barcode, systemInv);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Cannot add product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcode.Focus();
                txtBarcode.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Could not save the product:\n" + ex.Message,
                    "Database error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void ShowFieldError(string message, Control focusTarget)
        {
            MessageBox.Show(message, "Check your entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            focusTarget.Focus();
            if (focusTarget is TextBox tb)
                tb.SelectAll();
        }

        private void AddProductForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnCancel.PerformClick();
            }
        }
    }
}
