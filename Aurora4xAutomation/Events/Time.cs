using System;
using System.Text.RegularExpressions;

namespace Aurora4xAutomation.Events
{
    public class Time
    {
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
            Month = groups[3].Value == "January"
                ? 1
                : groups[3].Value == "February"
                    ? 2
                    : groups[3].Value == "March"
                        ? 3
                        : groups[3].Value == "April"
                            ? 4
                            : groups[3].Value == "May"
                                ? 5
                                : groups[3].Value == "June"
                                    ? 6
                                    : groups[3].Value == "July"
                                        ? 7
                                        : groups[3].Value == "August"
                                            ? 8
                                            : groups[3].Value == "September"
                                                ? 9
                                                : groups[3].Value == "October"
                                                    ? 10
                                                    : groups[3].Value == "November"
                                                        ? 11
                                                        : groups[3].Value == "December"
                                                            ? 12 : -1;
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
