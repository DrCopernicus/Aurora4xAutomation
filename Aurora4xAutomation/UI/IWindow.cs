using System;

namespace Aurora4xAutomation.UI
{
    public interface IWindow : IControl
    {
        IntPtr Handle { get; }
        Window.RECT Dimensions { get; }

        void MakeActive();
    }
}
