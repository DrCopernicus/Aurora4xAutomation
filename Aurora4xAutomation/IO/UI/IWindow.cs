using System;

namespace Aurora4xAutomation.IO.UI
{
    public interface IWindow : IControl
    {
        IntPtr Handle { get; }

        void MakeActive();
    }
}
