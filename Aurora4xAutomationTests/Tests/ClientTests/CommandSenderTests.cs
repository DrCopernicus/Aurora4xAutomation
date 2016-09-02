using Aurora4xAutomationClient.ClientUI;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class CommandSenderTests
    {
        private class TestConsole : IConsole
        {
            public TestConsole()
            {
            }

            public int Read()
            {
                throw new System.NotImplementedException();
            }

            public string ReadLine()
            {
                return "";
            }

            public void Write(string message)
            {
                CurrentConsoleScreen += message;
            }

            public void WriteLine(string message)
            {
                Write(message + "\n");
            }

            public string CurrentConsoleScreen { get; private set; }
        }

        [Test]
        public void PromptsUserForInputInConsole()
        {
            var commandSender = new CommandSender(new TestConsole());
        }
    }
}
