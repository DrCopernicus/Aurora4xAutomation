using System.Drawing;
using Aurora4xAutomation.Common;
using Pranas;

namespace Aurora4xAutomation.IO.UI.Display
{
    public class ScreenDataRetriever : IScreenDataRetriever
    {
        public Color GetPixel(int x, int y)
        {
            return CurrentScreen.GetPixel(x, y);
        }

        public void Dirty()
        {
            _dirty = true;
        }

        private bool _dirty = true;

        public Bitmap CurrentScreen
        {
            get
            {
                if (!_dirty)
                    return _currentScreen;

                if (_currentScreen != null)
                    _currentScreen.Dispose();

                Sleeper.Sleep(500);
                _currentScreen = new Bitmap(ScreenshotCapture.TakeScreenshot());
                Sleeper.Sleep(250);

                _dirty = false;

                return _currentScreen;
            }
        }

        private Bitmap _currentScreen;
    }
}
