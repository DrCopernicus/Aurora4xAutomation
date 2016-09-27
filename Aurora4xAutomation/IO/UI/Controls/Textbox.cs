using Aurora4xAutomation.Common;
using Pranas;
using System.Drawing;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Textbox : Control
    {
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Textbox(IScreenObject parent, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(parent, inputDevice, top, bottom, left, right)
        {

        }
        public Textbox(IScreen screen, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(screen, inputDevice, top, bottom, left, right)
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
                this.PressKeys(value);
            }
        }

        protected string ReadBox()
        {
            var screen = new Bitmap(ScreenshotCapture.TakeScreenshot());

            return OCRReader.ReadTableRow(
                    PixelGetter.GetPixelsOfColor(
                        screen,
                        Left,
                        Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        Colors),
                    OCRReader.Alphabet);
        }
    }
}
