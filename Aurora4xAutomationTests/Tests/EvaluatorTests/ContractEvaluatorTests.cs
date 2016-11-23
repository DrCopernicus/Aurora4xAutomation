using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.IO;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class ContractEvaluatorTests
    {
        [Test]
        public void HasHelpText()
        {
            Assert.DoesNotThrow(() =>
            {
                var x = new ContractEvaluator("", Substitute.For<IUIMap>()).Help;
            });
        }
    }
}
