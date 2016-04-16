using System;
using System.Text.RegularExpressions;

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

        public static bool SimilarTo(this string str, string other)
        {
            string a = str.Replace(" ", "").Replace("I", "l").ToLower().Trim();
            string b = other.Replace(" ", "").Replace("I", "l").ToLower().Trim();

            return a == b;
        }

        public static bool SimilarContains(this string str, string other)
        {
            string a = str.Replace(" ", "").Replace("I", "l").ToLower().Trim();
            string b = other.Replace(" ", "").Replace("I", "l").ToLower().Trim();

            return a.Contains(b);
        }
    }
}
