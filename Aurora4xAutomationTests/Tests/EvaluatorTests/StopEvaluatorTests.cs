using System;
using System.Collections.Generic;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class StopEvaluatorTests
    {
        private class TestSettingsStore : ISettingsStore
        {
            public bool Stopped { get; set; }
            public bool AutoTurnsOn { get; set; }
            public string DatabaseLocation { get; private set; }
            public string DatabasePassword { get; private set; }
            public string EventLogLocation { get; private set; }
            public int RaceId { get; set; }
            public Dictionary<string, string> Research { get; set; }
            public Dictionary<string, Dictionary<string, string>> ResearchFocuses { get; private set; }
            public int GameId { get; set; }
            public IncrementLength Increment { get; set; }
        }

        [Test]
        public void SetsTheStoppedFlagToTrue()
        {
            var settings = new TestSettingsStore();
            settings.Stopped = false;
            var evaluator = new StopEvaluator("evaluator", settings);

            evaluator.Execute();

            Assert.AreEqual(true, settings.Stopped);
        }

        [Test]
        public void KeepsTheStoppedFlagAsTrue()
        {
            var settings = new TestSettingsStore();
            settings.Stopped = true;
            var evaluator = new StopEvaluator("evaluator", settings);

            evaluator.Execute();

            Assert.AreEqual(true, settings.Stopped);
        }

        [Test]
        public void ThrowsIfAnyParameters()
        {
            var settings = new TestSettingsStore();
            settings.Stopped = true;
            var evaluator = new StopEvaluator("evaluator", settings);
            new EvaluatorParameterizer().SetParameters(evaluator, true);
            
            Assert.Throws<TooManyParametersException>(() => evaluator.Execute());
        }
    }
}
