using System.Collections.Generic;
using System.Drawing;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.OCR;

namespace Aurora4xAutomation.UI.Controls
{
    public class Textbox : Control
    {
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Textbox(Window parent)
            : base(parent)
        {
            
        }

        public string Text
        {
            get { return ReadBox(); }
        }

        protected string ReadBox()
        {
            var screen = new Bitmap(Pranas.ScreenshotCapture.TakeScreenshot());

            return OCRReader.ReadTableRow(
                    PixelGetter.GetPixelsOfColor(
                        screen,
                        Parent.Dimensions.Left + Left,
                        Parent.Dimensions.Top + Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        Colors),
                    OCRReader.Alphabet);
        }
    }
}
