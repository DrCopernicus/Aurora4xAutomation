namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Control : ScreenObject
    {
        public Control(IScreen screen, IInputDevice inputDevice, int top, int bottom, int left, int right)
        {
            Screen = screen;
            InputDevice = inputDevice;
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public Control(IScreenObject parent, IInputDevice inputDevice, int top, int bottom, int left, int right)
        {
            Screen = parent.Screen;
            InputDevice = inputDevice;
            Top = parent.Top + top;
            Bottom = parent.Top + bottom;
            Left = parent.Left + left;
            Right = parent.Left + right;
        }
    }
}