using System;

namespace Server.IO.UI
{
    public interface IWindow
    {
        IntPtr Handle { get; }

        void MakeActive();
    }
}
