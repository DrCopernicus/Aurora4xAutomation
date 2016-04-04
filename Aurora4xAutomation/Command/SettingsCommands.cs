using System;

namespace Aurora4xAutomation.Command
{
    public static class SettingsCommands
    {
        public static void Stop(object sender, EventArgs e)
        {
            Settings.Stopped = true;
        }
    }
}