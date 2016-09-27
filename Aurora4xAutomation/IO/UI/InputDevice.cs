using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace Aurora4xAutomation.IO.UI
{
    public class InputDevice : IInputDevice
    {
        private static readonly InputSimulator Input = new InputSimulator();

        public void Click(int x, int y, int wait)
        {
            Cursor.Position = new Point(x, y);
            if (wait != 0)
                Thread.Sleep(wait);
            Input.Mouse.LeftButtonClick();
        }

        public void SendKeys(string text)
        {
            Input.Keyboard.TextEntry(text);
        }

        public void PressKey(VirtualKeyCode key)
        {
            Input.Keyboard.KeyPress(key);
        }
    }
}
