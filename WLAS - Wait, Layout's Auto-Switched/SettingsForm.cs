using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    public partial class SettingsForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private const int CornerRadius = 12;
        private readonly Color BgColor = ColorTranslator.FromHtml("#000000");
        private readonly Color CardColor = ColorTranslator.FromHtml("#1A1A1A");
        private readonly Color AccentColor = ColorTranslator.FromHtml("#7C3AED");
        private readonly Color AccentHover = ColorTranslator.FromHtml("#9333EA");

        public SettingsForm()
        {
            InitializeComponent();
            ApplyStyle();
        }

        private void ApplyStyle()
        {
            this.BackColor = BgColor;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "WLAS — Настройки";
            this.Size = new Size(380, 300);
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterScreen;

            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "WLAS_BACK_BLACK.ico");
            Icon appIcon = null;
            if (File.Exists(iconPath))
            {
                appIcon = new Icon(iconPath);
                this.Icon = appIcon;
            }

            int formCenterX = this.ClientSize.Width / 2;

            // Иконка + заголовок в одном ряду
            var iconBox = new PictureBox
            {
                Size = new Size(32, 32),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            if (appIcon != null)
                iconBox.Image = appIcon.ToBitmap();

            var lblTitle = new Label
            {
                Text = "WLAS — Настройки",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            int totalWidth = iconBox.Width + 8 + lblTitle.PreferredWidth;
            int startX = formCenterX - totalWidth / 2;

            iconBox.Location = new Point(startX, 20);
            lblTitle.Location = new Point(startX + iconBox.Width + 8, 24);

            this.Controls.Add(iconBox);
            this.Controls.Add(lblTitle);

            // Карточка-контейнер для чекбокса 1
            var card1 = CreateRoundedCard(320, 50);
            card1.Location = new Point(formCenterX - card1.Width / 2, 70);
            this.Controls.Add(card1);

            chkEnabled.Parent = card1;
            chkEnabled.ForeColor = Color.White;
            chkEnabled.BackColor = CardColor;
            chkEnabled.Font = new Font("Segoe UI", 10.5F);
            chkEnabled.FlatStyle = FlatStyle.Flat;
            chkEnabled.AutoSize = false;
            chkEnabled.Size = new Size(290, 30);
            chkEnabled.TextAlign = ContentAlignment.MiddleLeft;
            chkEnabled.Cursor = Cursors.Hand;
            chkEnabled.Location = new Point(15, 10);
            chkEnabled.Text = "WLAS - Wait, Layout Auto-Switched?";

            // Карточка-контейнер для чекбокса 2
            var card2 = CreateRoundedCard(320, 50);
            card2.Location = new Point(formCenterX - card2.Width / 2, 130);
            this.Controls.Add(card2);

            chkUseSlang.Parent = card2;
            chkUseSlang.ForeColor = Color.White;
            chkUseSlang.BackColor = CardColor;
            chkUseSlang.Font = new Font("Segoe UI", 10.5F);
            chkUseSlang.FlatStyle = FlatStyle.Flat;
            chkUseSlang.AutoSize = false;
            chkUseSlang.Size = new Size(290, 30);
            chkUseSlang.TextAlign = ContentAlignment.MiddleLeft;
            chkUseSlang.Cursor = Cursors.Hand;
            chkUseSlang.Location = new Point(15, 10);
            chkUseSlang.Text = "Использовать сленг-словарь";

            // Кнопка "Сохранить"
            btnSave.BackColor = ColorTranslator.FromHtml("#ECD9FF");
            btnSave.ForeColor = Color.Black;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.Size = new Size(160, 42);
            btnSave.Location = new Point(formCenterX - btnSave.Width / 2, 210);
            btnSave.Cursor = Cursors.Hand;
            btnSave.Region = RoundedRegion(btnSave.Width, btnSave.Height, CornerRadius);
            btnSave.Text = "Сохранить";

            btnSave.MouseEnter += (s, e) => btnSave.BackColor = ColorTranslator.FromHtml("#D9BFFF");
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = ColorTranslator.FromHtml("#ECD9FF");
        }

        private Panel CreateRoundedCard(int width, int height)
        {
            var panel = new Panel
            {
                Size = new Size(width, height),
                BackColor = CardColor
            };
            panel.Region = RoundedRegion(width, height, CornerRadius);
            return panel;
        }

        private System.Drawing.Region RoundedRegion(int width, int height, int radius)
        {
            IntPtr hRgn = CreateRoundRectRgn(0, 0, width, height, radius, radius);
            var region = System.Drawing.Region.FromHrgn(hRgn);
            DeleteObject(hRgn);
            return region;
        }

        [DllImport("Gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            chkEnabled.Checked = AppSettings.IsEnabled;
            chkUseSlang.Checked = WordDictionary.UseSlang;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppSettings.IsEnabled = chkEnabled.Checked;
            WordDictionary.UseSlang = chkUseSlang.Checked;

            this.Close();
        }
    }
}