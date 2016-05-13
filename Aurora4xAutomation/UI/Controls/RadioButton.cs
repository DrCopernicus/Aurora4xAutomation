using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.UI.Controls
{
    public class RadioButton : Control
    {
        public RadioButton(Window parent)
            : base(parent)
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
