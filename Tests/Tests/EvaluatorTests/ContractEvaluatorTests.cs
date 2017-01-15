using NSubstitute;
using NUnit.Framework;
using Server.Evaluators;
using Server.IO;

namespace Tests.Tests.EvaluatorTests
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
