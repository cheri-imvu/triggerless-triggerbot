using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Models
{
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            // The DoubleBuffered property is protected, so we need to use reflection.
            var prop = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop?.SetValue(control, enable, null);
        }

        const int WM_SETREDRAW = 0x000B;
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public static void SuspendDrawing(this Control c)
            => SendMessage(c.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

        public static void ResumeDrawing(this Control c, bool invalidate = true)
        {
            SendMessage(c.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            if (invalidate) c.Invalidate(true);
        }

        public static string ToBase36(this long value)
        {
            const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));

            // Max chars needed for 64-bit value in base36 is about 13
            char[] buffer = new char[13];
            int i = buffer.Length;

            long current = value;
            do
            {
                long remainder = current % 36;
                current /= 36;
                buffer[--i] = Alphabet[(int)remainder];
            } while (current != 0);

            return new string(buffer, i, buffer.Length - i);
        }
    }
}