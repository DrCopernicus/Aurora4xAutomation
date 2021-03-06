﻿using NSubstitute;
using NUnit.Framework;
using Server.Evaluators;
using Server.Evaluators.Helpers;

namespace Tests.Tests.EvaluatorTests.Factories
{
    [TestFixture]
    public class EvaluatorParamaterizerTests
    {
        [Test]
        public void AddsOneParameter()
        {
            var headEvaluator = Substitute.For<IEvaluator>();
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first");

            headEvaluator.Received(1).Body = Arg.Any<ParameterEvaluator>();
            Assert.AreEqual("first", headEvaluator.Body.Text);
            Assert.AreEqual(null, headEvaluator.Body.Next);
        }

        [Test]
        public void AddsTwoParameters()
        {
            var headEvaluator = Substitute.For<IEvaluator>();
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first", "second");

            headEvaluator.Received(1).Body = Arg.Any<ParameterEvaluator>();
            Assert.AreEqual("first", headEvaluator.Body.Text);
            Assert.AreEqual("second", headEvaluator.Body.Next.Text);
            Assert.AreEqual(null, headEvaluator.Body.Next.Next);
        }

        [Test]
        public void AddsThreeParameters()
        {
            var headEvaluator = Substitute.For<IEvaluator>();
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first", "second", "third");

            headEvaluator.Received(1).Body = Arg.Any<ParameterEvaluator>();
            Assert.AreEqual("first", headEvaluator.Body.Text);
            Assert.AreEqual("second", headEvaluator.Body.Next.Text);
            Assert.AreEqual("third", headEvaluator.Body.Next.Next.Text);
            Assert.AreEqual(null, headEvaluator.Body.Next.Next.Next);
        }

        [Test]
        public void OverwritesParameters()
        {
            var headEvaluator = Substitute.For<IEvaluator>();
            new EvaluatorParameterizer().SetParameters(headEvaluator, "first");
            new EvaluatorParameterizer().SetParameters(headEvaluator, "second");

            headEvaluator.Received(2).Body = Arg.Any<ParameterEvaluator>();
            Assert.AreEqual("second", headEvaluator.Body.Text);
            Assert.AreEqual(null, headEvaluator.Body.Next);
        }
    }
}
