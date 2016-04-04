﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aurora4xAutomation
{
    public class EventParser
    {
        public enum AuroraEventType
        {
            NonStopping,
            Research,
            MineralsLocated,
            Unspecified
        }

        public struct AuroraEvent
        {
            public AuroraEventType Type;
            public string Text;
            public string Args;
        }

        public static List<AuroraEvent> GetAllEvents(string time)
        {
            var file = @"D:\prog\Aurora\Aurora_latest\Aurora\FederationEventLog.txt";
            var allEvents = File.ReadAllLines(file);
            if (!GetLatestTime(allEvents).StartsWith(time))
                return new List<AuroraEvent>();
            return GetLatestEvents(allEvents).Select(GetEvent).ToList();
        }

        private static List<string> GetLatestEvents(string[] strs)
        {
            var list = new List<string>();

            string latestTime = null;
            for (int i = strs.Length - 1; i >= 0; i--)
            {
                var split = strs[i].Split(new[] {','}, 3);

                if (latestTime == null)
                    latestTime = split[0];

                if (split[0].Equals(latestTime))
                    list.Add(split[2]);
                else
                    break;
            }

            return list;
        }

        private static string GetLatestTime(string[] strs)
        {
            return strs.Last().Split(',').First();
        }

        private static AuroraEvent GetEvent(string str)
        {
            var ev = new AuroraEvent { Type = AuroraEventType.NonStopping, Text = str, Args = "" };

            if (IsMiningColonyUpgrade(str)
                || IsExperienceGain(str)
                || IsTrainingGain(str)
                || IsNewCivilianShip(str)
                || IsPromotion(str)
                || IsNavalOfficerCorpsJoin(str)
                || IsArmyOfficerCorpsJoin(str)
                || IsCivilianAdministrationJoin(str)
                || IsOfficerAssignment(str)
                || IsUnrestDecreasing(str)
                || IsUnrestRising(str)
                || IsCivilianShipScrapped(str)
                || IsSupplyHasBeenExhausted(str)
                || IsBonusIncrease(str)
                || IsMedicalProblem(str)
                || IsSevereMedicalProblemRetirementNoAssignment(str)
                || IsRetirementNoAssignment(str)
                || IsHasBeenKilledInAnAccident(str)
                || IsAllMineralsHaveBeenExhausted(str)
                || IsDeemedSurplus(str)
                || IsNewJumpPoint(str)
                || IsCrewMoraleFalling(str)
                || IsDeathNoAssignment(str)
                || IsMineralsLocated(str))
                return ev;

            if (IsResearchCompleted(str))
            {
                ev.Type = AuroraEventType.Research;
            }
            else if (IsMineralsLocated(str))
            {
                ev.Type = AuroraEventType.MineralsLocated;
                var workingStr = str.Replace("Minerals Discovered on ", "");
                ev.Args = workingStr.Remove(workingStr.IndexOf(": ", StringComparison.InvariantCulture));
            }
            else
            {
                ev.Type = AuroraEventType.Unspecified;
            }

            return ev;
        }

        private static bool IsResearchCompleted(string str)
        {
            var regex = new Regex(@"^A team on [a-zA-Z0-9\- ]* led by [a-zA-Z0-9\- ]* has completed research into ");
            return regex.IsMatch(str);
        }

        private static bool IsMineralsLocated(string str)
        {
            var regex = new Regex(@"^Minerals Discovered on [a-zA-Z0-9\- ]+");
            return regex.IsMatch(str);
        }

        private static bool IsNewCivilianShip(string str)
        {
            var regex = new Regex(@"^[a-zA-Z0-9\- ]* has launched a new ");
            return regex.IsMatch(str);
        }

        private static bool IsTrainingGain(string str)
        {
            var regex = new Regex(@"^Through training or experience");
            return regex.IsMatch(str);
        }

        private static bool IsBonusIncrease(string str)
        {
            var regex = new Regex(@"^The [a-zA-Z\- ]* of [a-zA-Z0-9\- ]* has increased to [0-9]*%");
            return regex.IsMatch(str);
        }

        private static bool IsExperienceGain(string str)
        {
            var regex = new Regex(@"^Through experience as a project leader");
            return regex.IsMatch(str);
        }

        private static bool IsPromotion(string str)
        {
            var regex = new Regex(@"has been promoted");
            return regex.IsMatch(str);
        }

        private static bool IsNavalOfficerCorpsJoin(string str)
        {
            var regex = new Regex(@"has joined your naval officer corps");
            return regex.IsMatch(str);
        }

        private static bool IsArmyOfficerCorpsJoin(string str)
        {
            var regex = new Regex(@"has joined your army officer corps");
            return regex.IsMatch(str);
        }

        private static bool IsCivilianAdministrationJoin(string str)
        {
            var regex = new Regex(@"has joined your civilian administration");
            return regex.IsMatch(str);
        }

        private static bool IsOfficerAssignment(string str)
        {
            var regex = new Regex(@"has been assigned to");
            return regex.IsMatch(str);
        }

        private static bool IsMiningColonyUpgrade(string str)
        {
            var regex = new Regex(@"^The civilian mining colony on [a-zA-Z0-9\- ]* has been expanded to [0-9]* civilian mining complexes");
            return regex.IsMatch(str);
        }

        private static bool IsUnrestDecreasing(string str)
        {
            var regex = new Regex(@"^As there is no longer any cause for unrest");
            return regex.IsMatch(str);
        }

        private static bool IsUnrestRising(string str)
        {
            var regex = new Regex(@"^Unrest is rising on");
            return regex.IsMatch(str);
        }

        private static bool IsCivilianShipScrapped(string str)
        {
            var regex = new Regex(@"has scrapped [a-zA-Z0-9\- \(\)]* due to (its )*(age|replacement by a newer vessel)");
            return regex.IsMatch(str);
        }

        private static bool IsSupplyHasBeenExhausted(string str)
        {
            var regex = new Regex(@"^The supply of [a-zA-Z0-9 ]* on [a-zA-Z0-9\- ]* has been exhausted");
            return regex.IsMatch(str);
        }

        private static bool IsMedicalProblem(string str)
        {
            var regex = new Regex(@"has developed a (significant |serious )*medical problem that will affect his long term health");
            return regex.IsMatch(str);
        }

        private static bool IsSevereMedicalProblemRetirementNoAssignment(string str)
        {
            var regex = new Regex(@"^[a-zA-Z0-9\- ]* has developed a severe medical problem that has forced (him|her) to retire. Assignment prior to retirement: (None|Unassigned)");
            return regex.IsMatch(str);
        }

        private static bool IsDeathNoAssignment(string str)
        {
            var regex = new Regex(@"^[a-zA-Z0-9\- ]* has died of natural causes. Assignment prior to death: (None|Unassigned)");
            return regex.IsMatch(str);
        }

        private static bool IsRetirementNoAssignment(string str)
        {
            var regex = new Regex(@"^[a-zA-Z0-9\- ]* has reached the retirement age of [0-9]* and has no current assignment.");
            return regex.IsMatch(str);
        }

        private static bool IsHasBeenKilledInAnAccident(string str)
        {
            var regex = new Regex(@"has been killed in an accident");
            return regex.IsMatch(str);
        }

        private static bool IsAllMineralsHaveBeenExhausted(string str)
        {
            var regex = new Regex(@"^The supplies of all minerals on [a-zA-Z0-9\- ]* have been exhausted.");
            return regex.IsMatch(str);
        }

        private static bool IsDeemedSurplus(string str)
        {
            var regex = new Regex(@"has been deemed surplus to requirements and released from the service");
            return regex.IsMatch(str);
        }

        private static bool IsNewJumpPoint(string str)
        {
            var regex = new Regex(@"New Jump Point found");
            return regex.IsMatch(str);
        }

        private static bool IsCrewMoraleFalling(string str)
        {
            var regex = new Regex(@"^Crew morale on board [a-zA-Z0-9\- ]* has fallen to ");
            return regex.IsMatch(str);
        }
    }
}
