using NSubstitute;
using NUnit.Framework;
using Server.Common;
using Server.IO.OCR;
using Server.IO.UI.Controls;
using Server.IO.UI.Display;
using Tests.ScriptFramework;

namespace Tests.Tests.UI.Component
{
    [TestFixture]
    public class LabelTests
    {
        private class CivilianTabInputDevice : HijackableInputDevice
        {
            public CivilianTabInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.window_civiliantab_supply);
            }
        }

        [Test]
        public void ReadsInstallationTypeLabelShrunkWrapToText()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var label = new Label(screen, inputDevice, ocrReader, 183, 193, 412, 488);
            Assert.AreEqual("lnstallationType", label.Text);
        }

        [Test]
        public void ReadsInstallationTypeLabelWithExtraSpaceBelow()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var label = new Label(screen, inputDevice, ocrReader, 183, 200, 412, 488);
            Assert.AreEqual("lnstallationType", label.Text);
        }

        [Test]
        public void ReadsInstallationTypeLabelWithExtraSpaceOnSides()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var label = new Label(screen, inputDevice, ocrReader, 183, 193, 406, 495);
            Assert.AreEqual("lnstallationType", label.Text);
        }

        [Test]
        public void ReadsInstallationTypeLabelWithExtraSpaceOnSidesAndBelow()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var label = new Label(screen, inputDevice, ocrReader, 183, 200, 406, 495);
            Assert.AreEqual("lnstallationType", label.Text);
        }
    }
}
