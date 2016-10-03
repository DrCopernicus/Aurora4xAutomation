using System.Collections.Generic;
using Aurora4xAutomation.IO.OCR;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.OCR
{
    [TestFixture]
    public class OCRIntegration
    {
        [Test]
        public void ReadsOneCharacter()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"1", new byte[,] {{1,1,0}, {0,1,0}, {1,1,1}}}
            };

            var text = new byte[,] { { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 1, 1, 1, 0 } };

            Assert.AreEqual("1", ocr.ReadTableRow(text, alphabet));
        }

        [Test]
        public void ReadsTwoOfSameCharacter()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"1", new byte[,] {{1,1,0}, {0,1,0}, {1,1,1}}}
            };

            var text = new byte[,] { { 1, 1, 0, 0, 1, 1, 0, 0 }, { 0, 1, 0, 0, 0, 1, 0, 0 }, { 1, 1, 1, 0, 1, 1, 1, 0 } };

            Assert.AreEqual("11", ocr.ReadTableRow(text, alphabet));
        }

        [Test]
        public void ReadsTwoDifferentCharacters()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"1", new byte[,] {{1,1,0}, {0,1,0}, {1,1,1}}},
                {"0", new byte[,] {{0,1,0}, {1,0,1}, {0,1,0}}}
            };

            var text = new byte[,] { { 0, 1, 0, 0, 1, 1, 0, 0 }, { 1, 0, 1, 0, 0, 1, 0, 0 }, { 0, 1, 0, 0, 1, 1, 1, 0 } };

            Assert.AreEqual("01", ocr.ReadTableRow(text, alphabet));
        }

        [Test]
        public void DifferentiatesBetweenDifferentDots()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var alphabet = new Dictionary<string, byte[,]>
            {
                {":", new byte[,] {{1}, {0}, {1}}},
                {"-", new byte[,] {{0}, {1}, {0}}},
                {".", new byte[,] {{0}, {0}, {1}}}
            };

            var text = new byte[,] { { 0, 0, 0, 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1, 0, 0, 0 }, { 1, 0, 1, 0, 0, 0, 1, 0 } };

            Assert.AreEqual("..-:", ocr.ReadTableRow(text, alphabet));
        }

        [Test]
        public void ReadsDifferentDotsWithoutTrailingWhitespaceAtTheEnd()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var alphabet = new Dictionary<string, byte[,]>
            {
                {":", new byte[,] {{1}, {0}, {1}}},
                {"-", new byte[,] {{0}, {1}, {0}}},
                {".", new byte[,] {{0}, {0}, {1}}}
            };

            var text = new byte[,] { { 0, 0, 0, 0, 0, 0, 1 }, { 0, 0, 0, 0, 1, 0, 0 }, { 1, 0, 1, 0, 0, 0, 1 } };

            Assert.AreEqual("..-:", ocr.ReadTableRow(text, alphabet));
        }


        [Test]
        public void DoesNotReadForcedWhitespace()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var alphabet = new Dictionary<string, byte[,]>
            {
                {".", new byte[,] {{0,0,0}, {0,0,0}, {1,0,0}}}
            };

            var text = new byte[,] { { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 1, 0, 0, 0, 1, 0, 0, 0, 0 } };

            Assert.AreEqual("", ocr.ReadTableRow(text, alphabet));
        }

        [Test]
        public void ReadsCharactersWithLotsOfWhitespaceAbove()
        {
            var ocr = new OCRReader(new OCRSplitter());

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

            Assert.AreEqual(".", ocr.ReadTableRow(text, alphabet));
        }

        [Test]
        public void ReadsVeryLongLines()
        {
            var ocr = new OCRReader(new OCRSplitter());

            var alphabet = new Dictionary<string, byte[,]>
            {
                {"_", new byte[,] {{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}}}
            };

            var text = new byte[,] { { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                     { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 } };

            Assert.AreEqual("_", ocr.ReadTableRow(text, alphabet));
        }

        [Test]
        public void ReadsTheLetterG()
        {
            var ocr = new OCRReader(new OCRSplitter());
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
            Assert.AreEqual("G", ocr.ReadTableRow(pixels, alphabet));
        }

        [Test]
        public void ReadsTheWordGenome()
        {
            var ocr = new OCRReader(new OCRSplitter());
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
            Assert.AreEqual("Genome", ocr.ReadTableRow(pixels, OCRReader.Alphabet));
        }

        [Test]
        public void ReadsTheWordGenomeWithExtraneousSpaces()
        {
            var ocr = new OCRReader(new OCRSplitter());
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
            Assert.AreEqual("Genome", ocr.ReadTableRow(pixels, OCRReader.Alphabet));
        }
    }
}
