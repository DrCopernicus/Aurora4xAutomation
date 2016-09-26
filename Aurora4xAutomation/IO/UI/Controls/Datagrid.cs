using System.Collections.Generic;
using System.Drawing;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Datagrid : Control
    {
        public int[] Columns { get; set; }
        public int LineHeight { get; set; }
        public int TopOfCharactersOffset { get; set; }
        public PrintSettings Settings;

        public Datagrid(IScreenObject parent, int top, int bottom, int left, int right)
            : base(parent, top, bottom, left, right)
        {
            
        }

        private List<string[]> ReadDataTable(int[] columns, int top, int bottom, int lineHeight, int topOfCharactersOffset)
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
                            columns[i],
                            currentRowY + topOfCharactersOffset,
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

        public List<string[]> GetTable()
        {
            return ReadDataTable(Columns, Top, Bottom, LineHeight, TopOfCharactersOffset);
        }

        public void ClickRow(int row)
        {
            this.Click((Right - Left) / 2, row * LineHeight + LineHeight/2);
        }
    }
}
