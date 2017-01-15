using System.Drawing;

namespace Server.Common
{
    public static class ColorExtensions
    {
        public static bool EqualsColor(this Color color, byte r, byte g, byte b)
        {
            return color.R == r && color.G == g && color.B == b;
        }
    }
}
