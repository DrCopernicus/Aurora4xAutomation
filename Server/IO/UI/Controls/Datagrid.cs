using Server.Common;
using Server.IO.OCR;
using Server.IO.UI.Display;
using System.Collections.Generic;
using System.Linq;

namespace Server.IO.UI.Controls
{
    public class Datagrid : Control
    {
        private IOCRReader OCR { get; set; }
        public int[] Columns { get; set; }
        public int LineHeight { get; set; }
        public int TopOfCharactersOffset { get; set; }
        public PrintSettings Settings;

        public Datagrid(IScreenObject parent, IInputDevice inputDevice, IOCRReader ocr, int top, int bottom, int[] columns)
            : base(parent, inputDevice, top, bottom, columns.First(), columns.Last())
        {
            OCR = ocr;
            Columns = columns;
        }

        public Datagrid(IScreen screen, IInputDevice inputDevice, IOCRReader ocr, int top, int bottom, int[] columns)
            : base(screen, inputDevice, top, bottom, columns.First(), columns.Last())
        {
            OCR = ocr;
            Columns = columns;
        }

        private List<string[]> ReadDataTable(int[] columns, int top, int bottom, int lineHeight, int topOfCharactersOffset)
        {
            var table = new List<string[]>();
            var currentRowY = top;

            while (currentRowY + lineHeight - 1 <= bottom)
            {
                var data = new string[columns.Length - 1];
                for (var i = 0; i < columns.Length - 1; i++)
                {
                    data[i] = OCR.ReadTableRow(
                        Screen.GetPixelsOfColor(
                            GetColumnStartX(i),
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
            Click((Right - Left) / 2, row * LineHeight + LineHeight/2);
        }

        private int GetColumnStartX(int index)
        {
            return Columns[index] + Left - Columns[0];
        }

        public void Dirty()
        {
            Screen.Dirty();
        }
    }
}
