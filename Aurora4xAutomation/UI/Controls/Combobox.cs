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

        public Combobox(IWindow parent, int top, int bottom, int left, int right)
            : base(parent, top, bottom, left, right)
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
            Screenshot.Dirty();
            if (Highlighted)
                return OCRReader.ReadTableRow(
                    PixelGetter.GetPixelsOfColor(
                        Left,
                        Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        new []{new byte[]{255,255,255}}),
                    OCRReader.Alphabet);
            else
                return OCRReader.ReadTableRow(
                    PixelGetter.GetPixelsOfColor(
                        Left,
                        Top + CharacterOffset,
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

        public bool Highlighted
        {
            get { return GetPixel(4, 4).EqualsColor(51, 153, 255); }
        }
    }
}
