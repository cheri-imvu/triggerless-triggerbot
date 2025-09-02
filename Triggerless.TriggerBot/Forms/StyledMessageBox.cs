// StyledMessageBox.cs
// .NET Framework 4.7.2 - WinForms helper
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Triggerless.TriggerBot
{
    public static class StyledMessageBox
    {
        // -------- Global defaults (set once at startup) --------
        public static Color DefaultBackColor { get; set; } = Color.FromArgb(32, 32, 32);
        public static Color DefaultForeColor { get; set; } = Color.WhiteSmoke;
        public static Font DefaultFont { get; set; } = (SystemFonts.MessageBoxFont ?? SystemFonts.DefaultFont);
        public static bool DefaultFollowSystemDarkTitleBar { get; set; } = true;

        public static void ConfigureTheme(Color back, Color fore, Font font, bool followSystemDarkTitleBar)
        {
            DefaultBackColor = back;
            DefaultForeColor = fore;
            DefaultFont = font ?? (SystemFonts.MessageBoxFont ?? SystemFonts.DefaultFont);
            DefaultFollowSystemDarkTitleBar = followSystemDarkTitleBar;
        }

        // ---- Win32 helpers for TopMost detection ----
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOPMOST = 0x00000008;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        private static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8) return GetWindowLongPtr64(hWnd, nIndex);
            return new IntPtr(GetWindowLong32(hWnd, nIndex));
        }

        private static bool IsWindowTopMost(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero) return false;
            long ex = GetWindowLongPtr(hwnd, GWL_EXSTYLE).ToInt64();
            return (ex & WS_EX_TOPMOST) != 0;
        }

        // -------- Public overloads mirroring MessageBox.Show --------

        public static DialogResult Show(string text)
        {
            return Show(Program.MainForm, text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(string text, string caption)
        {
            return Show(Program.MainForm, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(Program.MainForm, text, caption, buttons, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(Program.MainForm, text, caption, buttons, icon,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return Show(Program.MainForm, text, caption, buttons, icon, defaultButton,
                        null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        // Owner-aware overloads (no theme args; use defaults)
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return Show(owner, text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            return Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return Show(owner, text, caption, buttons, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(owner, text, caption, buttons, icon,
                        MessageBoxDefaultButton.Button1, null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton)
        {
            return Show(owner, text, caption, buttons, icon, defaultButton,
                        null, null, null, DefaultFollowSystemDarkTitleBar);
        }

        // Master overload (accepts optional per-call theme overrides)
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton,
            Color? backColor,
            Color? foreColor,
            Font font,
            bool followSystemDarkTitleBar)
        {
            // Resolve to global defaults when parameters are null
            Color effectiveBack = backColor.HasValue ? backColor.Value : DefaultBackColor;
            Color effectiveFore = foreColor.HasValue ? foreColor.Value : DefaultForeColor;
            Font effectiveFont = (font != null) ? font : (DefaultFont ?? (SystemFonts.MessageBoxFont ?? SystemFonts.DefaultFont));

            using (var dlg = new ThemedMessageBoxForm(
                text, caption, buttons, icon, defaultButton,
                effectiveBack, effectiveFore, effectiveFont, followSystemDarkTitleBar))
            {
                IntPtr ownerHandle = IntPtr.Zero;
                bool ownerTopMost = false;

                // Try to resolve a Form to read .TopMost; otherwise fall back to Win32 exstyle
                Form ownerForm = null;
                if (owner is Control)
                    ownerForm = ((Control)owner).FindForm();

                if (ownerForm != null)
                {
                    ownerHandle = ownerForm.Handle;
                    ownerTopMost = ownerForm.TopMost;
                }
                else if (owner != null)
                {
                    ownerHandle = owner.Handle;
                    ownerTopMost = IsWindowTopMost(ownerHandle);
                }

                // Inherit TopMost from the owner so the dialog reliably appears above it
                dlg.TopMost = ownerTopMost;

                return ownerHandle != IntPtr.Zero
                    ? dlg.ShowDialog(new WindowWrapper(ownerHandle))
                    : dlg.ShowDialog();
            }
        }

        // ---- Helpers / nested types ----

        private sealed class WindowWrapper : IWin32Window
        {
            public IntPtr Handle { get; private set; }
            public WindowWrapper(IntPtr handle) { Handle = handle; }
        }

        private sealed class ThemedMessageBoxForm : Form
        {
            private readonly Label _lbl;
            private readonly PictureBox _iconBox;
            private readonly FlowLayoutPanel _buttonPanel;
            private readonly MessageBoxButtons _buttons;
            private readonly MessageBoxIcon _icon;
            private readonly MessageBoxDefaultButton _defaultButton;

            private Button _btnOK, _btnCancel, _btnYes, _btnNo, _btnRetry, _btnIgnore, _btnAbort;

            public ThemedMessageBoxForm(
                string text, string caption,
                MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton,
                Color backColor, Color foreColor, Font font, bool followSystemDarkTitleBar)
            {
                Text = string.IsNullOrEmpty(caption) ? Application.ProductName : caption;
                _buttons = buttons;
                _icon = icon;
                _defaultButton = defaultButton;

                // Form chrome similar to MessageBox
                StartPosition = FormStartPosition.CenterParent;
                ShowInTaskbar = false;
                MinimizeBox = false;
                MaximizeBox = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                AutoScaleMode = AutoScaleMode.Dpi;
                Padding = new Padding(14);
                KeyPreview = true;

                // Theme
                this.BackColor = backColor;
                this.ForeColor = foreColor;
                if (font != null) this.Font = font;

                // Layout root
                var root = new TableLayoutPanel();
                root.Dock = DockStyle.Fill;
                root.ColumnCount = 2;
                root.RowCount = 2;
                root.Padding = new Padding(0);
                root.AutoSize = true;
                root.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                Controls.Add(root);

                // Icon
                _iconBox = new PictureBox();
                _iconBox.SizeMode = PictureBoxSizeMode.CenterImage;
                _iconBox.Margin = new Padding(0, 0, 12, 0);
                _iconBox.Width = 48;
                _iconBox.Height = 48;
                _iconBox.Image = GetIconBitmap(icon);
                root.Controls.Add(_iconBox, 0, 0);

                // Text label
                _lbl = new Label();
                _lbl.AutoSize = true;
                _lbl.MaximumSize = new Size(640, 0); // wrap long messages
                _lbl.Text = text;
                _lbl.ForeColor = foreColor;
                _lbl.Margin = new Padding(0, 4, 0, 8);
                root.Controls.Add(_lbl, 1, 0);

                // Buttons panel
                _buttonPanel = new FlowLayoutPanel();
                _buttonPanel.FlowDirection = FlowDirection.RightToLeft;
                _buttonPanel.Dock = DockStyle.Fill;
                _buttonPanel.WrapContents = false;
                _buttonPanel.AutoSize = true;
                _buttonPanel.Margin = new Padding(0, 12, 0, 0);
                root.SetColumnSpan(_buttonPanel, 2);
                root.Controls.Add(_buttonPanel, 0, 1);

                CreateButtons();
                ApplyDefaultButton();
                ApplyCancelBehavior();

                // Apply immersive dark title bar (Win10/11) according to system
                if (followSystemDarkTitleBar)
                {
                    this.HandleCreated += delegate { TryApplyImmersiveDarkTitleBar(this.Handle, IsAppsDarkMode()); };
                    this.VisibleChanged += delegate { TryApplyImmersiveDarkTitleBar(this.Handle, IsAppsDarkMode()); };
                }

                // Play system sound according to icon
                PlaySystemSound(icon);
            }

            // Safe focusing of default button (no "Invisible or disabled control cannot be activated")
            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);
                Control def = GetDefaultButtonControl();
                if (def != null)
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (def.Visible && def.Enabled && def.CanSelect)
                            def.Select();
                    }));
                }
            }

            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                base.OnFormClosing(e);
                // If user clicked the X and no DialogResult set yet, mimic MessageBox behavior
                if (e.CloseReason == CloseReason.UserClosing && this.DialogResult == DialogResult.None)
                {
                    this.DialogResult = FallbackDialogResultForCloseX(_buttons);
                }
            }

            private void CreateButtons()
            {
                _btnOK = MakeBtn("OK", DialogResult.OK);
                _btnCancel = MakeBtn("Cancel", DialogResult.Cancel);
                _btnYes = MakeBtn("Yes", DialogResult.Yes);
                _btnNo = MakeBtn("No", DialogResult.No);
                _btnRetry = MakeBtn("Retry", DialogResult.Retry);
                _btnIgnore = MakeBtn("Ignore", DialogResult.Ignore);
                _btnAbort = MakeBtn("Abort", DialogResult.Abort);

                var order = new List<Button>();
                switch (_buttons)
                {
                    case MessageBoxButtons.OK:
                        order.Add(_btnOK);
                        break;
                    case MessageBoxButtons.OKCancel:
                        order.Add(_btnCancel);
                        order.Add(_btnOK);
                        break;
                    case MessageBoxButtons.YesNo:
                        order.Add(_btnNo);
                        order.Add(_btnYes);
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        order.Add(_btnCancel);
                        order.Add(_btnNo);
                        order.Add(_btnYes);
                        break;
                    case MessageBoxButtons.RetryCancel:
                        order.Add(_btnCancel);
                        order.Add(_btnRetry);
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                        order.Add(_btnIgnore);
                        order.Add(_btnRetry);
                        order.Add(_btnAbort);
                        break;
                    default:
                        order.Add(_btnOK);
                        break;
                }

                foreach (Button b in order)
                    _buttonPanel.Controls.Add(b);
            }

            private Button MakeBtn(string text, DialogResult dr)
            {
                var b = new Button();
                b.Text = text;
                b.DialogResult = dr;
                b.AutoSize = true;
                b.FlatStyle = FlatStyle.System; // keeps native look
                b.Margin = new Padding(6, 0, 0, 0);
                b.Click += delegate { this.DialogResult = dr; };
                return b;
            }

            private void ApplyDefaultButton()
            {
                var visibleButtons = new List<Button>();
                foreach (Control c in _buttonPanel.Controls)
                {
                    Button b = c as Button;
                    if (b != null && b.Visible) visibleButtons.Add(b);
                }

                int idx;
                switch (_defaultButton)
                {
                    case MessageBoxDefaultButton.Button1: idx = 0; break;
                    case MessageBoxDefaultButton.Button2: idx = 1; break;
                    case MessageBoxDefaultButton.Button3: idx = 2; break;
                    default: idx = 0; break;
                }

                if (idx >= 0 && idx < visibleButtons.Count)
                {
                    Button def = visibleButtons[idx];
                    this.AcceptButton = def;
                }
            }

            private Control GetDefaultButtonControl()
            {
                return this.AcceptButton as Control;
            }

            private void ApplyCancelBehavior()
            {
                // Match MessageBox-like behavior for Esc / Close
                Button cancel = FindButton(DialogResult.Cancel);
                if (cancel != null)
                {
                    this.CancelButton = cancel;
                    return;
                }

                if (_buttons == MessageBoxButtons.YesNo)
                {
                    Button no = FindButton(DialogResult.No);
                    if (no != null)
                    {
                        this.CancelButton = no;
                        return;
                    }
                }

                Button ok = FindButton(DialogResult.OK);
                if (ok != null) this.CancelButton = ok;
            }

            private Button FindButton(DialogResult dr)
            {
                foreach (Control c in _buttonPanel.Controls)
                {
                    Button b = c as Button;
                    if (b != null && b.DialogResult == dr) return b;
                }
                return null;
            }

            private static DialogResult FallbackDialogResultForCloseX(MessageBoxButtons buttons)
            {
                // Mirrors MessageBox: X behaves like Cancel when present; otherwise a safe default
                switch (buttons)
                {
                    case MessageBoxButtons.OK: return DialogResult.OK;
                    case MessageBoxButtons.OKCancel: return DialogResult.Cancel;
                    case MessageBoxButtons.YesNo: return DialogResult.No;
                    case MessageBoxButtons.YesNoCancel: return DialogResult.Cancel;
                    case MessageBoxButtons.RetryCancel: return DialogResult.Cancel;
                    case MessageBoxButtons.AbortRetryIgnore: return DialogResult.Cancel;
                    default: return DialogResult.OK;
                }
            }

            private static Bitmap GetIconBitmap(MessageBoxIcon icon)
            {
                // Avoid duplicate case labels: Asterisk==Information, Exclamation==Warning, Hand/Stop==Error
                Icon sysIco;
                switch (icon)
                {
                    case MessageBoxIcon.Information: sysIco = SystemIcons.Information; break; // covers Asterisk
                    case MessageBoxIcon.Warning: sysIco = SystemIcons.Warning; break; // covers Exclamation
                    case MessageBoxIcon.Error: sysIco = SystemIcons.Error; break; // covers Hand/Stop
                    case MessageBoxIcon.Question: sysIco = SystemIcons.Question; break;
                    default: sysIco = null; break;
                }
                return (sysIco != null) ? sysIco.ToBitmap() : null;
            }

            private static void PlaySystemSound(MessageBoxIcon icon)
            {
                switch (icon)
                {
                    case MessageBoxIcon.Information: SystemSounds.Asterisk.Play(); break; // covers Asterisk
                    case MessageBoxIcon.Warning: SystemSounds.Exclamation.Play(); break; // covers Exclamation
                    case MessageBoxIcon.Error: SystemSounds.Hand.Play(); break; // covers Hand/Stop
                    case MessageBoxIcon.Question: SystemSounds.Question.Play(); break;
                    default: break;
                }
            }

            // ---- Dark title bar support (Win10/11) ----

            private static bool IsAppsDarkMode()
            {
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                        @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                    {
                        object v = (key == null) ? null : key.GetValue("AppsUseLightTheme");
                        if (v is int)
                        {
                            // 0 = Dark, 1 = Light
                            return ((int)v) == 0;
                        }
                    }
                }
                catch { }
                return false;
            }

            private static void TryApplyImmersiveDarkTitleBar(IntPtr hwnd, bool enable)
            {
                if (hwnd == IntPtr.Zero) return;
                int on = enable ? 1 : 0;

                // Try both known attributes; ignore failures on older builds
                DwmSetWindowAttribute(hwnd, (DWMWINDOWATTRIBUTE)20, ref on, sizeof(int)); // DWMWA_USE_IMMERSIVE_DARK_MODE
                DwmSetWindowAttribute(hwnd, (DWMWINDOWATTRIBUTE)19, ref on, sizeof(int)); // pre-20 constant on older builds
            }

            private enum DWMWINDOWATTRIBUTE
            {
                DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20 = 19,
                DWMWA_USE_IMMERSIVE_DARK_MODE = 20
            }

            [DllImport("dwmapi.dll")]
            private static extern int DwmSetWindowAttribute(
                IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref int pvAttribute, int cbAttribute);
        }
    }
}
