namespace Aurora4xAutomationClient.ClientUI.Console
{
    public interface IConsole
    {
        int Read();
        string ReadLine();
        void Write(string message);
        void WriteLine(string message);
    }
}
