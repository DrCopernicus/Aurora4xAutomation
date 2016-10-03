using System;
using System.Drawing;
using WindowsInput.Native;
using Aurora4xAutomation.IO.OCR;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class TreeListTests
    {
        private class TestScreen : IScreen
        {
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

        [Test]
        public void CorrectlyReadsEmptyTreeList()
        {
            var treelist = new TreeList(new TestScreen(), new TestInputDevice(), new OCRReader(new OCRSplitter()), 0, 340, 0, 707);
            Assert.AreEqual("No children.", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsOneLevelTreeList()
        {
            var treelist = new TreeList(new TestScreen(), new TestInputDevice(), new OCRReader(new OCRSplitter()), 0, 340, 0, 707);
            Assert.AreEqual("", treelist.Text);
        }
    }
}
