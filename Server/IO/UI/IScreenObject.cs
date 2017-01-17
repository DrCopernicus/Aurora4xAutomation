using Server.IO.UI.Display;
using System.Drawing;
using WindowsInput.Native;

namespace Server.IO.UI
{
    public interface IScreenObject : IPositionable
    {
        Color GetPixel(int x, int y);

        void Click();
        void Click(int x, int y, int wait);

        void PressKey(VirtualKeyCode key);
        void PressKeys(string text);

        IScreen Screen { get; }
        IInputDevice InputDevice { get; }
    }
}
