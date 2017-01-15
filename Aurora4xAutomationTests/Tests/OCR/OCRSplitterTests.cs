using NUnit.Framework;
using Server.IO.OCR;

namespace Aurora4xAutomationTests.Tests.OCR
{
    [TestFixture]
    public class OCRSplitterTests
    {
        [Test]
        public void SplitsTwoCharactersWithTrailingWhitespace()
        {
            var text = new byte[,] { { 1,1,0,0,0,1,0,0 },
                                     { 0,1,0,0,1,0,1,0 },
                                     { 1,1,1,0,0,1,0,0 } };

            var first = new byte[,] { { 1, 1, 0 }, { 0, 1, 0 }, { 1, 1, 1 } };
            var second = new byte[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };

            var splitter = new OCRSplitter();

            Assert.AreEqual(first, splitter.Split(text, 3)[0]);
            Assert.AreEqual(second, splitter.Split(text, 3)[1]);
        }

        [Test]
        public void SplitsTwoCharactersWithoutTrailingWhitespace()
        {
            var text = new byte[,] { { 1,1,0,0,0,1,0 },
                                     { 0,1,0,0,1,0,1 },
                                     { 1,1,1,0,0,1,0 } };

            var first = new byte[,] { { 1, 1, 0 }, { 0, 1, 0 }, { 1, 1, 1 } };
            var second = new byte[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };

            var splitter = new OCRSplitter();

            Assert.AreEqual(first, splitter.Split(text, 3)[0]);
            Assert.AreEqual(second, splitter.Split(text, 3)[1]);
        }
    }
}
