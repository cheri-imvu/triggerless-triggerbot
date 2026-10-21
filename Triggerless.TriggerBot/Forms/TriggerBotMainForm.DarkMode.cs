using Microsoft.Win32;
using System;
using System.Windows.Forms;
using Triggerless.Native;

namespace Triggerless.TriggerBot
{
    public partial class TriggerBotMainForm
    {        

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyImmersiveDarkAccordingToSystem();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_SETTINGCHANGE = 0x001A;
            const int WM_THEMECHANGED = 0x031A;

            if (m.Msg == WM_SETTINGCHANGE || m.Msg == WM_THEMECHANGED)
                ApplyImmersiveDarkAccordingToSystem();
        }

        private void ApplyImmersiveDarkAccordingToSystem()
        {
            bool useDark = Theme.IsAppsDarkMode();
            Theme.SetImmersiveDarkMode(this.Handle, useDark);
        }

        static class Theme
        {
            // Windows stores “apps theme” here: 0 = Dark, 1 = Light
            public static bool IsAppsDarkMode()
            {
                const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
                using (var key = Registry.CurrentUser.OpenSubKey(keyPath))
                {
                    var v = key?.GetValue("AppsUseLightTheme");
                    return (v is int i) ? (i == 0) : false; // default to Light if missing
                }
            }

            public static void SetImmersiveDarkMode(IntPtr hwnd, bool enabled)
            {
                if (hwnd == IntPtr.Zero) return;
                int on = enabled ? 1 : 0;

                // Win10 1809 used 19; Win10 1903+/Win11 use 20. Try both, ignore failures.
                Shell32.DwmSetWindowAttribute(hwnd, (Shell32.DWMWINDOWATTRIBUTE)20, ref on, sizeof(int));
                Shell32.DwmSetWindowAttribute(hwnd, (Shell32.DWMWINDOWATTRIBUTE)19, ref on, sizeof(int));
            }
        }
    }
}
