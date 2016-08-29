using System.Collections.Generic;
using Aurora4xAutomation.IO;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests
{
    [TestFixture]
    public class OCRTests
    {
        [Test]
        public void TestByteArrayEquality()
        {
            var arrayA = new byte[,] {{0,1,0,0,0,1},
                                      {1,0,0,0,0,1}};

            var arrayB = new byte[,] {{0,1,0,0,0,1},
                                      {1,0,0,0,0,1}};

            Assert.IsTrue(arrayA.EqualsPixels(arrayB));
        }

        [Test]
        public void TestReadCharacter()
        {
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"1", new byte[,] {{1,1,0}, {0,1,0}, {1,1,1}}}
            };

            var text = new byte[,] { { 1, 1, 0 }, { 0, 1, 0 }, { 1, 1, 1 } };

            Assert.AreEqual("1", OCRReader.ReadCharacter(text, alphabet));
        }

        [Test]
        public void TestSplit()
        {
            var text = new byte[,] { { 1,1,0,0,0,1,0,0 },
                                     { 0,1,0,0,1,0,1,0 },
                                     { 1,1,1,0,0,1,0,0 } };

            var first = new byte[,] { { 1, 1, 0 }, { 0, 1, 0 }, { 1, 1, 1 } };
            var second = new byte[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };

            Assert.AreEqual(first, text.Split(3)[0]);
            Assert.AreEqual(second, text.Split(3)[1]);
        }

        [Test]
        public void TestSplitAsString()
        {
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"1", new byte[,] {{1,1,0}, {0,1,0}, {1,1,1}}},
                {"0", new byte[,] {{0,1,0}, {1,0,1}, {0,1,0}}}
            };

            var text = new byte[,] { { 1,1,0,0,0,1,0,0 },
                                     { 0,1,0,0,1,0,1,0 },
                                     { 1,1,1,0,0,1,0,0 } };

            Assert.AreEqual("10", OCRReader.ReadTableRow(text, alphabet));
        }

        [Test]
        public void TestDots()
        {
            var alphabet = new Dictionary<string, byte[,]>
            {
                {".", new byte[,] {{0}, {0}, {1}}}
            };

            var text = new byte[,] { { 0,0 },
                                     { 0,0 },
                                     { 1,0 } };

            Assert.AreEqual(".", OCRReader.ReadTableRow(text, alphabet));
        }

        [Test]
        public void TestTallDots()
        {
            var alphabet = new Dictionary<string, byte[,]>
            {
                {".", new byte[,] {{0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {1}}}
            };

            var text = new byte[,] { { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 1,0 } };

            Assert.AreEqual(".", OCRReader.ReadTableRow(text, alphabet));
        }

        [Test]
        public void TestVeryTallDots()
        {
            var alphabet = new Dictionary<string, byte[,]>
            {
                {".", new byte[,] {{0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {0}, {1}}}
            };

            var text = new byte[,] { { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 0,0 },
                                     { 1,0 } };

            Assert.AreEqual(".", OCRReader.ReadTableRow(text, alphabet));
        }

        [Test]
        public void TestVeryLongLines()
        {
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"_", new byte[,] {{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}}}
            };

            var text = new byte[,] { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                     { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 } };

            Assert.AreEqual("_", OCRReader.ReadTableRow(text, alphabet));
        }

        [Test]
        public void TestG()
        {
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"G", new byte[,] {{0, 1, 1, 1, 1, 0},
                                   {1, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 0},
                                   {1, 0, 0, 0, 0, 0},
                                   {1, 0, 0, 1, 1, 1},
                                   {1, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 0, 1},
                                   {1, 0, 0, 0, 1, 1},
                                   {0, 1, 1, 1, 0, 1},
                                   {0, 0, 0, 0, 0, 0},
                                   {0, 0, 0, 0, 0, 0}}}
            };

            var pixels = new byte[,] {{0,1,1,1,1,0,0},
                                      {1,0,0,0,0,1,0},
                                      {1,0,0,0,0,0,0},
                                      {1,0,0,0,0,0,0},
                                      {1,0,0,1,1,1,0},
                                      {1,0,0,0,0,1,0},
                                      {1,0,0,0,0,1,0},
                                      {1,0,0,0,1,1,0},
                                      {0,1,1,1,0,1,0},
                                      {0,0,0,0,0,0,0},
                                      {0,0,0,0,0,0,0}};
            Assert.AreEqual("G", OCRReader.ReadTableRow(pixels, alphabet));
        }

        [Test]
        public void TestGenome()
        {
            var pixels = new byte[,] {{0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,0,0,0,0,0,0,0,0,1,1,1,0,0,1,0,1,1,0,0,0,1,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,0,0},
                                      {1,0,0,1,1,1,0,0,1,0,0,0,1,0,1,1,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,0,1,0},
                                      {1,0,0,0,0,1,0,0,1,1,1,1,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,1,0},
                                      {1,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,0,0,0},
                                      {1,0,0,0,1,1,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,0,1,0},
                                      {0,1,1,1,0,1,0,0,0,1,1,1,0,0,1,0,0,0,1,0,0,1,1,1,0,0,1,0,0,1,0,0,1,0,0,1,1,1,0,0},
                                      {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
            Assert.AreEqual("Genome", OCRReader.ReadTableRow(pixels, OCRReader.Alphabet));
        }

        [Test]
        public void TestGenomeWithSpace()
        {
            var pixels = new byte[,] {{0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,1,0,1,1,0,0,0,1,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,0,0},
                                      {1,0,0,1,1,1,0,0,1,0,0,0,1,0,0,0,0,1,1,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,0,1,0},
                                      {1,0,0,0,0,1,0,0,1,1,1,1,1,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,1,0},
                                      {1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,0,0,0},
                                      {1,0,0,0,1,1,0,0,1,0,0,0,1,0,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,0,1,0},
                                      {0,1,1,1,0,1,0,0,0,1,1,1,0,0,0,0,0,1,0,0,0,1,0,0,1,1,1,0,0,1,0,0,1,0,0,1,0,0,1,1,1,0,0},
                                      {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
            Assert.AreEqual("Genome", OCRReader.ReadTableRow(pixels, OCRReader.Alphabet));
        }
    }
}
