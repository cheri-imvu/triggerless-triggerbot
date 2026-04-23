using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Triggerless.Native;
using WindowsInput;
using KeyCode = WindowsInput.Native.VirtualKeyCode;

namespace Triggerless.TriggerBot.Models
{
    internal static class ImvuWindow
    {
        private static IInputSimulator _sim = new InputSimulator();
        private static object _kbdLock = new object();

        private static IntPtr _imvuChatWindow = IntPtr.Zero;

        public static bool IsRunning
        {
            get {
                if (User32.IsWindow(_imvuChatWindow))
                {
                    return true;
                }
                return Refresh();   
            }            
        }

        public static void SetFocus()
        {
            if (!Refresh())
            {
                LaunchImvu();
                return;
            }
            User32.BringWindowToTop(_imvuChatWindow);
            User32.SetForegroundWindow(_imvuChatWindow);
        }

        private static bool Refresh()
        {
            if (User32.IsWindow(_imvuChatWindow))
            {
                return true;
            }
            _imvuChatWindow = IntPtr.Zero;
            Process[] p = Process.GetProcesses();
            Process imvuProc = null;
            foreach (var proc in p)
            {
                if (proc.ToString().ToLowerInvariant().Contains("imvuclient"))
                {
                    imvuProc = proc;
                    break;
                }
            }

            if (imvuProc != null)
            {
                _imvuChatWindow = imvuProc.MainWindowHandle;
                return true;
            }
            return false;
        }

        public static void SendText(string text)
        {
            SetFocus();
            lock (_kbdLock)
            {
                
                string currClipText = string.Empty;
                if (Clipboard.ContainsText())
                {
                    currClipText = Clipboard.GetText();
                }
                
                _sim.Keyboard.ModifiedKeyStroke(KeyCode.CONTROL, KeyCode.VK_A);
                _sim.Keyboard.ModifiedKeyStroke(KeyCode.CONTROL, KeyCode.VK_X);
                _sim.Keyboard.TextEntry(text);
                _sim.Keyboard.KeyPress(KeyCode.RETURN);
                if (Clipboard.ContainsText())
                {
                    string prevText = Clipboard.GetText();
                    if (prevText == currClipText || String.IsNullOrEmpty(prevText))
                    {
                        return;
                    }
                    _sim.Keyboard.TextEntry(prevText);
                }
            }
        }

        private static void LaunchImvu()
        {
            if (User32.IsWindow(_imvuChatWindow)) return;
            var message = "TriggerBot can't send anything if IMVU Classic isn't running. Launch it now?";
            var dlgResult = StyledMessageBox.Show(Program.MainForm, message,
                "IMVU isn't running", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dlgResult == DialogResult.Yes)
            {
                var exeName = Path.Combine(PlugIn.Location.ImvuLocation, "IMVUQualityAgent.exe");
                if (!File.Exists(exeName))
                {
                    StyledMessageBox.Show("You have to install IMVU Classic first.", "Install IMVU Classic", MessageBoxButtons.OK);
                    Process.Start("https://www.imvu.com/download.php");
                }
                else
                {
                    Process.Start(exeName);
                }
            }
        }
    }
}
