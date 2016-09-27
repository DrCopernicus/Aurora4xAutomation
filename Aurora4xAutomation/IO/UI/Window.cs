using Aurora4xAutomation.Common;
using Aurora4xAutomation.Common.Exceptions;
using Aurora4xAutomation.Settings;
using System;
using System.Drawing;
using System.Text;

namespace Aurora4xAutomation.IO.UI
{
    public abstract class Window : IScreenObject, IWindow
    {
        public IntPtr Handle { get; private set; }

        protected Window(string title, IScreen screen, IWindowFinder windowFinder, ISettingsStore settings)
        {
            Settings = settings;
            Screen = screen;

            IntPtr handle;

            try
            {
                handle = windowFinder.GetWindowHandle(title);
            }
            catch (WindowNotFoundException)
            {
                OpenIfNotFound();
                handle = windowFinder.GetWindowHandle(title);
            }

            var dimensions = windowFinder.GetDimensions(handle);

            Handle = handle;
            Left = dimensions.Left;
            Right = dimensions.Right;
            Top = dimensions.Top;
            Bottom = dimensions.Bottom;
        }

        protected abstract void OpenIfNotFound();
        
        public void MakeActive()
        {
            for (var i = 0; i < 12; i++)
            {
                NativeMethods.SetForegroundWindow(Handle);
                if (!WaitActive())
                    continue;
                Screenshot.Dirty();
                Sleeper.Sleep(500);
                return;
            }
            throw new Exception("Window never opened!");
        }

        private bool WaitActive(int ms = 500, int times = 20)
        {
            while (times > 0 && NativeMethods.GetForegroundWindow() != Handle)
            {
                Sleeper.Sleep(ms);
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

        protected ISettingsStore Settings { get; set; }
        protected IWindowFinder WindowFinder { get; set; }
        public IScreen Screen { get; private set; }
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

        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Left { get; private set; }
        public int Right { get; private set; }
    }
}
