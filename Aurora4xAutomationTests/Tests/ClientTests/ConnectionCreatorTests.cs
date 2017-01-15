using Aurora4xAutomationClient.ClientUI;
using Aurora4xAutomationClient.ClientUI.Client;
using Aurora4xAutomationClient.ClientUI.Console;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class ConnectionCreatorTests
    {
        private class TestConsole : IConsole
        {
            public TestConsole(string server)
            {
                _server = server;
            }

            public int Read()
            {
                throw new System.NotImplementedException();
            }

            public string ReadLine()
            {
                return _server;
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
            private readonly string _server;
        }

        [Test]
        public void WritesPromptToConsoleForUserInput()
        {
            var console = new TestConsole("http://192.168.1.2:1234");
            var connectionCreator = new ConnectionCreator(console);

            var client = connectionCreator.CreateClient();
            Assert.AreNotEqual("", console.CurrentConsoleScreen);
        }

        [Test]
        public void CreatesClientWithCorrectLocalNetworkAddress()
        {
            var address = "http://192.168.1.2:1234";
            var console = new TestConsole(address);
            var connectionCreator = new ConnectionCreator(console);

            var client = connectionCreator.CreateClient();

            Assert.AreEqual(address, client.BaseUrl);
        }

        [Test]
        public void CreatesClientWithCorrectLocalhostAddress()
        {
            var address = "http://127.0.0.1:5823";
            var console = new TestConsole(address);
            var connectionCreator = new ConnectionCreator(console);

            var client = connectionCreator.CreateClient();

            Assert.AreEqual(address, client.BaseUrl);
        }

        [Test]
        public void CreatesClientWithCorrectRemoteAddress()
        {
            var address = "http://74.125.224.72:105";
            var console = new TestConsole(address);
            var connectionCreator = new ConnectionCreator(console);

            var client = connectionCreator.CreateClient();

            Assert.AreEqual(address, client.BaseUrl);
        }
    }
}
