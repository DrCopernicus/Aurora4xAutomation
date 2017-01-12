using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.IO;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class SetPopulationEvaluatorTests
    {
        [Test]
        public void SetsMiningDestination()
        {
            
        }

        [Test]
        public void HasHelpText()
        {
            Assert.DoesNotThrow(() =>
            {
                var x = new SetPopulationEvaluator("", Substitute.For<IUIMap>()).Help;
            });
        }
    }
}
