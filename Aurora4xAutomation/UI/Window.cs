﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;

namespace Aurora4xAutomation.UI
{
    public class Window
    {
        protected static readonly InputSimulator Input = new InputSimulator();

        public IntPtr Handle { get; private set; }
        public RECT Dimensions { get; private set; }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

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

        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll", EntryPoint = "PostMessage", CallingConvention = CallingConvention.Winapi)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        private uint MakeDWORD(ushort hiWord, ushort loWord)
        {
            return ((((uint)hiWord) << 16) | loWord);
        }

        private static IDictionary<IntPtr, string> GetOpenWindows()
        {
            var shellWindow = GetShellWindow();
            var windows = new Dictionary<IntPtr, string>();

            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                var builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        public Window(string title)
        {
            var handle = GetOpenWindows().First(x => x.Value.StartsWith(title)).Key;
            RECT dimensions;
            GetWindowRect(handle, out dimensions);

            Handle = handle;
            Dimensions = dimensions;
        }

        protected void Click(int x, int y)
        {
            Cursor.Position = new Point(Dimensions.Left + x, Dimensions.Top + y);
            Thread.Sleep(250);
            Input.Mouse.LeftButtonClick();
        }

        public void MakeActive()
        {
            for (int i = 0; i < 12; i++)
            {
                SetForegroundWindow(Handle);
                if (WaitActive())
                    return;
            }
            throw new Exception("Window never opened!");
        }

        public Color GetPixel(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, Dimensions.Left + x, Dimensions.Top + y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        private bool WaitActive(int ms = 500, int times = 20)
        {
            while (times > 0 && GetForegroundWindow() != Handle)
            {
                Thread.Sleep(ms);
                times--;
            }
            return times > 0;
        }

        protected string GetWindowText()
        {
            var length = GetWindowTextLength(Handle);
            var builder = new StringBuilder(length);
            GetWindowText(Handle, builder, length + 1);

            return builder.ToString();
        }
    }
}
