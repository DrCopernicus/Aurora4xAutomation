using System.Drawing;

namespace Aurora4xAutomation.IO.UI
{
    public interface IScreen
    {
        Color GetPixel(int x, int y);
    }
}
