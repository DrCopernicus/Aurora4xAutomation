using Server.IO.UI.Display;

namespace Server.IO.UI.Controls
{
    public class Control : ScreenObject
    {
        public Control(IScreen screen, IInputDevice inputDevice, int top, int bottom, int left, int right)
        {
            Screen = screen;
            Parent = screen;
            InputDevice = inputDevice;
            _relativeTop = top;
            _relativeBottom = bottom;
            _relativeLeft = left;
            _relativeRight = right;
        }

        public Control(IScreenObject parent, IInputDevice inputDevice, int top, int bottom, int left, int right)
        {
            Screen = parent.Screen;
            Parent = parent;
            InputDevice = inputDevice;
            _relativeTop = top;
            _relativeBottom = bottom;
            _relativeLeft = left;
            _relativeRight = right;
        }
    }
}