using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Aurora4xAutomation.IO.UI
{
    public class NativeMethods
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,

            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,

            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14,
        }

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("USER32.DLL")]
        public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "PostMessage", CallingConvention = CallingConvention.Winapi)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        public static RECT GetWindowRect(IntPtr handle)
        {
            RECT dimensions;
            GetWindowRect(handle, out dimensions);
            return dimensions;
        }
    }
}
