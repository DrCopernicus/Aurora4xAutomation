using Server.Common;
using System.Drawing;
using System.Linq;

namespace Server.IO.UI.Display
{
    public class Screen : IScreen
    {
        public IScreenDataRetriever ScreenDataRetriever { get; set; }

        public Screen(IScreenDataRetriever screenDataRetriever)
        {
            ScreenDataRetriever = screenDataRetriever;
        }

        public Color GetPixel(int x, int y)
        {
            return ScreenDataRetriever.GetPixel(x, y);
        }

        public byte[,] GetPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
        {
            var pixels = new byte[height, width];
            var screen = ScreenDataRetriever.Latest;

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
            var screen = ScreenDataRetriever.Latest;

            for (var xi = 0; xi < width; xi++)
            {
                for (var yi = 0; yi < height; yi++)
                {
                    var pixel = screen.GetPixel(x + xi, y + yi);
                    if (colors.Any(color => pixel.EqualsColor(color[0], color[1], color[2])))
                        return true;
                }
            }

            return false;
        }

        public bool OnlyHasPixelsOfColor(int x, int y, int width, int height, byte[][] colors)
        {
            var screen = ScreenDataRetriever.Latest;

            for (var xi = 0; xi < width; xi++)
            {
                for (var yi = 0; yi < height; yi++)
                {
                    var pixel = screen.GetPixel(x + xi, y + yi);
                    if (!colors.Any(color => pixel.EqualsColor(color[0], color[1], color[2])))
                        return false;
                }
            }

            return true;
        }

        public void Dirty()
        {
            ScreenDataRetriever.Dirty();
        }

        public int Top { get { return 0; } }
        public int Bottom { get { return 0; } }
        public int Left { get { return 0; } }
        public int Right { get { return 0; } }
    }
}
