using NSubstitute;
using NUnit.Framework;
using Server.Evaluators;
using Server.IO;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class MoveEvaluatorTests
    {
        [Test]
        public void HasHelpText()
        {
            Assert.DoesNotThrow(() =>
            {
                var x = new MoveEvaluator("", Substitute.For<IUIMap>()).Help;
            });
        }
    }
}
