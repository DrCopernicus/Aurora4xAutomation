using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.UI.Display;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class RadioButton : Control
    {
        public RadioButton(IScreenObject parent, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(parent, inputDevice, top, bottom, left, right)
        {

        }
        public RadioButton(IScreen screen, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(screen, inputDevice, top, bottom, left, right)
        {

        }

        public bool Selected
        {
            get
            {
                return this.GetPixel((Right - Left) / 2, (Bottom - Top) / 2).EqualsColor(0, 0, 0);
            }
            set
            {
                if (value)
                {
                    if (!Selected)
                        this.Click();
                }
                else
                {
                    if (Selected)
                        this.Click();
                }
            }
        }
    }
}
