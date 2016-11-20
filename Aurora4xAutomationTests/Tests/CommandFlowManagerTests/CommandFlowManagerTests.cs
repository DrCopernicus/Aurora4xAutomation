using Aurora4xAutomation.Automation;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.CommandFlowManagerTests
{
    [TestFixture]
    public class CommandFlowManagerTests
    {
        [Test]
        public void QueuesPrintEvaluator()
        {
            var settings = Substitute.For<ISettingsStore>();
            var uiMap = Substitute.For<IUIMap>();
            var messages = Substitute.For<IMessageManager>();
            var eventManager = Substitute.For<IEventManager>();
            var logger = Substitute.For<ILogger>();
            var commandFlowManager = new CommandFlowManager(settings, uiMap, messages, eventManager, logger);

            commandFlowManager.QueueCommand("print abcdefg");

            eventManager.Received(1).AddEvent(Arg.Any<PrintEvaluator>(), Arg.Any<Time>());
        }

        [Test]
        public void QueuesLogEvaluatorIfUnrecognizedEvaluator()
        {
            var settings = Substitute.For<ISettingsStore>();
            var uiMap = Substitute.For<IUIMap>();
            var messages = Substitute.For<IMessageManager>();
            var eventManager = Substitute.For<IEventManager>();
            var logger = Substitute.For<ILogger>();
            var commandFlowManager = new CommandFlowManager(settings, uiMap, messages, eventManager, logger);

            commandFlowManager.QueueCommand("nonexistantevaluator abcd efgh ijkl mnop");

            eventManager.Received(1).AddEvent(Arg.Any<LogEvaluator>(), Arg.Any<Time>());
        }
    }
}
