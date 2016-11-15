using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsInput.Native;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class ClickingTests
    {
        private class TestScreen : IScreen
        {
            public void Dirty()
            {
                throw new NotImplementedException();
            }

            public Color GetPixel(int x, int y)
            {
                throw new NotImplementedException();
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

        private class TestInputDevice : IInputDevice
        {
            public int[] LastClickedSpot { get; private set; }

            public void Click(int x, int y, int wait)
            {
                LastClickedSpot = new[] {x, y};
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

        private class TestOCRReader : IOCRReader
        {
            public string ReadTableRow(byte[,] pixels, Dictionary<string, byte[,]> alphabet)
            {
                throw new NotImplementedException();
            }
        }

        private void AssertClicksAreCorrect(IScreenObject control, TestInputDevice inputDevice)
        {
            control.Click(0, 0, 0);
            Assert.AreEqual(new[] { 1, 1 }, inputDevice.LastClickedSpot);
            control.Click(2, 2, 0);
            Assert.AreEqual(new[] { 3, 3 }, inputDevice.LastClickedSpot);
            control.Click();
            Assert.AreEqual(new[] { 2, 2 }, inputDevice.LastClickedSpot);
            control.Click(-1, -55, 0);
            Assert.AreEqual(new[] { 0, -54 }, inputDevice.LastClickedSpot);
            control.Click(-100, 2, 0);
            Assert.AreEqual(new[] { -99, 3 }, inputDevice.LastClickedSpot);
            control.Click(3, 4, 0);
            Assert.AreEqual(new[] { 4, 5 }, inputDevice.LastClickedSpot);
            control.Click(1, 4, 0);
            Assert.AreEqual(new[] { 2, 5 }, inputDevice.LastClickedSpot);
        }

        [Test]
        public void ClicksCorrectPixelOnGenericControl()
        {
            var inputDevice = new TestInputDevice();
            var control = new Control(new TestScreen(), inputDevice, 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnButton()
        {
            var inputDevice = new TestInputDevice();
            var control = new Button(new TestScreen(), inputDevice, 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnCombobox()
        {
            var inputDevice = new TestInputDevice();
            var control = new Combobox(new TestScreen(), inputDevice, new TestOCRReader(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnDatagrid()
        {
            var inputDevice = new TestInputDevice();
            var control = new Datagrid(new TestScreen(), inputDevice, new TestOCRReader(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnLabel()
        {
            var inputDevice = new TestInputDevice();
            var control = new Label(new TestScreen(), inputDevice, new TestOCRReader(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnRadioButton()
        {
            var inputDevice = new TestInputDevice();
            var control = new RadioButton(new TestScreen(), inputDevice, 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnTextbox()
        {
            var inputDevice = new TestInputDevice();
            var control = new Textbox(new TestScreen(), inputDevice, new TestOCRReader(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnTreeList()
        {
            var inputDevice = new TestInputDevice();
            var control = new TreeList(new TestScreen(), inputDevice, new TestOCRReader(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }
    }
}
