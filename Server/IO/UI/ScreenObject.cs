using Server.IO.UI.Display;
using System;
using System.Drawing;
using WindowsInput.Native;

namespace Server.IO.UI
{
    public abstract class ScreenObject : IScreenObject
    {
        public void Click(int x, int y, int wait = 250)
        {
            InputDevice.Click(Left + x, Top + y, wait);
        }

        public void PressKeys(string text)
        {
            InputDevice.SendKeys(text);
        }

        public void PressKey(VirtualKeyCode key)
        {
            InputDevice.PressKey(key);
        }

        public void Click()
        {
            Click((Right - Left) / 2, (Bottom - Top) / 2);
        }

        public IPositionable Parent { get; protected set; }
        public IScreen Screen { get; protected set; }
        public IInputDevice InputDevice { get; protected set; }

        public Color GetPixel(int x, int y)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException("x", "x cannot be less than 0");
            if (Left + x > Right)
                throw new ArgumentOutOfRangeException("x", "x cannot be greater than width");
            if (y < 0)
                throw new ArgumentOutOfRangeException("y", "y cannot be less than 0");
            if (Top + y > Bottom)
                throw new ArgumentOutOfRangeException("y", "y cannot be greater than height");
            return Screen.GetPixel(Left + x, Top + y);
        }

        public virtual int Top { get { return Parent.Top + _relativeTop; } }
        public virtual int Bottom { get { return Parent.Top + _relativeBottom; } }
        public virtual int Left { get { return Parent.Left + _relativeLeft; } }
        public virtual int Right { get { return Parent.Left + _relativeRight; } }

        protected int _relativeTop;
        protected int _relativeBottom;
        protected int _relativeLeft;
        protected int _relativeRight;
    }
}
