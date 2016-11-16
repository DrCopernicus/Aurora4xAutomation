using Aurora4xAutomation.Common;
using System.Drawing;

namespace Aurora4xAutomation.IO.UI.Display
{
    public class ScreenDataRetriever : IScreenDataRetriever
    {
        private ISleeper Sleeper { get; set; }
        private IScreenshotCapturer ScreenshotCapturer { get; set; }
        private bool _dirty = true;
        private Bitmap _currentScreen;

        public ScreenDataRetriever(ISleeper sleeper, IScreenshotCapturer screenshotCapturer)
        {
            Sleeper = sleeper;
            ScreenshotCapturer = screenshotCapturer;
        }

        public Color GetPixel(int x, int y)
        {
            return Latest.GetPixel(x, y);
        }

        public void Dirty()
        {
            _dirty = true;
        }

        public Bitmap Latest
        {
            get
            {
                if (!_dirty)
                    return _currentScreen;

                if (_currentScreen != null)
                    _currentScreen.Dispose();

                Sleeper.Sleep(500);
                _currentScreen = new Bitmap(ScreenshotCapturer.TakeScreenshot());
                Sleeper.Sleep(250);

                _dirty = false;

                return _currentScreen;
            }
        }
    }
}
