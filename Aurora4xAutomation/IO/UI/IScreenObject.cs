using System.Drawing;

namespace Aurora4xAutomation.IO.UI
{
    public interface IScreenObject
    {
        IScreen Screen { get; }
        Color GetPixel(int x, int y);

        int Top { get; }
        int Bottom { get; }
        int Left { get; }
        int Right { get; }
    }
}
