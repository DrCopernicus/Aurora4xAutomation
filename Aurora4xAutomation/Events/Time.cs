using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aurora4xAutomation.Events
{
    public class Time
    {
        private static readonly Dictionary<string, int> MonthStringToInt = new Dictionary<string, int>
        {
            {"january", 1},
            {"february", 2},
            {"march", 3},
            {"april", 4},
            {"may", 5},
            {"june", 6},
            {"july", 7},
            {"august", 8},
            {"september", 9},
            {"october", 10},
            {"november", 11},
            {"december", 12}
        };

        public Time()
        {
            Unparsed = "";
            Year = 0;
            Month = 0;
            Day = 0;
            Hour = 0;
            Minute = 0;
            Second = 0;
        }

        public Time(int year, int month, int day, int hour, int minute, int second)
        {
            Unparsed = "";
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public Time(string unparsed)
        {
            Unparsed = unparsed;
            var groups = new Regex("([0-9]+)(st|nd|rd|th) ([a-zA-Z]+) ([0-9]+) ([0-9]+):([0-9]+)(:([0-9]+))?").Match(unparsed).Groups;
            Year = int.Parse(groups[4].Value);
            Month = MonthStringToInt[groups[3].Value.ToLower()];
            Day = int.Parse(groups[1].Value);
            Hour = int.Parse(groups[5].Value);
            Minute = int.Parse(groups[6].Value);
            if (groups[7].Value != "")
                Second = int.Parse(groups[8].Value);
        }

        public string Unparsed;
        public int Year;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;

        public static bool operator >(Time left, Time right)
        {
            if (left.Year > right.Year)
                return true;
            if (left.Year < right.Year)
                return false;

            if (left.Month > right.Month)
                return true;
            if (left.Month < right.Month)
                return false;

            if (left.Day > right.Day)
                return true;
            if (left.Day < right.Day)
                return false;

            if (left.Hour > right.Hour)
                return true;
            if (left.Hour < right.Hour)
                return false;

            if (left.Minute > right.Minute)
                return true;
            if (left.Minute < right.Minute)
                return false;

            if (left.Second > right.Second)
                return true;

            return false;
        }

        public static bool operator <(Time left, Time right)
        {
            if (left.Year < right.Year)
                return true;
            if (left.Year > right.Year)
                return false;

            if (left.Month < right.Month)
                return true;
            if (left.Month > right.Month)
                return false;

            if (left.Day < right.Day)
                return true;
            if (left.Day > right.Day)
                return false;

            if (left.Hour < right.Hour)
                return true;
            if (left.Hour > right.Hour)
                return false;

            if (left.Minute < right.Minute)
                return true;
            if (left.Minute > right.Minute)
                return false;

            if (left.Second < right.Second)
                return true;

            return false;
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Year == right.Year
                   && left.Month == right.Month
                   && left.Day == right.Day
                   && left.Hour == right.Hour
                   && left.Minute == right.Minute
                   && left.Second == right.Second;
        }

        public static bool operator !=(Time left, Time right)
        {
            return left.Year != right.Year
                   || left.Month != right.Month
                   || left.Day != right.Day
                   || left.Hour != right.Hour
                   || left.Minute != right.Minute
                   || left.Second != right.Second;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left < right || left == right;
        }

        public static Time operator +(Time left, Time right)
        {
            var seconds = left.Second + right.Second;
            var minutes = left.Minute + right.Minute + seconds/60;
            seconds -= (seconds / 60)*60;
            var hours = left.Hour + right.Hour + minutes / 60;
            minutes -= (minutes / 60) * 60;
            var days = left.Day + right.Day + hours / 24;
            hours -= (hours / 24)*24;
            var months = left.Month + right.Month + (days - 1) / 30;
            days -= ((days-1)/30)*30;
            var years = left.Year + right.Year + (months - 1) / 12;
            months -= ((months - 1)/12)*12;

            return new Time(years, months, days, hours, minutes, seconds);
        }
    }
}
