using NSubstitute;
using NUnit.Framework;
using Server.Common.Exceptions;
using Server.Evaluators;
using Server.Evaluators.Helpers;
using Server.Settings;

namespace Tests.Tests.EvaluatorTests
{
    [TestFixture]
    public class StopEvaluatorTests
    {
        [Test]
        public void SetsTheStoppedFlagToTrue()
        {
            var settings = Substitute.For<ISettingsStore>();
            settings.Stopped = false;
            var evaluator = new StopEvaluator("evaluator", settings);

            evaluator.Execute();

            Assert.AreEqual(true, settings.Stopped);
        }

        [Test]
        public void KeepsTheStoppedFlagAsTrue()
        {
            var settings = Substitute.For<ISettingsStore>();
            settings.Stopped = true;
            var evaluator = new StopEvaluator("evaluator", settings);

            evaluator.Execute();

            Assert.AreEqual(true, settings.Stopped);
        }

        [Test]
        public void ThrowsIfAnyParameters()
        {
            var settings = Substitute.For<ISettingsStore>();
            settings.Stopped = true;
            var evaluator = new StopEvaluator("evaluator", settings);
            new EvaluatorParameterizer().SetParameters(evaluator, true);
            
            Assert.Throws<WrongParameterCountException>(() => evaluator.Execute());
        }

        [Test]
        public void HasHelpText()
        {
            Assert.DoesNotThrow(() =>
            {
                var x = new StopEvaluator("", Substitute.For<ISettingsStore>()).Help;
            });
        }
    }
}
