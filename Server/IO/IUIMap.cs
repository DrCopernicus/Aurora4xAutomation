using Server.Events;
using Server.IO.UI.Windows;

namespace Server.IO
{
    public interface IUIMap
    {
        BaseAuroraWindow BaseAuroraWindow { get; }
        EventWindow Events { get; }
        CommandersWindow Leaders { get; }
        SystemMapWindow SystemMap { get; }
        TaskGroupsWindow TaskGroups { get; }
        PopulationAndProductionWindow PopulationAndProduction { get; }

        Time GetTime();
    }
}
