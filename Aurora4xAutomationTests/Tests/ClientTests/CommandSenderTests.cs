using Aurora4xAutomationClient.ClientUI;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class CommandSenderTests
    {
        [Test]
        public void PromptsUserForInputInConsole()
        {
            var console = Substitute.For<IConsole>();
            var commandSender = new CommandSender(console);

            console.Received().WriteLine(Arg.Any<string>());
        }
    }
}
