using System.Windows.Forms;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.UI.Controls;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class CommandersWindow : Window
    {
        public CommandersWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, IOCRReader ocr, ISettingsStore settings) 
            : base("Commanders", screen, windowFinder, inputDevice, settings)
        {
            LeaderType = new Combobox(this, inputDevice, ocr, left: 101, right: 272, top: 140, bottom: 156)
            {
                CharacterOffset = 4,
                CharacterHeight = 9,
                Colors = new[] {new byte[] {0, 0, 0}}
            };

            Officiers = new TreeList(this, inputDevice, ocr, left: 29, right: 289, top: 176, bottom: 292)
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
            new BaseAuroraWindow(Screen, WindowFinder, InputDevice, Settings).MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("{F4}");
        }
    }
}
