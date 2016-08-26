﻿using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO.UI.Windows;

namespace Aurora4xAutomation.IO
{
    public interface IUIMap
    {
        BaseAuroraWindow BaseAuroraWindow { get; }
        EventWindow EventWindow { get; }
        CommandersWindow Leaders { get; }
        SystemMapWindow SystemMap { get; }
        TaskGroupsWindow TaskGroups { get; }
        PopulationAndProductionWindow PopulationAndProductionWindow { get; }

        Time GetTime();
    }
}
