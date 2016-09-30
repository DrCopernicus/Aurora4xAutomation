using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.OCR;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Label : Control
    {
        private IOCRReader OCR { get; set; }
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Label(IScreenObject parent, IInputDevice inputDevice, IOCRReader ocr, int top, int bottom, int left, int right)
            : base(parent, inputDevice, top, bottom, left, right)
        {
            CharacterHeight = 9;
            CharacterOffset = 0;
            Colors = new[] { new byte[] { 0, 0, 0 } };
            OCR = ocr;
        }

        public Label(IScreen screen, IInputDevice inputDevice, IOCRReader ocr, int top, int bottom, int left, int right)
            : base(screen, inputDevice, top, bottom, left, right)
        {
            CharacterHeight = 9;
            CharacterOffset = 0;
            Colors = new[] { new byte[] { 0, 0, 0 } };
            OCR = ocr;
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
            return OCR.ReadTableRow(
                    Screen.GetPixelsOfColor(
                        Left,
                        Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        Colors),
                    OCRReader.Alphabet);
        }
    }
}
