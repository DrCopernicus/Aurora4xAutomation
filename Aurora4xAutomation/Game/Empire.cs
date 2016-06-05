using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Game
{
    public class Empire
    {
        public Dirtyable<string> Name = new Dirtyable<string>(
            () =>
            {
                UIMap.PopulationAndProductionWindow.Empire.SelectOption(0);
                return UIMap.PopulationAndProductionWindow.Empire.Text;
            },
            x =>
            {
                UIMap.PopulationAndProductionWindow.Empire.Text = x;
            });
    }
}
