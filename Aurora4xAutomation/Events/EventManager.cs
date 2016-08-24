using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.Settings;

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

                Sleeper.Sleep(1000);
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

            if (SettingsStore.DatabasePassword == null)
            {
                UIMap.EventWindow.MakeActive();
                EventParser.ParseUsingEventWindow(UIMap.SystemMap.GetTime());
            }
            else
                EventParser.ParseUsingDatabase();
        }

    }
}
