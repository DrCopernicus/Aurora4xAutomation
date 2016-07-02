using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.UI
{
    public abstract class Window : IWindow
    {
        public IntPtr Handle { get; private set; }

        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Left { get; private set; }
        public int Right { get; private set; }
        
        private static IDictionary<IntPtr, string> GetOpenWindows()
        {
            var shellWindow = NativeMethods.GetShellWindow();
            var windows = new Dictionary<IntPtr, string>();

            NativeMethods.EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;

                int length = NativeMethods.GetWindowTextLength(hWnd);
                if (length == 0) return true;

                var builder = new StringBuilder(length);
                NativeMethods.GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        protected Window(string title)
        {
            var window = GetOpenWindows().FirstOrDefault(x => x.Value.StartsWith(title));

            if (window.Value == null)
            {
                OpenIfNotFound();
                Thread.Sleep(500);
            }

            window = GetOpenWindows().FirstOrDefault(x => x.Value.StartsWith(title));

            if (window.Value == null)
                throw new Exception(string.Format("{0} window not found!", title));

            var handle = window.Key;

            NativeMethods.RECT dimensions;
            NativeMethods.GetWindowRect(handle, out dimensions);

            Handle = handle;
            Left = dimensions.Left;
            Right = dimensions.Right;
            Top = dimensions.Top;
            Bottom = dimensions.Bottom;
        }

        protected abstract void OpenIfNotFound();
        
        public void MakeActive()
        {
            for (int i = 0; i < 12; i++)
            {
                NativeMethods.SetForegroundWindow(Handle);
                if (WaitActive())
                {
                    Screenshot.Dirty();
                    return;
                }
            }
            throw new Exception("Window never opened!");
        }

        private bool WaitActive(int ms = 500, int times = 20)
        {
            while (times > 0 && NativeMethods.GetForegroundWindow() != Handle)
            {
                Thread.Sleep(ms);
                times--;
            }
            return times > 0;
        }

        protected string GetWindowText()
        {
            var length = NativeMethods.GetWindowTextLength(Handle);
            var builder = new StringBuilder(length);
            NativeMethods.GetWindowText(Handle, builder, length + 1);

            return builder.ToString();
        }
    }
}
