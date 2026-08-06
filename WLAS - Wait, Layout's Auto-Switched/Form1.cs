using System;
using System.Text;
using System.Windows.Forms;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    public partial class Form1 : Form
    {
        private KeyboardHook _hook;
        private StringBuilder _buffer = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;

            WordDictionary.Load();

            notifyIcon1.Visible = true;
            notifyIcon1.Text = "WLAS — Wait, Layout's Auto-Switched";

            автоЗарускСВиндойToolStripMenuItem.Checked = AutoStartHelper.IsEnabled();

            _hook = new KeyboardHook();
            _hook.OnCharTyped += Hook_OnCharTyped;
            _hook.OnSpacePressed += Hook_OnSpacePressed;
            _hook.OnBackspacePressed += Hook_OnBackspacePressed;
            _hook.OnBufferReset += () => _buffer.Clear();
            _hook.Start();

            KeyboardLayoutHelper.IsCurrentLayoutRussian();
            LayoutSwitcher.TryFixWord("test", false);
        }

        private void Hook_OnCharTyped(char c)
        {
            _buffer.Append(c);
        }

        private void Hook_OnSpacePressed()
        {
            if (!AppSettings.IsEnabled)
            {
                _buffer.Clear();
                return;
            }

            if (_buffer.Length == 0)
                return;

            string original = _buffer.ToString();
            _buffer.Clear();

            bool isRussian = KeyboardLayoutHelper.IsCurrentLayoutRussian();
            string fixedWord = LayoutSwitcher.TryFixWord(original, isRussian);

            if (fixedWord == null)
                return;

            _hook.IsPaused = true;

            System.Threading.Tasks.Task.Run(() =>
            {
                System.Threading.Thread.Sleep(20);

                KeyboardLayoutHelper.ResetModifiers();
                KeyboardLayoutHelper.SendBackspaces(original.Length + 1);

                if (isRussian)
                    KeyboardLayoutHelper.SwitchToEnglish();
                else
                    KeyboardLayoutHelper.SwitchToRussian();

                System.Threading.Thread.Sleep(50);

                KeyboardLayoutHelper.TypeText(fixedWord + " ");

                _hook.IsPaused = false;

                notifyIcon1.BalloonTipTitle = "WLAS исправил:";
                notifyIcon1.BalloonTipText = $"{original} → {fixedWord}";
                notifyIcon1.ShowBalloonTip(1000);
            });
        }

        private void Hook_OnBackspacePressed()
        {
            if (_buffer.Length > 0)
                _buffer.Remove(_buffer.Length - 1, 1);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var settings = new SettingsForm();
            settings.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void автоЗарускСВиндойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            menuItem.Checked = !menuItem.Checked;

            if (menuItem.Checked)
                AutoStartHelper.Enable();
            else
                AutoStartHelper.Disable();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _hook?.Stop();
            base.OnFormClosing(e);
        }
    }
}