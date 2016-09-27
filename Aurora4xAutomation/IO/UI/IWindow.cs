using System;

namespace Aurora4xAutomation.IO.UI
{
    public interface IWindow
    {
        IntPtr Handle { get; }

        void MakeActive();
    }
}
