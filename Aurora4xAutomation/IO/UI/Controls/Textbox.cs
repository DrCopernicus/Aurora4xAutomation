﻿using Aurora4xAutomation.Common;
using Pranas;
using System.Drawing;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Textbox : Control
    {
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Textbox(IScreenObject parent, int top, int bottom, int left, int right)
            : base(parent, top, bottom, left, right)
        {

        }
        public Textbox(IScreen screen, int top, int bottom, int left, int right)
            : base(screen, top, bottom, left, right)
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
                this.SendKeys(value);
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
