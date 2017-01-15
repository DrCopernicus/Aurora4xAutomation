using System.Collections.Generic;

namespace Server.Common
{
    public static class TableExtensions
    {
        public static string Print(this List<string[]> table, PrintSettings settings)
        {
            var output = "";
            output += settings.Titles.PrintHeaders(settings.Widths);
            for (int i = 0; i < table.Count; i++)
            {
                var row = table[i];
                if (row[0] != "")
                    output += row.PrintRow(settings.Widths, i);
            }
            return output + "\n";
        }

        private static string PrintHeaders(this string[] headers, int[] widths)
        {
            var output = "=#==";
            for (int i = 0; i < headers.Length; i++)
            {
                output += "=" + headers[i].MaxWidth(widths[i]).PadRight(widths[i] - 1, '=');
            }
            return output + "\n";
        }

        private static string PrintRow(this string[] row, int[] widths, int rowNum)
        {
            var output = rowNum.ToString().PadLeft(2, ' ') + ": ";
            for (int i = 0; i < row.Length; i++)
            {
                output += row[i].MaxWidth(widths[i]).PadRight(widths[i], ' ');
            }
            return output + "\n";
        }
    }
}
