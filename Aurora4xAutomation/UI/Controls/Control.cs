using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;

namespace Aurora4xAutomation.UI.Controls
{
    public class Control
    {
        public Window Parent { get; set; }
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;

        public Control(Window parent)
        {
            Parent = parent;
        }

        protected static readonly InputSimulator Input = new InputSimulator();

        public void Click(int x, int y)
        {
            Cursor.Position = new Point(Parent.Dimensions.Left + Left + x, Parent.Dimensions.Top + Top + y);
            Thread.Sleep(250);
            Input.Mouse.LeftButtonClick();
        }

        public void Click()
        {
            Click((Right-Left)/2, (Bottom-Top)/2);
        }

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        public Color GetPixel(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, Parent.Dimensions.Left + x, Parent.Dimensions.Top + y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
    }
}
