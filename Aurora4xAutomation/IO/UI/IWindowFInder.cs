using System;

namespace Aurora4xAutomation.IO.UI
{
    public interface IWindowFinder
    {
        IntPtr GetWindowHandle(string title);
        NativeMethods.RECT GetDimensions(IntPtr handle);
    }
}
