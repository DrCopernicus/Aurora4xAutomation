﻿using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Display;
using System;
using System.Drawing;
using WindowsInput.Native;

namespace Aurora4xAutomationTests.Tests.UI.Component
{
    public class HijackableScreenShotCapturer : IScreenshotCapturer
    {
        public Bitmap TakeScreenshot()
        {
            return CurrentScreen;
        }

        public Bitmap CurrentScreen { private get; set; }
    }

    public abstract class HijackableInputDevice : IInputDevice
    {
        protected HijackableScreenShotCapturer Screenshot { get; private set; }

        protected HijackableInputDevice(HijackableScreenShotCapturer screenshot)
        {
            Screenshot = screenshot;
        }

        public virtual void Click(int x, int y, int wait)
        {
            throw new Exception(string.Format("Illegally clicked at: <{0}, {1}>!", x, y));
        }

        public virtual void SendKeys(string text)
        {
            throw new Exception(string.Format("Illegally sent keys: <{0}>!", text));
        }

        public virtual void PressKey(VirtualKeyCode key)
        {
            throw new Exception(string.Format("Illegally pressed key: <{0}>!", key));
        }

        protected bool Within(int x, int y, int left, int top, int right, int bottom)
        {
            return x >= left && x <= right && y >= top && y <= bottom;
        }
    }
}