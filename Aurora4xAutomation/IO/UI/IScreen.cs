﻿using System.Drawing;

namespace Aurora4xAutomation.IO.UI
{
    public interface IScreen
    {
        Color GetPixel(int x, int y);
        byte[,] GetPixelsOfColor(int x, int y, int width, int height, byte[][] colors);
        bool HasPixelsOfColor(int x, int y, int width, int height, byte[][] colors);
        bool OnlyHasPixelsOfColor(int x, int y, int width, int height, byte[][] colors);
    }
}
