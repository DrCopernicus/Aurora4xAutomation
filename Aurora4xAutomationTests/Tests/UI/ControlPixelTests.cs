using System;
using System.Drawing;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using NUnit.Framework;

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

        [Test]
        public void PointsOnButtonAreCorrectColors()
        {
            var screen = new MultiColoredScreen();
            var button = new Button(screen, 1, 3, 1, 3);

            Assert.AreEqual(Color.LightBlue, button.GetPixel(0, 0));
            Assert.AreEqual(Color.LightGreen, button.GetPixel(0, 2));
            Assert.AreEqual(Color.DarkGreen, button.GetPixel(2, 2));
            Assert.AreEqual(Color.DarkBlue, button.GetPixel(2, 0));
        }

        [Test]
        public void PointsOutsideOfButtonThrow()
        {
            var screen = new MultiColoredScreen();
            var button = new Button(screen, 1, 3, 1, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() => button.GetPixel(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => button.GetPixel(-1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => button.GetPixel(0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => button.GetPixel(-5, -100));
            Assert.Throws<ArgumentOutOfRangeException>(() => button.GetPixel(0, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => button.GetPixel(10, 49));
        }
    }
}
