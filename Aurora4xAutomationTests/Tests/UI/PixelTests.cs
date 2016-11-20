using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using Aurora4xAutomation.IO.UI.Display;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Settings;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Drawing;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class PixelTests
    {
        private void AssertPixelsOnControlAreCorrect(IScreenObject control)
        {
            Assert.AreEqual(Color.LightBlue, control.GetPixel(0, 0));
            Assert.AreEqual(Color.LightGreen, control.GetPixel(0, 2));
            Assert.AreEqual(Color.DarkGreen, control.GetPixel(2, 2));
            Assert.AreEqual(Color.DarkBlue, control.GetPixel(2, 0));
            Assert.AreEqual(Color.Red, control.GetPixel(1, 1));
        }

        private void AssertGettingOutOfBoundsPixelsThrows(IScreenObject control)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => control.GetPixel(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.GetPixel(-1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.GetPixel(0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.GetPixel(-5, -100));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.GetPixel(0, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.GetPixel(10, 49));
        }

        private IScreen GetMultiColoredScreen()
        {
            Color[][] display = {
                new []{Color.Black, Color.Black, Color.Black, Color.Black, Color.Black},
                new []{Color.Black, Color.LightBlue, Color.LightCoral, Color.LightGreen, Color.Black},
                new []{Color.Black, Color.Blue, Color.Red, Color.Green, Color.Black},
                new []{Color.Black, Color.DarkBlue, Color.DarkRed, Color.DarkGreen, Color.Black},
                new []{Color.Black, Color.Black, Color.Black, Color.Black, Color.Black}
            };

            var screen = Substitute.For<IScreen>();
            screen.GetPixel(Arg.Any<int>(), Arg.Any<int>()).Returns(args => display[(int)args[0]][(int)args[1]]);
            return screen;
        }

        private IWindowFinder GetWindowFinder()
        {
            var windowFinder = Substitute.For<IWindowFinder>();
            windowFinder.GetDimensions(Arg.Any<IntPtr>()).Returns(new NativeMethods.RECT { Left = 1, Bottom = 3, Right = 3, Top = 1 });
            return windowFinder;
        }

        [Test]
        public void PointsOnGenericControlReturnCorrectValues()
        {
            var control = new Control(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(control);
            AssertGettingOutOfBoundsPixelsThrows(control);
        }

        [Test]
        public void PointsOnButtonReturnCorrectValues()
        {
            var button = new Button(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(button);
            AssertGettingOutOfBoundsPixelsThrows(button);
        }

        [Test]
        public void PointsOnComboboxAreCorrectColors()
        {
            var combobox = new Combobox(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(combobox);
            AssertGettingOutOfBoundsPixelsThrows(combobox);
        }

        [Test]
        public void PointsOnDatagridAreCorrectColors()
        {
            var datagrid = new Datagrid(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(datagrid);
            AssertGettingOutOfBoundsPixelsThrows(datagrid);
        }

        [Test]
        public void PointsOnLabelAreCorrectColors()
        {
            var label = new Label(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(label);
            AssertGettingOutOfBoundsPixelsThrows(label);
        }

        [Test]
        public void PointsOnRadioButtonAreCorrectColors()
        {
            var radiobutton = new RadioButton(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(radiobutton);
            AssertGettingOutOfBoundsPixelsThrows(radiobutton);
        }

        [Test]
        public void PointsOnTextboxAreCorrectColors()
        {
            var textbox = new Textbox(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(textbox);
            AssertGettingOutOfBoundsPixelsThrows(textbox);
        }

        [Test]
        public void PointsOnTreeListAreCorrectColors()
        {
            var treelist = new TreeList(GetMultiColoredScreen(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(treelist);
            AssertGettingOutOfBoundsPixelsThrows(treelist);
        }

        [Test]
        public void PointsOnBaseAuroraWindowAreCorrectColors()
        {
            var window = new BaseAuroraWindow(GetMultiColoredScreen(), GetWindowFinder(), Substitute.For<IInputDevice>(), Substitute.For<ISettingsStore>());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnCommandersWindowAreCorrectColors()
        {
            var window = new CommandersWindow(GetMultiColoredScreen(), GetWindowFinder(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), Substitute.For<ISettingsStore>());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnConsoleWindowAreCorrectColors()
        {
            var window = new ConsoleWindow(GetMultiColoredScreen(), GetWindowFinder(), Substitute.For<IInputDevice>(), Substitute.For<ISettingsStore>());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnEventWindowAreCorrectColors()
        {
            var window = new EventWindow(GetMultiColoredScreen(), GetWindowFinder(), Substitute.For<IInputDevice>(), Substitute.For<ISettingsStore>());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnPopulationAndProductionWindowAreCorrectColors()
        {
            var window = new PopulationAndProductionWindow(GetMultiColoredScreen(), GetWindowFinder(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), Substitute.For<ISettingsStore>());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnSystemMapWindowAreCorrectColors()
        {
            var window = new SystemMapWindow(GetMultiColoredScreen(), GetWindowFinder(), Substitute.For<IInputDevice>(), Substitute.For<ISettingsStore>());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnTaskGroupsWindowAreCorrectColors()
        {
            var window = new TaskGroupsWindow(GetMultiColoredScreen(), GetWindowFinder(), Substitute.For<IInputDevice>(), Substitute.For<ISettingsStore>());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }
    }
}
