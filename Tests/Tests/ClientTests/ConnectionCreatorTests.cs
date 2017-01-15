using Client.ClientUI.Client;
using Client.ClientUI.Terminal;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Tests.ClientTests
{
    [TestFixture]
    public class ConnectionCreatorTests
    {
        [Test]
        public void WritesPromptToConsoleForUserInput()
        {
            var console = Substitute.For<IConsole>();
            console.ReadLine().Returns("");
            var connectionCreator = new ConnectionCreator(console);

            connectionCreator.CreateClient();
            console.Received(1).WriteToBuffer(Arg.Any<string>());
        }

        [Test]
        public void CreatesClientWithCorrectLocalNetworkAddress()
        {
            var address = "http://192.168.1.2:1234";

            var console = Substitute.For<IConsole>();
            console.ReadLine().Returns(address);
            var connectionCreator = new ConnectionCreator(console);

            var client = connectionCreator.CreateClient();
            Assert.AreEqual(address, client.BaseUrl);
        }

        [Test]
        public void CreatesClientWithCorrectLocalhostAddress()
        {
            var address = "http://127.0.0.1:5823";

            var console = Substitute.For<IConsole>();
            console.ReadLine().Returns(address);
            var connectionCreator = new ConnectionCreator(console);

            var client = connectionCreator.CreateClient();
            Assert.AreEqual(address, client.BaseUrl);
        }

        [Test]
        public void CreatesClientWithCorrectRemoteAddress()
        {
            var address = "http://74.125.224.72:105";

            var console = Substitute.For<IConsole>();
            console.ReadLine().Returns(address);
            var connectionCreator = new ConnectionCreator(console);

            var client = connectionCreator.CreateClient();
            Assert.AreEqual(address, client.BaseUrl);
        }
    }
}
