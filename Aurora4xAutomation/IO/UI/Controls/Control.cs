using System;
using System.Drawing;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Control : IScreenObject
    {
        public Control(IScreen screen, int top, int bottom, int left, int right)
        {
            Screen = screen;
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public Control(IScreenObject parent, int top, int bottom, int left, int right)
        {
            Screen = parent.Screen;
            Top = parent.Top + top;
            Bottom = parent.Top + bottom;
            Left = parent.Left + left;
            Right = parent.Left + right;
        }

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

        public IScreen Screen { get; protected set; }
        public int Top { get; protected set; }
        public int Bottom { get; protected set; }
        public int Left { get; protected set; }
        public int Right { get; protected set; }
    }
}