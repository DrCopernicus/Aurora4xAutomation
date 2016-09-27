using Aurora4xAutomation.IO.UI;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Control = Aurora4xAutomation.IO.UI.Controls.Control;

namespace Aurora4xAutomation.IO
{
    public static class IO
    {
        private static readonly InputSimulator Input = new InputSimulator();
        
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        
        public static void Click(this Window window, int x, int y)
        {
            Cursor.Position = new Point(window.Left + x, window.Top + y);
            Thread.Sleep(250);
            Input.Mouse.LeftButtonClick();
        }

        public static void Click(this Control control, int x, int y, int wait = 250)
        {
            Cursor.Position = new Point(control.Left + x, control.Top + y);
            if (wait != 0)
                Thread.Sleep(wait);
            Input.Mouse.LeftButtonClick();
        }

        public static void Click(this Control control)
        {
            control.Click((control.Right - control.Left) / 2, (control.Bottom - control.Top) / 2);
        }

        public static void SendKeys(this Window window, string text)
        {
            Input.Keyboard.TextEntry(text);
        }

        public static void SendKeys(this Control control, string text)
        {
            Input.Keyboard.TextEntry(text);
        }

        public static void PressKey(this Window window, VirtualKeyCode key)
        {
            Input.Keyboard.KeyPress(key);
        }

        public static void PressKey(this Control control, VirtualKeyCode key)
        {
            Input.Keyboard.KeyPress(key);
        }
    }
}
