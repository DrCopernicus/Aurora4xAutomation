using System.Drawing;
using WindowsInput.Native;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO;
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
                this.Click();
                this.Click();
                this.PressKey(VirtualKeyCode.HOME);
                this.SendKeys(value);
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

        public void SelectOption(int i)
        {
            this.Click();
            this.Click((Right - Left) / 2, (Bottom - Top) / 2 + i * (Bottom - Top));
        }
    }
}
