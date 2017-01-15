using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Server.Common;
using Server.IO.OCR;

namespace Aurora4xAutomationTests.Tests.OCR
{
    [TestFixture]
    public class OCRBitmapTests
    {
        private byte[,] Read(Bitmap bitmap, byte[][] colors)
        {
            var pixels = new byte[bitmap.Height, bitmap.Width];

            for (var xi = 0; xi < bitmap.Width; xi++)
            {
                for (var yi = 0; yi < bitmap.Height; yi++)
                {
                    var pix = bitmap.GetPixel(xi, yi);
                    if (colors.Any(color => pix.EqualsColor(color[0], color[1], color[2])))
                        pixels[yi, xi] = 1;
                }
            }

            return pixels;
        }

        [Test]
        public void ReadsUncomplicatedCharacters()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var alphabet = new Dictionary<string, byte[,]>
            {
                {"1", new byte[,] {{1,1,0}, {0,1,0}, {1,1,1}}}
            };
            var colors = new[] {new byte[] {0, 0, 0}};

            Assert.AreEqual("", ocr.ReadTableRow(Read(Properties.Resources.ocr_ones, colors), alphabet));
            Assert.AreEqual("11111", ocr.ReadTableRow(Read(Properties.Resources.ocr_onesclean, colors), alphabet));
        }

        [Test]
        public void ReadsUncomplicatedRealCharacters()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var colors = new[] { new byte[] { 0, 0, 0 } };

            Assert.AreEqual("Demand", ocr.ReadTableRow(Read(Properties.Resources.ocr_Demand, colors), OCRReader.Alphabet));
            Assert.AreEqual("MannedMines", ocr.ReadTableRow(Read(Properties.Resources.ocr_MannedMines, colors), OCRReader.Alphabet));
        }

        [Test]
        public void ReadsRealCharactersWithDropCharacters()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var colors = new[] { new byte[] { 0, 0, 0 } };

            Assert.AreEqual("CivilianMiningColonies", ocr.ReadTableRow(Read(Properties.Resources.ocr_CivilianMiningColonies, colors), OCRReader.Alphabet));
            Assert.AreEqual("Terraforminglnstallations", ocr.ReadTableRow(Read(Properties.Resources.ocr_TerraformingInstallations, colors), OCRReader.Alphabet));
        }

        [Test]
        public void ReadsRealCharactersWithSideBySideDropCharacters()
        {
            var ocr = new OCRReader(new OCRSplitter());
            var colors = new[] { new byte[] { 0, 0, 0 } };

            Assert.AreEqual("ManageShipyards", ocr.ReadTableRow(Read(Properties.Resources.ocr_ManageShipyards, colors), OCRReader.Alphabet));
            Assert.AreEqual("lnstallationType", ocr.ReadTableRow(Read(Properties.Resources.ocr_InstallationType, colors), OCRReader.Alphabet));
        }
    }
}
