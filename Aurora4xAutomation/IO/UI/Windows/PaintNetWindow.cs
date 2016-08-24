using System;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class PaintNetWindow : Window
    {
        public PaintNetWindow()
            : base("Untitled")
        {

        }

        public void Paint(int x, int y)
        {
            this.Click(x, y);
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Paint.Net not found!");
        }
    }
}
