namespace Aurora4xAutomation.Common
{
    public class PrintSettings
    {
        public int[] Widths;
        public string[] Titles;

        public static PrintSettings NewResearchTable
        {
            get
            {
                return new PrintSettings
                {
                    Widths = new[] { 63, 12 },
                    Titles = new[] { "Technology", "RPs" }
                };
            }
        }

        public static PrintSettings AvailableScientistTable
        {
            get
            {
                return new PrintSettings
                {
                    Widths = new[] { 55, 8, 6, 6 },
                    Titles = new[] { "Scientist", "Spec", "Bonus", "Max" }
                };
            }
        }
    }
}
