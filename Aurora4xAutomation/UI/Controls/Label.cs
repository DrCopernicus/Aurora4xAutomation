using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.OCR;

namespace Aurora4xAutomation.UI.Controls
{
    public class Label : Control
    {
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Label(Window parent, int left, int right, int top, int bottom)
            : base(parent)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
            CharacterHeight = 9;
            CharacterOffset = 0;
            Colors = new[] {new byte[] {0, 0, 0}};
        }

        public string Text
        {
            get
            {
                return ReadBox();
            }
        }

        protected string ReadBox()
        {
            return OCRReader.ReadTableRow(
                    PixelGetter.GetPixelsOfColor(
                        Screenshot.Latest,
                        Parent.Dimensions.Left + Left,
                        Parent.Dimensions.Top + Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        Colors),
                    OCRReader.Alphabet);
        }
    }
}
