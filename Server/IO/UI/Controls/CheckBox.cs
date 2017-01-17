using Server.Common;
using Server.IO.UI.Display;

namespace Server.IO.UI.Controls
{
    public class CheckBox : Control
    {
        public CheckBox(IScreenObject parent, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(parent, inputDevice, top, bottom, left, right)
        {

        }
        public CheckBox(IScreen screen, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(screen, inputDevice, top, bottom, left, right)
        {

        }

        public bool Selected
        {
            get
            {
                return GetPixel((Right - Left) / 2, (Bottom - Top) / 2).EqualsColor(0, 0, 0);
            }
        }

        public void Select()
        {
            if (Selected)
                return;

            Click();
            Screen.Dirty();
        }

        public void Deselect()
        {
            if (!Selected)
                return;

            Click();
            Screen.Dirty();
        }
    }
}
