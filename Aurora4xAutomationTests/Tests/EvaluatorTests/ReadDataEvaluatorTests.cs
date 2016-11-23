using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.IO;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class ReadDataEvaluatorTests
    {
        [Test]
        public void HasHelpText()
        {
            Assert.DoesNotThrow(() =>
            {
                var x = new ReadDataEvaluator("", Substitute.For<IUIMap>()).Help;
            });
        }
    }
}
