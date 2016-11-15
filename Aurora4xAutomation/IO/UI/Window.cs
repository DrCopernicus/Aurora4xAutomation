﻿using Aurora4xAutomation.Common;
using Aurora4xAutomation.Common.Exceptions;
using Aurora4xAutomation.Settings;
using System;

namespace Aurora4xAutomation.IO.UI
{
    public abstract class Window : ScreenObject, IWindow
    {
        public IntPtr Handle { get; private set; }

        protected Window(string title, IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, ISettingsStore settings)
        {
            Settings = settings;
            Screen = screen;
            InputDevice = inputDevice;

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
                WindowFinder.SetForegroundWindow(Handle);
                if (!WaitActive())
                    continue;
                Screen.Dirty();
                Sleeper.Sleep(500);
                return;
            }
            throw new Exception("Window never opened!");
        }

        private bool WaitActive(int ms = 500, int times = 20)
        {
            while (times > 0 && WindowFinder.GetForegroundWindow() != Handle)
            {
                Sleeper.Sleep(ms);
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
    }
}
