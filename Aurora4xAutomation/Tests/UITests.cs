using System;
using Aurora4xAutomation.UI;
using Aurora4xAutomation.UI.Controls;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class UITests
    {
        private class WindowDouble : IWindow
        {
            public int Top { get; private set; }
            public int Bottom { get; private set; }
            public int Left { get; private set; }
            public int Right { get; private set; }
            public IntPtr Handle { get; private set; }
            public Window.RECT Dimensions { get; private set; }

            public void MakeActive()
            {

            }

            public WindowDouble(int top, int bottom, int left, int right)
            {
                Top = top;
                Bottom = bottom;
                Left = left;
                Right = right;
            }
        }

        [Test]
        public void TestGenericControlCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new Control(window, 10, 30, 10, 50);

            Assert.AreEqual(20, control.Top);
            Assert.AreEqual(40, control.Bottom);
            Assert.AreEqual(20, control.Left);
            Assert.AreEqual(60, control.Right);
        }

        [Test]
        public void TestButtonCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var button = new Button(window, 20, 40, 20, 60);

            Assert.AreEqual(30, button.Top);
            Assert.AreEqual(50, button.Bottom);
            Assert.AreEqual(30, button.Left);
            Assert.AreEqual(70, button.Right);
        }
    }
}
