using NSubstitute;
using NUnit.Framework;
using Server.Evaluators;
using Server.IO;

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
