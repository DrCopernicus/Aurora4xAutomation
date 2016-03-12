using System.Collections.Generic;
using System.Drawing;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.OCR;

namespace Aurora4xAutomation.UI.Controls
{
    public class Control
    {
        public Window Parent { get; set; }

        public Control(Window parent)
        {
            Parent = parent;
        }

        protected List<string[]> ReadDataTable(int[] columns, int top, int bottom, int lineHeight, int topOfCharactersOffset)
        {
            var screen = new Bitmap(Pranas.ScreenshotCapture.TakeScreenshot());
            var table = new List<string[]>();
            var currentRowY = top;

            while (currentRowY <= bottom + lineHeight)
            {
                var data = new string[columns.Length - 1];
                for (int i = 0; i < columns.Length - 1; i++)
                {
                    data[i] = OCRReader.ReadTableRow(
                        PixelGetter.GetPixelsOfColor(
                            screen,
                            Parent.Dimensions.Left + columns[i],
                            Parent.Dimensions.Top + currentRowY + topOfCharactersOffset,
                            columns[i + 1] - columns[i],
                            lineHeight - topOfCharactersOffset - 1),
                        OCRReader.Alphabet);
                }
                table.Add(data);

                currentRowY += lineHeight;
            }

            return table;
        }
    }
}
