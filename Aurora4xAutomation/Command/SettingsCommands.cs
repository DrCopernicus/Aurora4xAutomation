using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public static class SettingsCommands
    {
        public static void Stop()
        {
            SettingsStore.Stopped = true;
        }
    }
}