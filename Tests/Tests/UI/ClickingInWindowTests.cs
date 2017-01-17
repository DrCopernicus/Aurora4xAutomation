using NSubstitute;
using NUnit.Framework;
using Server.IO;
using Server.IO.UI;
using Server.IO.UI.Display;
using Server.IO.UI.Windows;
using Server.Settings;
using System.Drawing;

namespace Tests.Tests.UI
{
    [TestFixture]
    public class ClickingInWindowTests
    {
        [Test]
        public void ClicksButtonInWindowWithoutOffsets()
        {
            var screen = Substitute.For<IScreen>();
            var windowFinder = Substitute.For<IWindowFinder>();
            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(0);
            settings.VerticalWindowOffset.Returns(0);
            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            window.CreateIndustrialProject.Click();
            inputDevice.Received(1).Click(671, 742, Arg.Any<int>());
        }

        [Test]
        public void ClicksButtonInWindowWithPositiveOffsets()
        {
            var screen = Substitute.For<IScreen>();
            var windowFinder = Substitute.For<IWindowFinder>();
            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(5);
            settings.VerticalWindowOffset.Returns(5);
            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            window.CreateIndustrialProject.Click();
            inputDevice.Received(1).Click(676, 747, Arg.Any<int>());
        }

        [Test]
        public void ClicksButtonInWindowWithNegativeOffsets()
        {
            var screen = Substitute.For<IScreen>();
            var windowFinder = Substitute.For<IWindowFinder>();
            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(-5);
            settings.VerticalWindowOffset.Returns(-5);
            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            window.CreateIndustrialProject.Click();
            inputDevice.Received(1).Click(666, 737, Arg.Any<int>());
        }

        [Test]
        public void ClicksCheckBoxInWindowWithoutOffsets()
        {
            var screen = Substitute.For<IScreen>();
            screen.GetPixel(Arg.Any<int>(), Arg.Any<int>()).Returns(Color.White);
            var windowFinder = Substitute.For<IWindowFinder>();
            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(0);
            settings.VerticalWindowOffset.Returns(0);
            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            window.MatchingScientistsOnly.Select();
            inputDevice.Received(1).Click(745, 367, Arg.Any<int>());
        }

        [Test]
        public void ClicksCheckBoxInWindowWithPositiveOffsets()
        {
            var screen = Substitute.For<IScreen>();
            screen.GetPixel(Arg.Any<int>(), Arg.Any<int>()).Returns(Color.White);
            var windowFinder = Substitute.For<IWindowFinder>();
            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(5);
            settings.VerticalWindowOffset.Returns(5);
            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            window.MatchingScientistsOnly.Select();
            inputDevice.Received(1).Click(750, 372, Arg.Any<int>());
        }
    }
}
