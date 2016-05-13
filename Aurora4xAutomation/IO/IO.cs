using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Aurora4xAutomation.UI;

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
            Cursor.Position = new Point(window.Dimensions.Left + x, window.Dimensions.Top + y);
            Thread.Sleep(250);
            Input.Mouse.LeftButtonClick();
        }

        public static Color GetPixel(this Window window, int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, window.Dimensions.Left + x, window.Dimensions.Top + y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        public static void SendKeys(this Window window, string text)
        {
            Input.Keyboard.TextEntry(text);
        }

        public static void PressKey(this Window window, VirtualKeyCode key)
        {
            Input.Keyboard.KeyPress(key);
        }
    }
}
