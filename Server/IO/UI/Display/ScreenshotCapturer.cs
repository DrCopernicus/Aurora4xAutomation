using System.Drawing;
using Pranas;

namespace Server.IO.UI.Display
{
    public class ScreenshotCapturer : IScreenshotCapturer
    {
        public Bitmap TakeScreenshot()
        {
            return new Bitmap(ScreenshotCapture.TakeScreenshot());
        }
    }
}