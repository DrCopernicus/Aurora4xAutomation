using System.Drawing;

namespace Server.IO.UI.Display
{
    public interface IScreen
    {
        void Dirty();
        Color GetPixel(int x, int y);
        byte[,] GetPixelsOfColor(int x, int y, int width, int height, byte[][] colors);
        bool HasPixelsOfColor(int x, int y, int width, int height, byte[][] colors);
        bool OnlyHasPixelsOfColor(int x, int y, int width, int height, byte[][] colors);
    }
}
