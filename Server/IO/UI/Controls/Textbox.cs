using Server.IO.OCR;
using Server.IO.UI.Display;

namespace Server.IO.UI.Controls
{
    public class Textbox : Control
    {
        private IOCRReader OCR { get; set; }
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Textbox(IScreenObject parent, IInputDevice inputDevice, IOCRReader ocr, int top, int bottom, int left, int right)
            : base(parent, inputDevice, top, bottom, left, right)
        {
            OCR = ocr;
        }

        public Textbox(IScreen screen, IInputDevice inputDevice, IOCRReader ocr, int top, int bottom, int left, int right)
            : base(screen, inputDevice, top, bottom, left, right)
        {
            OCR = ocr;
        }

        public string Text
        {
            get
            {
                return ReadBox();
            }
            set
            {
                Click();
                Click();
                PressKeys(value);
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
