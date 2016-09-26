using System.Drawing;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class Control : IScreenObject
    {
        public Control(IScreenObject parent, int top, int bottom, int left, int right)
        {
            Parent = parent;
            Top = Parent.Top + top;
            Bottom = Parent.Top + bottom;
            Left = Parent.Left + left;
            Right = Parent.Left + right;
        }

        public IScreenObject Parent { get; protected set; }
        public int Top { get; protected set; }
        public int Bottom { get; protected set; }
        public int Left { get; protected set; }
        public int Right { get; protected set; }

        public Color GetPixel(int x, int y)
        {
            return PixelGetter.GetPixel(Left + x, Top + y);
        }
    }
}