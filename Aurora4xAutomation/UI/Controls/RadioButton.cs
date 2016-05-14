using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.UI.Controls
{
    public class RadioButton : Control
    {
        public RadioButton(IWindow parent, int top, int bottom, int left, int right)
            : base(parent, top, bottom, left, right)
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
