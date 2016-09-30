using System.Collections.Generic;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.OCR;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.OCR
{
    [TestFixture]
    public class OCRTests
    {
        private class TestSplitter : ISplitter
        {
            public TestSplitter(List<byte[,]> preSplitBytes)
            {
                PreSplitBytes = preSplitBytes;
            }

            public List<byte[,]> Split(byte[,] pixels, int characterHeight = 20)
            {
                return PreSplitBytes;
            }

            private List<byte[,]> PreSplitBytes { get; set; }
        }
    }
}
