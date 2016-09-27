using WindowsInput.Native;

namespace Aurora4xAutomation.IO.UI
{
    public interface IInputDevice
    {
        void Click(int x, int y, int wait);
        void SendKeys(string text);
        void PressKey(VirtualKeyCode key);
    }
}
