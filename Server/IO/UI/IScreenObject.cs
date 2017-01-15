using System.Drawing;
using WindowsInput.Native;
using Server.IO.UI.Display;

namespace Server.IO.UI
{
    public interface IScreenObject
    {
        Color GetPixel(int x, int y);

        void Click();
        void Click(int x, int y, int wait);

        void PressKey(VirtualKeyCode key);
        void PressKeys(string text);

        IScreen Screen { get; }
        IInputDevice InputDevice { get; }
        int Top { get; }
        int Bottom { get; }
        int Left { get; }
        int Right { get; }
    }
}
