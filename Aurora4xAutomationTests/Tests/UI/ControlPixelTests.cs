using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using NUnit.Framework;
using System;
using System.Drawing;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class ControlPixelTests
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

            public Color GetPixel(int x, int y)
            {
                return _screen[x][y];
            }
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

        [Test]
        public void PointsOnGenericControlReturnCorrectValues()
        {
            var control = new Control(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(control);
            AssertGettingOutOfBoundsPixelsThrows(control);
        }

        [Test]
        public void PointsOnButtonReturnCorrectValues()
        {
            var button = new Button(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(button);
            AssertGettingOutOfBoundsPixelsThrows(button);
        }

        [Test]
        public void PointsOnComboboxAreCorrectColors()
        {
            var combobox = new Combobox(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(combobox);
            AssertGettingOutOfBoundsPixelsThrows(combobox);
        }

        [Test]
        public void PointsOnDatagridAreCorrectColors()
        {
            var datagrid = new Datagrid(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(datagrid);
            AssertGettingOutOfBoundsPixelsThrows(datagrid);
        }

        [Test]
        public void PointsOnLabelAreCorrectColors()
        {
            var label = new Label(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(label);
            AssertGettingOutOfBoundsPixelsThrows(label);
        }

        [Test]
        public void PointsOnRadioButtonAreCorrectColors()
        {
            var radiobutton = new RadioButton(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(radiobutton);
            AssertGettingOutOfBoundsPixelsThrows(radiobutton);
        }

        [Test]
        public void PointsOnTextboxAreCorrectColors()
        {
            var textbox = new Textbox(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(textbox);
            AssertGettingOutOfBoundsPixelsThrows(textbox);
        }

        [Test]
        public void PointsOnTreeListAreCorrectColors()
        {
            var treelist = new TreeList(new MultiColoredScreen(), 1, 3, 1, 3);
            AssertPixelsOnControlAreCorrect(treelist);
            AssertGettingOutOfBoundsPixelsThrows(treelist);
        }
    }
}
