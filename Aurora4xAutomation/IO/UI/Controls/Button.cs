namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Button : Control
    {
        public Button(IScreen screen, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(screen, inputDevice, top, bottom, left, right)
        {

        }

        public Button(IScreenObject parent, IInputDevice inputDevice, int top, int bottom, int left, int right)
            : base(parent, inputDevice, top, bottom, left, right)
        {

        }
    }
}
