namespace Client.Terminal
{
    public interface IConsole
    {
        void WriteToCurrentLine(string message);
        void WriteToBuffer(string message);

        string ReadLine();
        int ReadCharacter();
    }
}
