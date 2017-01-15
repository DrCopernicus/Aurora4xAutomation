using System.Drawing;

namespace Server.IO.UI.Display
{
    public interface IScreenshotCapturer
    {
        Bitmap TakeScreenshot();
    }
}