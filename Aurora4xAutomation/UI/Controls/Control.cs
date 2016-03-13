using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;

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

        protected static readonly InputSimulator Input = new InputSimulator();

        public void Click(int x, int y)
        {
            Cursor.Position = new Point(Parent.Dimensions.Left + Left + x, Parent.Dimensions.Top + Top + y);
            Thread.Sleep(250);
            Input.Mouse.LeftButtonClick();
        }

        public void Click()
        {
            Click((Right-Left)/2, (Bottom-Top)/2);
        }
    }
}
