using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Aurora4xAutomation.Common
{
    public class PixelGetter
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        public static Color GetPixel(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        public static byte[,] GetPixelsOfColor(Bitmap screen, int x, int y, int width, int height, byte[][] colors)
        {
            var pixels = new byte[height, width];

            for (int xi = 0; xi < width; xi++)
            {
                for (int yi = 0; yi < height; yi++)
                {
                    var pix = screen.GetPixel(x + xi, y + yi);
                    if (colors.Any(color => pix.EqualsColor(color[0], color[1], color[2])))
                    {
                        pixels[yi, xi] = 1;
                    }
                }
            }

            return pixels;
        }
    }
}
