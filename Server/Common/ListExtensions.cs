using System.Linq;

namespace Server.Common
{
    public static class ListExtensions
    {
        public static T[] Subset<T>(this T[] array, int start)
        {
            return array.Subset(start, array.Count() - 1);
        }

        public static T[] Subset<T>(this T[] array, int start, int end)
        {
            var subset = new T[end - start + 1];

            for (int i = start, j = 0; i <= end; i++, j++)
                subset[j] = array[i];

            return subset;
        }
    }
}
