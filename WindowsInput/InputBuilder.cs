﻿using System;
using System.Collections;
using System.Collections.Generic;
using WindowsInput.Native;

namespace WindowsInput
{
    /// <summary>
    /// A helper class for building a list of <see cref="INPUT"/> messages ready to be sent to the native Windows API.
    /// </summary>
    internal class InputBuilder : IReadOnlyList<INPUT>
    {
        /// <summary>
        /// The public list of <see cref="INPUT"/> messages being built by this instance.
        /// </summary>
        private readonly List<INPUT> _inputList;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputBuilder"/> class.
        /// </summary>
        public InputBuilder()
        {
            _inputList = new List<INPUT>();
        }

        /// <summary>
        /// Returns the list of <see cref="INPUT"/> messages as a <see cref="System.Array"/> of <see cref="INPUT"/> messages.
        /// </summary>
        /// <returns>The <see cref="System.Array"/> of <see cref="INPUT"/> messages.</returns>
        public INPUT[] ToArray()
        {
            return _inputList.ToArray();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list of <see cref="INPUT"/> messages.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the list of <see cref="INPUT"/> messages.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<INPUT> GetEnumerator()
        {
            return _inputList.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list of <see cref="INPUT"/> messages.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the list of <see cref="INPUT"/> messages.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="INPUT"/> at the specified position.
        /// </summary>
        /// <value>The <see cref="INPUT"/> message at the specified position.</value>
        public INPUT this[int position]
        {
            get
            {
                return _inputList[position];
            }
        }

        /// <summary>
        /// Determines if the <see cref="VirtualKeyCode"/> is an ExtendedKey
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        /// <returns>true if the key code is an extended key; otherwise, false.</returns>
        /// <remarks>
        /// The extended keys consist of the ALT and CTRL keys on the right-hand side of the keyboard; the INS, DEL, HOME, END, PAGE UP, PAGE DOWN, and arrow keys in the clusters to the left of the numeric keypad; the NUM LOCK key; the BREAK (CTRL+PAUSE) key; the PRINT SCRN key; and the divide (/) and ENTER keys in the numeric keypad.
        /// 
        /// See http://msdn.microsoft.com/en-us/library/ms646267(v=vs.85).aspx Section "Extended-Key Flag"
        /// </remarks>
        public static bool IsExtendedKey(VirtualKeyCode keyCode)
        {
            if (keyCode == VirtualKeyCode.MENU ||
                keyCode == VirtualKeyCode.LMENU ||
                keyCode == VirtualKeyCode.RMENU ||
                keyCode == VirtualKeyCode.CONTROL ||
                keyCode == VirtualKeyCode.RCONTROL ||
                keyCode == VirtualKeyCode.INSERT ||
                keyCode == VirtualKeyCode.DELETE ||
                keyCode == VirtualKeyCode.HOME ||
                keyCode == VirtualKeyCode.END ||
                keyCode == VirtualKeyCode.PRIOR ||
                keyCode == VirtualKeyCode.NEXT ||
                keyCode == VirtualKeyCode.RIGHT ||
                keyCode == VirtualKeyCode.UP ||
                keyCode == VirtualKeyCode.LEFT ||
                keyCode == VirtualKeyCode.DOWN ||
                keyCode == VirtualKeyCode.NUMLOCK ||
                keyCode == VirtualKeyCode.CANCEL ||
                keyCode == VirtualKeyCode.SNAPSHOT ||
                keyCode == VirtualKeyCode.DIVIDE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a key down to the list of <see cref="INPUT"/> messages.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/>.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddKeyDown(VirtualKeyCode keyCode)
        {
            var down =
                new INPUT(new KEYBDINPUT((UInt16) keyCode, 0, 0));
            _inputList.Add(down);
            return this;
        }

        /// <summary>
        /// Adds a key up to the list of <see cref="INPUT"/> messages.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/>.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddKeyUp(VirtualKeyCode keyCode)
        {
            var up =
                new INPUT(new KEYBDINPUT((UInt16)keyCode, 0, (UInt32)KeyboardFlag.KeyUp));

            _inputList.Add(up);
            return this;
        }

        /// <summary>
        /// Adds a key press to the list of <see cref="INPUT"/> messages which is equivalent to a key down followed by a key up.
        /// </summary>
        /// <param name="keyCode">The <see cref="VirtualKeyCode"/>.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddKeyPress(VirtualKeyCode keyCode)
        {
            AddKeyDown(keyCode);
            AddKeyUp(keyCode);
            return this;
        }

        /// <summary>
        /// Adds the character to the list of <see cref="INPUT"/> messages.
        /// </summary>
        /// <param name="character">The <see cref="System.Char"/> to be added to the list of <see cref="INPUT"/> messages.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddCharacter(char character)
        {
            UInt16 scanCode = character;

            var down = new INPUT(new KEYBDINPUT(0, scanCode, (UInt32) KeyboardFlag.Unicode));

            var up = new INPUT(new KEYBDINPUT(0, scanCode, (UInt32) (KeyboardFlag.KeyUp | KeyboardFlag.Unicode)));

            _inputList.Add(down);
            _inputList.Add(up);
            return this;
        }

        /// <summary>
        /// Adds all of the characters in the specified <see cref="IEnumerable{T}"/> of <see cref="char"/>.
        /// </summary>
        /// <param name="characters">The characters to add.</param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddCharacters(IEnumerable<char> characters)
        {
            foreach (var character in characters)
            {
                AddCharacter(character);
            }
            return this;
        }

        /// <summary>
        /// Moves the mouse relative to its current position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddRelativeMouseMovement(int x, int y)
        {
            var movement = new INPUT(new MOUSEINPUT((UInt32)MouseFlag.Move, x, y));

            _inputList.Add(movement);

            return this;
        }

        /// <summary>
        /// Move the mouse to an absolute position.
        /// </summary>
        /// <param name="absoluteX"></param>
        /// <param name="absoluteY"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddAbsoluteMouseMovement(int absoluteX, int absoluteY)
        {
            var movement = new INPUT(new MOUSEINPUT((UInt32)(MouseFlag.Move | MouseFlag.Absolute),
                absoluteX, absoluteY));

            _inputList.Add(movement);

            return this;
        }

        /// <summary>
        /// Move the mouse to the absolute position on the virtual desktop.
        /// </summary>
        /// <param name="absoluteX"></param>
        /// <param name="absoluteY"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddAbsoluteMouseMovementOnVirtualDesktop(int absoluteX, int absoluteY)
        {
            var movement = new INPUT(new MOUSEINPUT(
                (UInt32)(MouseFlag.Move | MouseFlag.Absolute | MouseFlag.VirtualDesk), absoluteX, absoluteY));

            _inputList.Add(movement);

            return this;
        }

        /// <summary>
        /// Adds a mouse button down for the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonDown(MouseButton button)
        {
            var buttonDown = new INPUT(new MOUSEINPUT((UInt32)ToMouseButtonDownFlag(button)));

            _inputList.Add(buttonDown);

            return this;
        }

        /// <summary>
        /// Adds a mouse button down for the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonDown(int xButtonId)
        {
            var buttonDown = new INPUT(new MOUSEINPUT((UInt32)MouseFlag.XDown, (UInt32)xButtonId));
            _inputList.Add(buttonDown);

            return this;
        }

        /// <summary>
        /// Adds a mouse button up for the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonUp(MouseButton button)
        {
            var buttonUp = new INPUT(new MOUSEINPUT((UInt32)ToMouseButtonUpFlag(button)));
            _inputList.Add(buttonUp);

            return this;
        }

        /// <summary>
        /// Adds a mouse button up for the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonUp(int xButtonId)
        {
            var buttonUp = new INPUT(new MOUSEINPUT((UInt32)MouseFlag.XUp, (UInt32)xButtonId));
            _inputList.Add(buttonUp);

            return this;
        }

        /// <summary>
        /// Adds a single click of the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonClick(MouseButton button)
        {
            return AddMouseButtonDown(button).AddMouseButtonUp(button);
        }

        /// <summary>
        /// Adds a single click of the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonClick(int xButtonId)
        {
            return AddMouseXButtonDown(xButtonId).AddMouseXButtonUp(xButtonId);
        }

        /// <summary>
        /// Adds a double click of the specified button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseButtonDoubleClick(MouseButton button)
        {
            return AddMouseButtonClick(button).AddMouseButtonClick(button);
        }

        /// <summary>
        /// Adds a double click of the specified button.
        /// </summary>
        /// <param name="xButtonId"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseXButtonDoubleClick(int xButtonId)
        {
            return AddMouseXButtonClick(xButtonId).AddMouseXButtonClick(xButtonId);
        }

        /// <summary>
        /// Scroll the vertical mouse wheel by the specified amount.
        /// </summary>
        /// <param name="scrollAmount"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseVerticalWheelScroll(int scrollAmount)
        {
            var scroll = new INPUT(new MOUSEINPUT((UInt32)MouseFlag.VerticalWheel, (UInt32)scrollAmount));

            _inputList.Add(scroll);

            return this;
        }

        /// <summary>
        /// Scroll the horizontal mouse wheel by the specified amount.
        /// </summary>
        /// <param name="scrollAmount"></param>
        /// <returns>This <see cref="InputBuilder"/> instance.</returns>
        public InputBuilder AddMouseHorizontalWheelScroll(int scrollAmount)
        {
            var scroll = new INPUT(new MOUSEINPUT((UInt32)MouseFlag.HorizontalWheel, (UInt32)scrollAmount));

            _inputList.Add(scroll);

            return this;
        }

        private static MouseFlag ToMouseButtonDownFlag(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return MouseFlag.LeftDown;

                case MouseButton.MiddleButton:
                    return MouseFlag.MiddleDown;

                case MouseButton.RightButton:
                    return MouseFlag.RightDown;

                default:
                    return MouseFlag.LeftDown;
            }
        }

        private static MouseFlag ToMouseButtonUpFlag(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return MouseFlag.LeftUp;

                case MouseButton.MiddleButton:
                    return MouseFlag.MiddleUp;

                case MouseButton.RightButton:
                    return MouseFlag.RightUp;

                default:
                    return MouseFlag.LeftUp;
            }
        }

        /// <summary>Gets the number of <see cref="INPUT"/> structs in the collection.</summary>
        /// <returns>The number of <see cref="INPUT"/> structs in the collection. </returns>
        public int Count
        {
            get { return _inputList.Count; }
        }
    }
}