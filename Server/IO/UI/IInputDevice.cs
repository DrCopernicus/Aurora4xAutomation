using WindowsInput.Native;

namespace Server.IO.UI
{
    public interface IInputDevice
    {
        void Click(int x, int y, int wait);
        void SendKeys(string text);
        void PressKey(VirtualKeyCode key);
    }
}
