namespace Aurora4xAutomation.UI.Controls
{
    public class Control
    {
        public Window Parent { get; set; }
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;

        public Control(Window parent)
        {
            Parent = parent;
        }
    }
}
