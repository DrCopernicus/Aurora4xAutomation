﻿using System.Drawing;

namespace Aurora4xAutomation.IO.UI
{
    public interface IScreenDataRetriever
    {
        Color GetPixel(int x, int y);
        Bitmap CurrentScreen { get; }
        void Dirty();
    }
}
