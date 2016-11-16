using Pranas;
using System.Drawing;

namespace Aurora4xAutomation.IO.UI.Display
{
    public class ScreenshotCapturer : IScreenshotCapturer
    {
        public Bitmap TakeScreenshot()
        {
            return new Bitmap(ScreenshotCapture.TakeScreenshot());
        }
    }
}