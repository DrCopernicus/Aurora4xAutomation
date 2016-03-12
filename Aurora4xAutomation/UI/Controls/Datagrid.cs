using System.Collections.Generic;
using System.Drawing;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.OCR;

namespace Aurora4xAutomation.UI.Controls
{
    public class Datagrid : Control
    {
        public int[] Columns { get; set; }
        public int LineHeight { get; set; }
        public int TopOfCharactersOffset { get; set; }
        public PrintSettings Settings;

        public Datagrid(Window parent) : base(parent)
        {
            
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
                            lineHeight - topOfCharactersOffset - 1,
                            new[] { new byte[] { 0, 0, 0 }, new byte[] { 255, 0, 0 } }),
                        OCRReader.Alphabet);
                }
                table.Add(data);

                currentRowY += lineHeight;
            }

            return table;
        }

        public string GetText()
        {
            return ReadDataTable(Columns, Top, Bottom, LineHeight, TopOfCharactersOffset).Print(Settings);
        }
    }
}
