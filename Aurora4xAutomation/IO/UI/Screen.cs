using System;
using System.Drawing;

namespace Aurora4xAutomation.IO.UI
{
    public class Screen : IScreen
    {
        public Color GetPixel(int x, int y)
        {
            var context = NativeMethods.GetDC(IntPtr.Zero);
            var pixel = NativeMethods.GetPixel(context, x, y);
            NativeMethods.ReleaseDC(IntPtr.Zero, context);
            var color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
    }
}
