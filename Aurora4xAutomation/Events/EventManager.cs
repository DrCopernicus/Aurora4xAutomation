using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Command.Evaluators;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Settings;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Events
{
    public class EventManager
    {
        public void Begin()
        {
            while (true)
            {
                ActOnActiveTimelineEntries();

                if (!SettingsStore.Stopped)
                {
                    ParseEvents();

                    if (!SettingsStore.Stopped)
                        TurnCommands.AdvanceTurn(this, EventArgs.Empty);

                    if (!SettingsStore.AutoTurnsOn)
                        SettingsCommands.Stop();
                }
                else
                {
                    SettingsStore.StatusMessage = "Waiting for user input";
                }

                Sleeper.Sleep(2000);
            }
        }

        private void ActOnActiveTimelineEntries()
        {
            SettingsStore.StatusMessage = "Evaluating events";
            Evaluator ev;
            while ((ev = Timeline.PopNextActiveEvent(new Time(UIMap.SystemMap.GetTime()))) != null)
                ev.Execute();
        }

        private void ParseEvents()
        {
            SettingsStore.StatusMessage = "Parsing events log";
            UIMap.EventWindow.MakeActive();
            if (SettingsStore.DatabasePassword == null)
                EventParser.ParseUsingEventWindow(UIMap.SystemMap.GetTime());
            else
                EventParser.ParseUsingDatabase();
        }

    }
}
