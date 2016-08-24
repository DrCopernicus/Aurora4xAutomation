using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class PaintNetWindow : Window
    {
        public PaintNetWindow(SettingsStore settings)
            : base("Untitled", settings)
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
