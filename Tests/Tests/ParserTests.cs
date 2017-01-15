using NSubstitute;
using NUnit.Framework;
using Server.Command.Parser;
using Server.Evaluators.Message;
using Server.Events;
using Server.IO;
using Server.Messages;
using Server.Settings;

namespace Tests.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void HelpCommandHasCorrectEvaluatorLabels()
        {
            var commandParser = new CommandParser(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), Substitute.For<IMessageManager>(),
                Substitute.For<IEventManager>());
            var evaluator = commandParser.Parse("help help");

            Assert.AreEqual("help", evaluator.Text);
            Assert.AreEqual("help", evaluator.Body.Text);
        }

        [Test]
        public void HelpCommandHasCorrectTypes()
        {
            var commandParser = new CommandParser(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), Substitute.For<IMessageManager>(),
                Substitute.For<IEventManager>());
            var evaluator = commandParser.Parse("help help");
            
            Assert.AreEqual(typeof(HelpEvaluator), evaluator.GetType());
            Assert.AreEqual(typeof(HelpEvaluator), evaluator.Body.GetType());
        }

        [Test]
        public void IncorrectCommandWithoutParametersReturnsLogCommand()
        {
            var commandParser = new CommandParser(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), Substitute.For<IMessageManager>(),
                Substitute.For<IEventManager>());
            var evaluator = commandParser.Parse("thiscommanddoesnotexist");

            Assert.AreEqual(typeof(LogEvaluator), evaluator.GetType());
        }

        [Test]
        public void IncorrectCommandWithOneParameterReturnsLogCommand()
        {
            var commandParser = new CommandParser(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), Substitute.For<IMessageManager>(),
                Substitute.For<IEventManager>());
            var evaluator = commandParser.Parse("thiscommanddoesnotexist parameter1");

            Assert.AreEqual(typeof(LogEvaluator), evaluator.GetType());
        }

        [Test]
        public void IncorrectCommandWithFiveParametersReturnsLogCommand()
        {
            var commandParser = new CommandParser(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), Substitute.For<IMessageManager>(),
                Substitute.For<IEventManager>());
            var evaluator = commandParser.Parse("thiscommanddoesnotexist parameter1 parameter2 parameter3 parameter4 parameter5");

            Assert.AreEqual(typeof(LogEvaluator), evaluator.GetType());
        }
    }
}
