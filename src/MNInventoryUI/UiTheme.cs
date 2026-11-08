using System.Drawing;
using System.Windows.Forms;

namespace MNInventoryUI
{
    internal static class UiTheme
    {
        public static readonly Color Accent = ColorTranslator.FromHtml("#2563EB");
        public static readonly Color Background = ColorTranslator.FromHtml("#F5F7FA");
        public static readonly Color Card = Color.White;
        public static readonly Color TextPrimary = ColorTranslator.FromHtml("#0F172A");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#64748B");
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Error = Color.FromArgb(220, 38, 38);
        public static readonly Color Border = Color.FromArgb(226, 232, 240);

        public static void StyleCard(Panel panel, Color? backColor = null)
        {
            panel.BackColor = backColor ?? Card;
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void StylePrimaryButton(Button button)
        {
            button.BackColor = Accent;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
        }

        public static void StyleSecondaryButton(Button button)
        {
            button.BackColor = Color.White;
            button.ForeColor = TextPrimary;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 250, 252);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(241, 245, 249);
        }

        public static void StyleTintButton(Button button)
        {
            button.BackColor = Color.FromArgb(239, 246, 255);
            button.ForeColor = Accent;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(191, 219, 254);
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(191, 219, 254);
        }

        public static void StyleDangerButton(Button button)
        {
            button.BackColor = Color.FromArgb(254, 242, 242);
            button.ForeColor = Color.FromArgb(185, 28, 28);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 226, 226);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(252, 165, 165);
        }

        public static void StyleTextInput(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = TextPrimary;
        }

        public static void StyleDropdown(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = TextPrimary;
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.GridColor = Border;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.RowHeadersVisible = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
            grid.AutoGenerateColumns = false;
            grid.RowTemplate.Height = 40;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = TextPrimary;
            grid.ColumnHeadersHeight = 44;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = TextPrimary;

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }
    }
}
