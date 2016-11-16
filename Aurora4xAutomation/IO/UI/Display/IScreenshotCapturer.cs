using System.Drawing;

namespace Aurora4xAutomation.IO.UI.Display
{
    public interface IScreenshotCapturer
    {
        Bitmap TakeScreenshot();
    }
}