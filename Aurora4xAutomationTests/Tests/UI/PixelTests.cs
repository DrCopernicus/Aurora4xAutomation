using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Settings;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsInput.Native;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI.Display;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class PixelTests
    {
        private class MultiColoredScreen : IScreen
        {
            private Color[][] _screen = {
                new []{Color.Black, Color.Black, Color.Black, Color.Black, Color.Black},
                new []{Color.Black, Color.LightBlue, Color.LightCoral, Color.LightGreen, Color.Black},
                new []{Color.Black, Color.Blue, Color.Red, Color.Green, Color.Black},
                new []{Color.Black, Color.DarkBlue, Color.DarkRed, Color.DarkGreen, Color.Black},
                new []{Color.Black, Color.Black, Color.Black, Color.Black, Color.Black}
            };

            public void Dirty()
            {
                throw new NotImplementedException();
            }

            public Color GetPixel(int x, int y)
            {
                return _screen[x][y];
            }

            public byte[,] GetPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
            {
                throw new NotImplementedException();
            }

            public bool HasPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
            {
                throw new NotImplementedException();
            }

            public bool OnlyHasPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
            {
                throw new NotImplementedException();
            }
        }

        private class TestWindowFinder : IWindowFinder
        {
            public IntPtr GetWindowHandle(string title)
            {
                return IntPtr.Zero;
            }

            public NativeMethods.RECT GetDimensions(IntPtr handle)
            {
                return new NativeMethods.RECT { Left = 1, Bottom = 3, Right = 3, Top = 1 };
            }

            public void SetForegroundWindow(IntPtr handle)
            {
                throw new NotImplementedException();
            }

            public IntPtr GetForegroundWindow()
            {
                throw new NotImplementedException();
            }

            public string GetWindowText(IntPtr handle)
            {
                throw new NotImplementedException();
            }
        }

        private class TestInputDevice : IInputDevice
        {
            public void Click(int x, int y, int wait)
            {
                throw new NotImplementedException();
            }

            public void SendKeys(string text)
            {
                throw new NotImplementedException();
            }

            public void PressKey(VirtualKeyCode key)
            {
                throw new NotImplementedException();
            }
        }

        private class TestSettingsStore : ISettingsStore
        {
            public bool Stopped { get; set; }
            public bool AutoTurnsOn { get; set; }
            public string DatabaseLocation { get; private set; }
            public string DatabasePassword { get; private set; }
            public string EventLogLocation { get; private set; }
            public int RaceId { get; set; }
            public Dictionary<string, string> Research { get; set; }
            public Dictionary<string, Dictionary<string, string>> ResearchFocuses { get; private set; }
            public int GameId { get; set; }
            public IncrementLength Increment { get; set; }
            public string GameName { get; set; }
        }

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

        private class TestOCRReader : IOCRReader
        {
            public string ReadTableRow(byte[,] pixels, Dictionary<string, byte[,]> alphabet)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void PointsOnGenericControlReturnCorrectValues()
        {
            var control = new Control(new MultiColoredScreen(), new TestInputDevice(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(control);
            AssertGettingOutOfBoundsPixelsThrows(control);
        }

        [Test]
        public void PointsOnButtonReturnCorrectValues()
        {
            var button = new Button(new MultiColoredScreen(), new TestInputDevice(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(button);
            AssertGettingOutOfBoundsPixelsThrows(button);
        }

        [Test]
        public void PointsOnComboboxAreCorrectColors()
        {
            var combobox = new Combobox(new MultiColoredScreen(), new TestInputDevice(), new TestOCRReader(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(combobox);
            AssertGettingOutOfBoundsPixelsThrows(combobox);
        }

        [Test]
        public void PointsOnDatagridAreCorrectColors()
        {
            var datagrid = new Datagrid(new MultiColoredScreen(), new TestInputDevice(), new TestOCRReader(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(datagrid);
            AssertGettingOutOfBoundsPixelsThrows(datagrid);
        }

        [Test]
        public void PointsOnLabelAreCorrectColors()
        {
            var label = new Label(new MultiColoredScreen(), new TestInputDevice(), new TestOCRReader(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(label);
            AssertGettingOutOfBoundsPixelsThrows(label);
        }

        [Test]
        public void PointsOnRadioButtonAreCorrectColors()
        {
            var radiobutton = new RadioButton(new MultiColoredScreen(), new TestInputDevice(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(radiobutton);
            AssertGettingOutOfBoundsPixelsThrows(radiobutton);
        }

        [Test]
        public void PointsOnTextboxAreCorrectColors()
        {
            var textbox = new Textbox(new MultiColoredScreen(), new TestInputDevice(), new TestOCRReader(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(textbox);
            AssertGettingOutOfBoundsPixelsThrows(textbox);
        }

        [Test]
        public void PointsOnTreeListAreCorrectColors()
        {
            var treelist = new TreeList(new MultiColoredScreen(), new TestInputDevice(), new TestOCRReader(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(treelist);
            AssertGettingOutOfBoundsPixelsThrows(treelist);
        }

        [Test]
        public void PointsOnBaseAuroraWindowAreCorrectColors()
        {
            var window = new BaseAuroraWindow(new MultiColoredScreen(), new TestWindowFinder(), new TestInputDevice(), new TestSettingsStore());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnCommandersWindowAreCorrectColors()
        {
            var window = new CommandersWindow(new MultiColoredScreen(), new TestWindowFinder(), new TestInputDevice(), new TestOCRReader(), new TestSettingsStore());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnConsoleWindowAreCorrectColors()
        {
            var window = new ConsoleWindow(new MultiColoredScreen(), new TestWindowFinder(), new TestInputDevice(), new TestSettingsStore());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnEventWindowAreCorrectColors()
        {
            var window = new EventWindow(new MultiColoredScreen(), new TestWindowFinder(), new TestInputDevice(), new TestSettingsStore());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnPopulationAndProductionWindowAreCorrectColors()
        {
            var window = new PopulationAndProductionWindow(new MultiColoredScreen(), new TestWindowFinder(), new TestInputDevice(), new TestOCRReader(), new TestSettingsStore());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnSystemMapWindowAreCorrectColors()
        {
            var window = new SystemMapWindow(new MultiColoredScreen(), new TestWindowFinder(), new TestInputDevice(), new TestSettingsStore());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }

        [Test]
        public void PointsOnTaskGroupsWindowAreCorrectColors()
        {
            var window = new TaskGroupsWindow(new MultiColoredScreen(), new TestWindowFinder(), new TestInputDevice(),  new TestSettingsStore());
            AssertPixelsOnControlAreCorrect(window);
            AssertGettingOutOfBoundsPixelsThrows(window);
        }
    }
}
