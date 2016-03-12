using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aurora4xAutomation.Common
{
    public static class StringExtensions
    {
        public static bool Matches(this string str, string regex)
        {
            return new Regex(regex).IsMatch(str);
        }

        public static string MaxWidth(this string str, int width)
        {
            return str.Substring(0, Math.Min(str.Length, width));
        }
    }
}
