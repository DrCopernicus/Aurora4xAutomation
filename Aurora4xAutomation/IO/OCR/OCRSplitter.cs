using System.Collections.Generic;

namespace Aurora4xAutomation.IO.OCR
{
    public class OCRSplitter : ISplitter
    {
        public List<byte[,]> Split(byte[,] pixels, int characterHeight = 20)
        {
            var currentPosition = 0;
            var list = new List<byte[,]>();
            var charInProgress = new List<byte[]>();

            while (currentPosition < pixels.GetLength(1))
            {
                if (!IsBlankColumn(pixels, currentPosition))
                {
                    CopyColumnToEndOfList(charInProgress, pixels, currentPosition);
                }
                else
                {
                    CopyCharacterBytesToList(list, charInProgress, characterHeight);
                    charInProgress = new List<byte[]>();
                }
                currentPosition++;
            }

            return list;
        }

        private void CopyColumnToEndOfList(List<byte[]> charInProgress, byte[,] pixels, int column)
        {
            var pixelColumn = new byte[pixels.GetLength(0)];
            for (int i = 0; i < pixelColumn.Length; i++)
            {
                pixelColumn[i] = pixels[i, column];
            }
            charInProgress.Add(pixelColumn);
        }

        private void CopyCharacterBytesToList(List<byte[,]> list, List<byte[]> charInProgress, int characterHeight)
        {
            var twodbytes = new byte[characterHeight, charInProgress.Count];
            for (int x = 0; x < charInProgress.Count; x++)
            {
                for (int y = 0; y < charInProgress[0].Length; y++)
                {
                    twodbytes[y, x] = charInProgress[x][y];
                }
            }
            list.Add(twodbytes);
        }

        private bool IsBlankColumn(byte[,] pixels, int column)
        {
            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                if (pixels[i, column] != 0)
                    return false;
            }
            return true;
        }
    }
}
