using Server.Common;
using Server.Common.Exceptions;
using Server.IO.UI.Display;
using Server.Settings;
using System;

namespace Server.IO.UI
{
    public abstract class Window : ScreenObject, IWindow
    {
        public IntPtr Handle { get; private set; }

        protected Window(string title, IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, ISettingsStore settings)
        {
            Parent = screen;
            Settings = settings;
            Screen = screen;
            InputDevice = inputDevice;
            WindowFinder = windowFinder;

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

            _relativeLeft = dimensions.Left;
            _relativeRight = dimensions.Right;
            _relativeBottom = dimensions.Bottom;
            _relativeTop = dimensions.Top;

            Handle = handle;
        }

        protected abstract void OpenIfNotFound();
        
        public void MakeActive()
        {
            for (var i = 0; i < 12; i++)
            {
                WindowFinder.SetForegroundWindow(Handle);
                if (!WaitActive())
                    continue;
                Screen.Dirty();
                StaticSleeper.Sleep(500);
                return;
            }
            throw new Exception("Window never opened!");
        }

        private bool WaitActive(int ms = 500, int times = 20)
        {
            while (times > 0 && WindowFinder.GetForegroundWindow() != Handle)
            {
                StaticSleeper.Sleep(ms);
                times--;
            }
            return times > 0;
        }

        protected string GetWindowText()
        {
            return WindowFinder.GetWindowText(Handle);
        }

        protected ISettingsStore Settings { get; set; }
        protected IWindowFinder WindowFinder { get; set; }
        
        public override int Top { get { return Parent.Top + _relativeTop + Settings.VerticalWindowOffset; } }
        public override int Bottom { get { return Parent.Top + _relativeBottom + Settings.VerticalWindowOffset; } }
        public override int Left { get { return Parent.Left + _relativeLeft + Settings.HorizontalWindowOffset; } }
        public override int Right { get { return Parent.Left + _relativeRight + Settings.HorizontalWindowOffset; } }
    }
}
