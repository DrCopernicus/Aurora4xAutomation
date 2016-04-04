using Aurora4xAutomation.Common;

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
                return GetPixel((Right - Left) / 2, (Bottom - Top) / 2).EqualsColor(0, 0, 0);
            }
            set
            {
                if (value)
                {
                    if (!Selected)
                        Click();
                }
                else
                {
                    if (Selected)
                        Click();
                }
            }
        }
    }
}
