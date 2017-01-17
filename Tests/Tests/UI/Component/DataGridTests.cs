using NSubstitute;
using NUnit.Framework;
using Server.Common;
using Server.IO.OCR;
using Server.IO.UI;
using Server.IO.UI.Controls;
using Server.IO.UI.Display;
using Server.IO.UI.Windows;
using Server.Settings;
using System;
using Tests.ScriptFramework;

namespace Tests.Tests.UI.Component
{
    [TestFixture]
    public class DataGridTests
    {
        private class ResearchTableInputDevice : HijackableInputDevice
        {
            public ResearchTableInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.datagrid_researchTable);
            }
        }

        private class ResearchTableInWindowInputDevice : HijackableInputDevice
        {
            public ResearchTableInWindowInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.datagrid_researchTableInWindow);
            }
        }

        private class ActualResearchTableInputDevice : HijackableInputDevice
        {
            public ActualResearchTableInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.datagrid_researchTableActual);
            }
        }

        private class ActualResearchTableOnScreenInputDevice : HijackableInputDevice
        {
            public ActualResearchTableOnScreenInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.datagrid_researchTableInWindowOnScreen);
            }
        }

        [Test]
        public void ReadsResearchTable()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new ResearchTableInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocr = new OCRReader(new OCRSplitter());

            var datagrid = new Datagrid(screen, inputDevice, ocr, 1, 48, new[] { 1, 282, 348 })
            {
                LineHeight = 16,
                TopOfCharactersOffset = 3
            };

            var table = datagrid.GetTable();

            Assert.AreEqual(3, table.Count);

            Assert.AreEqual("GenomeSequenceResearch", table[0][0]);
            Assert.AreEqual("TerraformingModule", table[1][0]);
            Assert.AreEqual("TerraformingRate0.0012atm", table[2][0]);

            Assert.AreEqual("5000", table[0][1]);
            Assert.AreEqual("5000", table[1][1]);
            Assert.AreEqual("3000", table[2][1]);
        }

        [Test]
        public void ReadsResearchTableInWindow()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new ResearchTableInWindowInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var window = new Control(screen, inputDevice, 5, 1000, 5, 1000);
            var ocr = new OCRReader(new OCRSplitter());

            var datagrid = new Datagrid(window, inputDevice, ocr, 521, 568, new[] { 370, 652, 718 })
            {
                LineHeight = 16,
                TopOfCharactersOffset = 3
            };

            var table = datagrid.GetTable();

            Assert.AreEqual(3, table.Count);

            Assert.AreEqual("GenomeSequenceResearch", table[0][0]);
            Assert.AreEqual("TerraformingModule", table[1][0]);
            Assert.AreEqual("TerraformingRate0.0012atm", table[2][0]);

            Assert.AreEqual("5000", table[0][1]);
            Assert.AreEqual("5000", table[1][1]);
            Assert.AreEqual("3000", table[2][1]);
        }

        [Test]
        public void ReadsActualResearchTable()
        {
            var windowFinder = Substitute.For<IWindowFinder>();
            windowFinder.GetWindowHandle(Arg.Any<string>()).Returns(IntPtr.Zero);
            windowFinder.GetDimensions(IntPtr.Zero).Returns(new NativeMethods.RECT { Bottom = 924, Left = 0, Right = 1251, Top = 0 });
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new ActualResearchTableInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocr = new OCRReader(new OCRSplitter());
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(5);
            settings.VerticalWindowOffset.Returns(5);
            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            var table = window.ResearchTable.GetTable();

            Assert.AreEqual(13, table.Count);

            Assert.AreEqual("GenomeSequenceResearch", table[0][0]);
            Assert.AreEqual("TerraformingModule", table[1][0]);
            Assert.AreEqual("TerraformingRate0.0012atm", table[2][0]);

            Assert.AreEqual("5000", table[0][1]);
            Assert.AreEqual("5000", table[1][1]);
            Assert.AreEqual("3000", table[2][1]);
        }

        [Test]
        public void ReadsActualResearchTableOnLargeScreen()
        {
            var windowFinder = Substitute.For<IWindowFinder>();
            windowFinder.GetWindowHandle(Arg.Any<string>()).Returns(IntPtr.Zero);
            windowFinder.GetDimensions(IntPtr.Zero).Returns(new NativeMethods.RECT { Bottom = 1224, Left = 500, Right = 1751, Top = 300 });
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new ActualResearchTableOnScreenInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocr = new OCRReader(new OCRSplitter());
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(5);
            settings.VerticalWindowOffset.Returns(5);
            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            var table = window.ResearchTable.GetTable();

            Assert.AreEqual(13, table.Count);

            Assert.AreEqual("GenomeSequenceResearch", table[0][0]);
            Assert.AreEqual("TerraformingModule", table[1][0]);
            Assert.AreEqual("TerraformingRate0.0012atm", table[2][0]);

            Assert.AreEqual("5000", table[0][1]);
            Assert.AreEqual("5000", table[1][1]);
            Assert.AreEqual("3000", table[2][1]);
        }
    }
}
