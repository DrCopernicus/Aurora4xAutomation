using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Evaluators.Factories;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class EvaluatorParamaterizerTests
    {

        [Test]
        public void AddsCorrectNumberOfParameters()
        {
            var headEvaluator = Substitute.For<Evaluator>(Arg.Any<string>());
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first", "second", "third");
            Assert.AreEqual(3, headEvaluator.Parameters.Count);
        }

        [Test]
        public void AddsOnlyOneParameter()
        {
            var headEvaluator = Substitute.For<Evaluator>(Arg.Any<string>());
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first");
            Assert.AreEqual(1, headEvaluator.Parameters.Count);
        }

        [Test]
        public void ParametersAreInOrder()
        {
            var headEvaluator = Substitute.For<Evaluator>(Arg.Any<string>());
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first", "second", "third");
            Assert.AreEqual("first", headEvaluator.Parameters[0]);
            Assert.AreEqual("second", headEvaluator.Parameters[1]);
            Assert.AreEqual("third", headEvaluator.Parameters[2]);
        }

        [Test]
        public void OnlyOneParameterVisible()
        {
            var headEvaluator = Substitute.For<Evaluator>(Arg.Any<string>());
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first");
            Assert.AreEqual("first", headEvaluator.Parameters[0]);
        }

        [Test]
        public void OverwritesParameters()
        {
            var headEvaluator = Substitute.For<Evaluator>(Arg.Any<string>());
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first");
            new EvaluatorParameterizer().SetParameters(headEvaluator, "second");
            Assert.AreEqual(1, headEvaluator.Parameters.Count);
            Assert.AreEqual("second", headEvaluator.Parameters[0]);
        }
    }
}
