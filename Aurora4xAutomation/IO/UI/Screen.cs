using System;
using System.Drawing;
using System.Linq;
using Aurora4xAutomation.Common;

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

        public byte[,] GetPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
        {
            var pixels = new byte[height, width];
            var screen = Screenshot.Latest;

            for (var xi = 0; xi < width; xi++)
            {
                for (var yi = 0; yi < height; yi++)
                {
                    var pix = screen.GetPixel(x + xi, y + yi);
                    if (colors.Any(color => pix.EqualsColor(color[0], color[1], color[2])))
                        pixels[yi, xi] = 1;
                }
            }

            return pixels;
        }

        public bool HasPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
        {
            var screen = Screenshot.Latest;

            for (var xi = 0; xi < width; xi++)
            {
                for (var yi = 0; yi < height; yi++)
                {
                    var pix = screen.GetPixel(x + xi, y + yi);
                    if (colors.Any(color => pix.EqualsColor(color[0], color[1], color[2])))
                        return true;
                }
            }

            return false;
        }

        public bool OnlyHasPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
        {
            var screen = Screenshot.Latest;

            for (var xi = 0; xi < width; xi++)
            {
                for (var yi = 0; yi < height; yi++)
                {
                    var pix = screen.GetPixel(x + xi, y + yi);
                    if (!colors.Any(color => pix.EqualsColor(color[0], color[1], color[2])))
                        return false;
                }
            }

            return true;
        }
    }
}
