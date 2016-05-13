namespace Aurora4xAutomation.UI.Controls
{
    public class Control : IControl
    {
        public IWindow Parent { get; set; }

        public Control(IWindow parent)
        {
            Parent = parent;
        }

        public Control(IWindow parent, int top, int bottom, int left, int right)
        {
            Parent = parent;
            Top = Parent.Top + top;
            Bottom = Parent.Top + bottom;
            Left = Parent.Left + left;
            Right = Parent.Left + right;
        }

        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
}
