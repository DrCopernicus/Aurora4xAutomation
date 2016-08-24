using Aurora4xAutomation.IO.UI.Windows;

namespace Aurora4xAutomation.IO
{
    public interface IUIMap
    {
        EventWindow EventWindow { get; }
        CommandersWindow Leaders { get; }
        SystemMapWindow SystemMap { get; }
        TaskGroupsWindow TaskGroups { get; }
        PopulationAndProductionWindow PopulationAndProductionWindow { get; }
    }
}
