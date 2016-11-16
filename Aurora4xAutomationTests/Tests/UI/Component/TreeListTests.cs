using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.OCR;
using Aurora4xAutomation.IO.UI.Controls;
using Aurora4xAutomation.IO.UI.Display;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Aurora4xAutomationTests.Tests.UI.Component
{
    [TestFixture]
    public class TreeListTests
    {
        private class TestEmptyViewInputDevice : HijackableInputDevice
        {
            public TestEmptyViewInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                Screenshot.CurrentScreen = Properties.Resources.prodpop_empty;
            }
        }

        private class TestCategoriesViewInputDevice : HijackableInputDevice
        {
            private bool _populatedSystemsExpanded = true;
            private bool _solExpanded = true;

            public TestCategoriesViewInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                Screenshot.CurrentScreen = Properties.Resources.prodpop_categories_step1;
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 4, 4, 12, 12))
                    _populatedSystemsExpanded = !_populatedSystemsExpanded;
                else if (Within(x, y, 21, 20, 29, 28) && _populatedSystemsExpanded)
                    _solExpanded = !_solExpanded;
                else
                    throw new Exception(string.Format("incorrectly clicked at ({0},{1})", x, y));

                if (_populatedSystemsExpanded && _solExpanded)
                    Screenshot.CurrentScreen = Properties.Resources.prodpop_categories_step4;
                if (_populatedSystemsExpanded && !_solExpanded)
                    Screenshot.CurrentScreen = Properties.Resources.prodpop_categories_step3;
                if (!_populatedSystemsExpanded)
                    Screenshot.CurrentScreen = Properties.Resources.prodpop_categories_step2;
            }
        }

        private class TestSimpleViewInputDevice : HijackableInputDevice
        {
            private bool _populatedSystemsExpanded = true;
            private bool _solExpanded = true;

            public TestSimpleViewInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                Screenshot.CurrentScreen = Properties.Resources.prodpop_simple_step1;
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 4, 4, 12, 12))
                    _populatedSystemsExpanded = !_populatedSystemsExpanded;
                else if (Within(x, y, 21, 20, 29, 28) && _populatedSystemsExpanded)
                    _solExpanded = !_solExpanded;
                else
                    throw new Exception(string.Format("incorrectly clicked at ({0},{1})", x, y));

                if (_populatedSystemsExpanded && _solExpanded)
                    Screenshot.CurrentScreen = Properties.Resources.prodpop_simple_step4;
                if (_populatedSystemsExpanded && !_solExpanded)
                    Screenshot.CurrentScreen = Properties.Resources.prodpop_simple_step3;
                if (!_populatedSystemsExpanded)
                    Screenshot.CurrentScreen = Properties.Resources.prodpop_simple_step2;
            }
        }

        [Test]
        public void CorrectlyReadsEmptyTreeList()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new TestEmptyViewInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var treelist = new TreeList(screen, inputDevice, ocrReader, 0, 707, 0, 340);

            Assert.AreEqual("No children.\n", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsSamplePopulatedSystemsCategoriesViewTreeList()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new TestCategoriesViewInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var treelist = new TreeList(screen, inputDevice, ocrReader, 0, 707, 0, 340);

            Assert.AreEqual("+PopulatedSstems\n+AutomatedMiningColonies\n+CivilianMiningColonies\n+ListeningPosts\n+ArcheologicalDigs\n+TerraformingSites\n+OtherColonies\n", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsSamplePopulatedSystemsSimpleViewTreeList()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new TestSimpleViewInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var treelist = new TreeList(screen, inputDevice, ocrReader, 0, 707, 0, 340);

            Assert.AreEqual("+PopulatedSstems\n +Sol(500m)\n  +Earth-Human(Capital):500m\n", treelist.Text);
        }
    }
}
