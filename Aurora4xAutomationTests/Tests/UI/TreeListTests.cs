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
    public class TreeListTests
    {
        private class TestScreen : IScreen
        {
            public Color GetPixel(int x, int y)
            {
                throw new System.NotImplementedException();
            }

            public byte[,] GetPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
            {
                throw new System.NotImplementedException();
            }

            public bool HasPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
            {
                throw new System.NotImplementedException();
            }

            public bool OnlyHasPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
            {
                throw new System.NotImplementedException();
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
        public void CorrectlyReadsEmptyTreeList()
        {
            var treelist = new TreeList(new TestScreen(), new TestInputDevice(), new TestOCRReader(), 0, 100, 0, 100);
            Assert.AreEqual("No children.", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsOneLevelTreeList()
        {
            var treelist = new TreeList(new TestScreen(), new TestInputDevice(), new TestOCRReader(), 0, 100, 0, 100);
            Assert.AreEqual("", treelist.Text);
        }
    }
}
