using System;
using System.Collections.Generic;
using System.Linq;
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

    public static class LinqExtensions
    {
        public static T FindMostPrevalentItem<T>(
        this IEnumerable<T> source,
        bool preferFirstOnTie = false)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (!preferFirstOnTie)
            {
                return source.GroupBy(x => x)
                             .OrderByDescending(g => g.Count())
                             .First().Key;
            }

            // Prefer the item whose first occurrence appears earliest when counts tie
            return source.Select((v, i) => new { v, i })
                         .GroupBy(x => x.v)
                         .Select(g => new { Key = g.Key, Count = g.Count(), FirstIndex = g.Min(x => x.i) })
                         .OrderByDescending(x => x.Count)
                         .ThenBy(x => x.FirstIndex)
                         .First().Key;
        }

        public static string ToCpath(this short[] paths)
        {
            if (paths == null || !paths.Any()) return "[]";
            string[] numbers = paths.Select(s => s.ToString()).ToArray();
            string allNumbers = string.Join(", ", numbers);
            string result = "[" + allNumbers + "]";
            return result;
        }
    }
}