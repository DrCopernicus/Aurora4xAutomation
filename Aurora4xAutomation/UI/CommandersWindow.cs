using System.Threading;
using System.Windows.Forms;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.UI.Controls;

namespace Aurora4xAutomation.UI
{
    public class CommandersWindow : Window
    {
        public CommandersWindow() 
            : base("Commanders")
        {
            LeaderType = new Combobox(this) { Left = 101, Right = 272, Top = 140, Bottom = 156, CharacterOffset = 4, CharacterHeight = 9, Colors = new[] { new byte[] { 0, 0, 0 } } };
            Officiers = new TreeList(this, 29, 289, 176, 292)
            {
                CharacterOffset = 2,
                BottomOffset = 2,
                CharacterHeight = 11
            };
        }

        public Combobox LeaderType { get; set; }
        public TreeList Officiers { get; set; }

        public void SetAutomatedAssignments(bool toggle)
        {
            if ((GetPixel(100, 88).EqualsColor(0, 0, 0) && !toggle)
                || GetPixel(100, 88).EqualsColor(255, 255, 255) && toggle)
            {
                Click(100, 88);
            }
        }

        protected override void OpenIfNotFound()
        {
            new AuroraWrapperWindow().OpenBase();
            Thread.Sleep(1000);
            SendKeys.SendWait("{F4}");
        }
    }
}
