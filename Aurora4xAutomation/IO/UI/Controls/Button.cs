namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Button : Control
    {
        public Button(IScreen screen, int top, int bottom, int left, int right)
            : base(screen, top, bottom, left, right)
        {

        }

        public Button(IScreenObject parent, int top, int bottom, int left, int right)
            : base(parent, top, bottom, left, right)
        {

        }
    }
}
