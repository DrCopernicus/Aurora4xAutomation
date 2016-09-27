﻿using Aurora4xAutomation.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora4xAutomation.IO.UI
{
    public class WindowFinder : IWindowFinder
    {
        private IDictionary<IntPtr, string> GetOpenWindows()
        {
            var shellWindow = NativeMethods.GetShellWindow();
            var windows = new Dictionary<IntPtr, string>();

            NativeMethods.EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;

                var length = NativeMethods.GetWindowTextLength(hWnd);
                if (length == 0) return true;

                var builder = new StringBuilder(length);
                NativeMethods.GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;
            }, 0);

            return windows;
        }

        public IntPtr GetWindowHandle(string title)
        {
            var window = GetOpenWindows().FirstOrDefault(x => x.Value.StartsWith(title));

            if (window.Value == null)
                throw new WindowNotFoundException(title);

            return window.Key;
        }

        public NativeMethods.RECT GetDimensions(IntPtr handle)
        {
            return NativeMethods.GetWindowRect(handle);
        }
    }
}
