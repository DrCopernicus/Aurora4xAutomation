using System.Drawing;
using WindowsInput.Native;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.OCR;
using Pranas;

namespace Aurora4xAutomation.UI.Controls
{
    public class Combobox : Control
    {
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Combobox(Window parent)
            : base(parent)
        {
            
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
                Input.Keyboard.KeyPress(VirtualKeyCode.HOME);
                Input.Keyboard.TextEntry(value);
            }
        }

        protected string ReadBox()
        {
            var screen = new Bitmap(ScreenshotCapture.TakeScreenshot());

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
