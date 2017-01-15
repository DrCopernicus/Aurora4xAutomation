using System.Drawing;

namespace Server.IO.UI.Display
{
    public interface IScreenDataRetriever
    {
        Color GetPixel(int x, int y);
        Bitmap Latest { get; }
        void Dirty();
    }
}
