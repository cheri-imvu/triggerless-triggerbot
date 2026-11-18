using System;
using System.Collections.Generic;
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
            var imvuProcs = new List<Process>();
            foreach (var proc in p)
            {
                var procName = proc.ToString().ToLowerInvariant();
                // We added IMVUQualityAgent to support IMVU Lite.
                if (procName.Contains("imvuclient") || procName.Contains("imvuqualityagent"))
                {
                    if (proc.MainWindowHandle != IntPtr.Zero &&
                        proc.MainWindowTitle != null &&
                        proc.MainWindowTitle.StartsWith("[#]")) //don't send to Sandboxie
                    {
                        imvuProcs.Add(proc);
                    }
                }
            }

            if (imvuProcs.Count == 0) return false;
            _imvuChatWindow = imvuProcs[0].MainWindowHandle;
            return true;
        }

        public static void Paste()
        {
            SetFocus();
            _sim.Keyboard.ModifiedKeyStroke(KeyCode.LCONTROL, KeyCode.VK_V);
            _sim.Keyboard.KeyPress(KeyCode.RETURN);

        }

        public static void SendText(string text)
        {
            SetFocus();
            lock (_kbdLock)
            {
                var currentClipboardText = Clipboard.GetText();
                _sim.Keyboard.ModifiedKeyStroke(KeyCode.LCONTROL, KeyCode.VK_A);
                _sim.Keyboard.ModifiedKeyStroke(KeyCode.LCONTROL, KeyCode.VK_X);
                string existingChatText = Clipboard.GetText();
                if (currentClipboardText ==  existingChatText) 
                {
                    existingChatText = string.Empty;
                }

                _sim.Keyboard.TextEntry(text);
                _sim.Keyboard.KeyPress(KeyCode.RETURN);

                if (!string.IsNullOrEmpty(existingChatText))
                {
                    _sim.Keyboard.ModifiedKeyStroke(KeyCode.LCONTROL, KeyCode.VK_V);
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
