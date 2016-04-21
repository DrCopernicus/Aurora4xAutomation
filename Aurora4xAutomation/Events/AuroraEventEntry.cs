namespace Aurora4xAutomation.Events
{
    public class AuroraEventEntry
    {
        public AuroraEventEntry(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; private set; }
        public string Text { get; private set; }
    }
}
