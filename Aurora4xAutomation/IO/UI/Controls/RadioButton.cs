using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class RadioButton : Control
    {
        public RadioButton(IScreenObject screen, int top, int bottom, int left, int right)
            : base(screen, top, bottom, left, right)
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
