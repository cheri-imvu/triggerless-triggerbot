using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Triggerless.Native
{
    public static class InputInjector
    {
        #region P/Invoke

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        #endregion

        #region Constants

        private const int INPUT_KEYBOARD = 1;

        private const uint KEYEVENTF_SCANCODE = 0x0008;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_UNICODE = 0x0004;

        private const ushort VK_SHIFT = 0x10;
        private const ushort VK_CONTROL = 0x11;
        private const ushort VK_MENU = 0x12;

        private const ushort VK_HOME = 0x24;
        private const ushort VK_END = 0x23;
        private const ushort VK_RETURN = 0x0D;
        private const ushort VK_X = 0x58;

        #endregion

        #region Builder

        private class InputBuilder
        {
            private readonly INPUT[] _buffer;
            private int _index;

            public InputBuilder(INPUT[] buffer)
            {
                _buffer = buffer;
                _index = 0;
            }

            public void KeyDown(ushort vk)
            {
                _buffer[_index++] = CreateKey(vk, false);
            }

            public void KeyUp(ushort vk)
            {
                _buffer[_index++] = CreateKey(vk, true);
            }

            public void UnicodeDown(char c)
            {
                _buffer[_index++] = CreateUnicode(c, false);
            }

            public void UnicodeUp(char c)
            {
                _buffer[_index++] = CreateUnicode(c, true);
            }

            public int Count => _index;

            private static INPUT CreateKey(ushort vk, bool keyUp)
            {
                return new INPUT
                {
                    type = INPUT_KEYBOARD,
                    ki = new KEYBDINPUT
                    {
                        wScan = (ushort)MapVirtualKey(vk, 0),
                        dwFlags = KEYEVENTF_SCANCODE | (keyUp ? KEYEVENTF_KEYUP : 0)
                    }
                };
            }

            private static INPUT CreateUnicode(char c, bool keyUp)
            {
                return new INPUT
                {
                    type = INPUT_KEYBOARD,
                    ki = new KEYBDINPUT
                    {
                        wScan = c,
                        dwFlags = KEYEVENTF_UNICODE | (keyUp ? KEYEVENTF_KEYUP : 0)
                    }
                };
            }
        }

        #endregion

        #region Public API

        public static void SendText(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            // Worst case: each char uses modifiers + key (up to ~8 inputs per char)
            INPUT[] buffer = new INPUT[text.Length * 8];
            var b = new InputBuilder(buffer);

            foreach (char c in text)
            {
                short vk = VkKeyScan(c);

                if (vk == -1)
                {
                    b.UnicodeDown(c);
                    b.UnicodeUp(c);
                    continue;
                }

                ushort vkCode = (ushort)(vk & 0xFF);
                ushort modifiers = (ushort)((vk >> 8) & 0xFF);

                // Modifiers down
                if ((modifiers & 1) != 0) b.KeyDown(VK_SHIFT);
                if ((modifiers & 2) != 0) b.KeyDown(VK_CONTROL);
                if ((modifiers & 4) != 0) b.KeyDown(VK_MENU);

                // Key
                b.KeyDown(vkCode);
                b.KeyUp(vkCode);

                // Modifiers up (reverse order)
                if ((modifiers & 4) != 0) b.KeyUp(VK_MENU);
                if ((modifiers & 2) != 0) b.KeyUp(VK_CONTROL);
                if ((modifiers & 1) != 0) b.KeyUp(VK_SHIFT);
            }

            SendInput((uint)b.Count, buffer, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void SendEnter()
        {
            SendKeyPress(VK_RETURN);
        }

        public static void SendHome()
        {
            SendKeyPress(VK_HOME);
        }

        public static void SendShiftEnd()
        {
            INPUT[] buffer = new INPUT[4];
            var b = new InputBuilder(buffer);

            b.KeyDown(VK_SHIFT);
            b.KeyDown(VK_END);
            b.KeyUp(VK_END);
            b.KeyUp(VK_SHIFT);

            SendInput((uint)b.Count, buffer, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void SendCtrlX()
        {
            INPUT[] buffer = new INPUT[4];
            var b = new InputBuilder(buffer);

            b.KeyDown(VK_CONTROL);
            b.KeyDown(VK_X);
            b.KeyUp(VK_X);
            b.KeyUp(VK_CONTROL);

            SendInput((uint)b.Count, buffer, Marshal.SizeOf(typeof(INPUT)));
        }

        #endregion

        #region Helpers

        private static void SendKeyPress(ushort vk)
        {
            INPUT[] buffer = new INPUT[2];
            var b = new InputBuilder(buffer);

            b.KeyDown(vk);
            b.KeyUp(vk);

            SendInput((uint)b.Count, buffer, Marshal.SizeOf(typeof(INPUT)));
        }

        #endregion
    }
}