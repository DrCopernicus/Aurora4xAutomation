using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Display;
using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsInput.Native;

namespace Aurora4xAutomationTests.ScriptFramework
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
        protected List<ScriptableState> States { get; private set; }

        protected HijackableInputDevice(HijackableScreenShotCapturer screenshot)
        {
            Screenshot = screenshot;
            States = new List<ScriptableState>();
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

        protected bool Within(int x, int y, int top, int bottom, int left, int right)
        {
            return x >= left && x <= right && y >= top && y <= bottom;
        }

        protected void SetScreen(Bitmap screen)
        {
            Screenshot.CurrentScreen = screen;
        }
    }
}
