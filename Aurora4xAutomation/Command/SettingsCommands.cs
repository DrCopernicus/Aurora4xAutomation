using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Command
{
    public static class SettingsCommands
    {
        public static void Stop(object sender, EventArgs e)
        {
            SettingsStore.Stopped = true;
        }
    }
}