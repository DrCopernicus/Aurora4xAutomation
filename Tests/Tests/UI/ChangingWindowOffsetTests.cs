using NSubstitute;
using NUnit.Framework;
using Server.IO;
using Server.IO.UI;
using Server.IO.UI.Display;
using Server.IO.UI.Windows;
using Server.Settings;
using System;

namespace Tests.Tests.UI
{
    [TestFixture]
    public class ChangingWindowOffsetTests
    {
        [Test]
        public void GetsCorrectWindowOffsetWithoutChanging()
        {
            var screen = Substitute.For<IScreen>();
            var windowFinder = Substitute.For<IWindowFinder>();
            windowFinder.GetWindowHandle(Arg.Any<string>()).Returns(IntPtr.Zero);
            windowFinder.GetDimensions(IntPtr.Zero).Returns(new NativeMethods.RECT{Bottom = 924, Left = 0, Right = 1251, Top = 0});

            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(0);
            settings.VerticalWindowOffset.Returns(0);

            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            Assert.AreEqual(924, window.Bottom);
            Assert.AreEqual(0, window.Left);
            Assert.AreEqual(1251, window.Right);
            Assert.AreEqual(0, window.Top);
        }

        [Test]
        public void ChangesWindowOffsetAfterInitializing()
        {
            var screen = Substitute.For<IScreen>();
            var windowFinder = Substitute.For<IWindowFinder>();
            windowFinder.GetWindowHandle(Arg.Any<string>()).Returns(IntPtr.Zero);
            windowFinder.GetDimensions(IntPtr.Zero).Returns(new NativeMethods.RECT { Bottom = 924, Left = 0, Right = 1251, Top = 0 });

            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(0);
            settings.VerticalWindowOffset.Returns(0);

            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            Assert.AreEqual(924, window.Bottom);
            Assert.AreEqual(0, window.Left);
            Assert.AreEqual(1251, window.Right);
            Assert.AreEqual(0, window.Top);

            settings.HorizontalWindowOffset.Returns(2);
            settings.VerticalWindowOffset.Returns(3);

            Assert.AreEqual(927, window.Bottom);
            Assert.AreEqual(2, window.Left);
            Assert.AreEqual(1253, window.Right);
            Assert.AreEqual(3, window.Top);
        }

        [Test]
        public void InitializesWithNonZeroOffsetAndChangesLater()
        {
            var screen = Substitute.For<IScreen>();
            var windowFinder = Substitute.For<IWindowFinder>();
            windowFinder.GetWindowHandle(Arg.Any<string>()).Returns(IntPtr.Zero);
            windowFinder.GetDimensions(IntPtr.Zero).Returns(new NativeMethods.RECT { Bottom = 924, Left = 0, Right = 1251, Top = 0 });

            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(1);
            settings.VerticalWindowOffset.Returns(2);

            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            Assert.AreEqual(926, window.Bottom);
            Assert.AreEqual(1, window.Left);
            Assert.AreEqual(1252, window.Right);
            Assert.AreEqual(2, window.Top);

            settings.HorizontalWindowOffset.Returns(2);
            settings.VerticalWindowOffset.Returns(3);

            Assert.AreEqual(927, window.Bottom);
            Assert.AreEqual(2, window.Left);
            Assert.AreEqual(1253, window.Right);
            Assert.AreEqual(3, window.Top);
        }
        
        [Test]
        public void ChangesControlOffset()
        {
            var screen = Substitute.For<IScreen>();
            var windowFinder = Substitute.For<IWindowFinder>();
            windowFinder.GetWindowHandle(Arg.Any<string>()).Returns(IntPtr.Zero);
            windowFinder.GetDimensions(IntPtr.Zero).Returns(new NativeMethods.RECT { Bottom = 924, Left = 0, Right = 1251, Top = 0 });

            var inputDevice = Substitute.For<IInputDevice>();
            var ocr = Substitute.For<IOCRReader>();
            var settings = Substitute.For<ISettingsStore>();
            settings.HorizontalWindowOffset.Returns(0);
            settings.VerticalWindowOffset.Returns(0);

            var window = new PopulationAndProductionWindow(screen, windowFinder, inputDevice, ocr, settings);

            Assert.AreEqual(613, window.ResearchTable.Bottom);
            Assert.AreEqual(406, window.ResearchTable.Left);
            Assert.AreEqual(776, window.ResearchTable.Right);
            Assert.AreEqual(406, window.ResearchTable.Top);

            settings.HorizontalWindowOffset.Returns(5);
            settings.VerticalWindowOffset.Returns(5);

            Assert.AreEqual(618, window.ResearchTable.Bottom);
            Assert.AreEqual(411, window.ResearchTable.Left);
            Assert.AreEqual(781, window.ResearchTable.Right);
            Assert.AreEqual(411, window.ResearchTable.Top);
        }
    }
}
