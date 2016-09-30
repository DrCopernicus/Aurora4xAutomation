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
                Click();
                Click();
                PressKeys(value);
            }
        }

        protected string ReadBox()
        {
            return OCRReader.ReadTableRow(
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
