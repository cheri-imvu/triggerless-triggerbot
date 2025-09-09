using System;
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