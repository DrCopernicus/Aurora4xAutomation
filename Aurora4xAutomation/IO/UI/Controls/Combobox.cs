using Aurora4xAutomation.Common;
using WindowsInput.Native;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Combobox : Control
    {
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public Combobox(IScreenObject parent, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(parent, inputDevice, top, bottom, left, right)
        {

        }

        public Combobox(IScreen screen, IInputDevice inputDevice, int top, int bottom, int left, int right)
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
                PressKey(VirtualKeyCode.HOME);
                PressKeys(value);
            }
        }

        protected string ReadBox()
        {
            Screenshot.Dirty();
            if (Highlighted)
                return OCRReader.ReadTableRow(
                    Screen.GetPixelsOfColor(
                        Left,
                        Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        new []{new byte[]{255,255,255}}),
                    OCRReader.Alphabet);
            else
                return OCRReader.ReadTableRow(
                    Screen.GetPixelsOfColor(
                        Left,
                        Top + CharacterOffset,
                        Right - Left,
                        CharacterHeight,
                        Colors),
                    OCRReader.Alphabet);
        }

        public void SelectOption(int i)
        {
            Click();
            Click((Right - Left) / 2, (Bottom - Top) / 2 + i * (Bottom - Top));
        }

        public bool Highlighted
        {
            get { return GetPixel(4, 4).EqualsColor(51, 153, 255); }
        }
    }
}
