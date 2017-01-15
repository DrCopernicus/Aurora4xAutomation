namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public interface IConsoleWriter
    {
        int Read();
        string ReadLine();
        void Write(string message);
        void WriteLine(string message);
        void Clear();
    }
}