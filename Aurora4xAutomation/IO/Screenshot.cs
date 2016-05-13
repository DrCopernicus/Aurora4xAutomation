using System.Drawing;
using Aurora4xAutomation.Common;
using Pranas;

namespace Aurora4xAutomation.IO
{
    public static class Screenshot
    {
        public static void Dirty()
        {
            _dirty = true;
        }

        private static bool _dirty = true;

        public static Bitmap Latest
        {
            get
            {
                if (_dirty)
                {
                    if (_latest != null)
                        _latest.Dispose();
                    Sleeper.Sleep(500);
                    _latest = new Bitmap(ScreenshotCapture.TakeScreenshot());
                    Sleeper.Sleep(250);
                    _dirty = false;
                }
                return _latest;
            }
        }

        private static Bitmap _latest;
    }
}
