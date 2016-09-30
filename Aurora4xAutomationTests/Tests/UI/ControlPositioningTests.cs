using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsInput.Native;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class ControlPositioningTests
    {
        private class WindowDouble : IScreenObject
        {
            public void PressKeys(string text)
            {
                throw new System.NotImplementedException();
            }

            public IScreen Screen { get; private set; }
            public IInputDevice InputDevice { get; private set; }

            public Color GetPixel(int x, int y)
            {
                throw new System.NotImplementedException();
            }

            public void Click()
            {
                throw new System.NotImplementedException();
            }

            public void Click(int x, int y, int wait)
            {
                throw new System.NotImplementedException();
            }

            public void PressKey(VirtualKeyCode key)
            {
                throw new System.NotImplementedException();
            }

            public int Top { get; private set; }
            public int Bottom { get; private set; }
            public int Left { get; private set; }
            public int Right { get; private set; }

            public WindowDouble(int top, int bottom, int left, int right)
            {
                Top = top;
                Bottom = bottom;
                Left = left;
                Right = right;
            }
        }

        private class TestInputDevice : IInputDevice
        {
            public void Click(int x, int y, int wait)
            {
                throw new System.NotImplementedException();
            }

            public void SendKeys(string text)
            {
                throw new System.NotImplementedException();
            }

            public void PressKey(VirtualKeyCode key)
            {
                throw new System.NotImplementedException();
            }
        }

        private class TestOCRReader : IOCRReader
        {
            public string ReadTableRow(byte[,] pixels, Dictionary<string, byte[,]> alphabet)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestGenericControlCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new Control(window, new TestInputDevice(), 10, 30, 10, 50);

            Assert.AreEqual(20, control.Top);
            Assert.AreEqual(40, control.Bottom);
            Assert.AreEqual(20, control.Left);
            Assert.AreEqual(60, control.Right);
        }

        [Test]
        public void TestButtonCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new Button(window, new TestInputDevice(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestComboboxCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new Combobox(window, new TestInputDevice(), new TestOCRReader(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestDataGridCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new Datagrid(window, new TestInputDevice(), new TestOCRReader(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestLabelCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new Label(window, new TestInputDevice(), new TestOCRReader(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestRadioButtonCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new RadioButton(window, new TestInputDevice(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestTextboxCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new Textbox(window, new TestInputDevice(), new TestOCRReader(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestTreeListCorrectLocation()
        {
            var window = new WindowDouble(10, 110, 10, 110);
            var control = new TreeList(window, new TestInputDevice(), new TestOCRReader(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }
    }
}
