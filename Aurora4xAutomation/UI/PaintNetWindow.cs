using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora4xAutomation.UI
{
    public class PaintNetWindow : Window
    {
        public PaintNetWindow()
            : base("Untitled")
        {

        }

        public void Paint(int x, int y)
        {
            Click(x, y);
        }
    }
}
