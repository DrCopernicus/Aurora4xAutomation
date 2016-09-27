﻿using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Label : Control
    {
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Label(IScreenObject parent, int top, int bottom, int left, int right)
            : base(parent, top, bottom, left, right)
        {
            CharacterHeight = 9;
            CharacterOffset = 0;
            Colors = new[] { new byte[] { 0, 0, 0 } };
        }

        public Label(IScreen screen, int top, int bottom, int left, int right)
            : base(screen, top, bottom, left, right)
        {
            CharacterHeight = 9;
            CharacterOffset = 0;
            Colors = new[] { new byte[] { 0, 0, 0 } };
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
                        Left,
                        Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        Colors),
                    OCRReader.Alphabet);
        }
    }
}
