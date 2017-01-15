using NSubstitute;
using NUnit.Framework;
using Server.Common;
using Server.IO.OCR;
using Server.IO.UI.Controls;
using Server.IO.UI.Display;
using Tests.ScriptFramework;

namespace Tests.Tests.UI.Component
{
    [TestFixture]
    public class TreeListTests
    {
        private class EmptyViewInputDevice : HijackableInputDevice
        {
            public EmptyViewInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.prodpop_empty);
            }
        }

        private class CategoriesViewInputDevice : HijackableInputDevice
        {
            private bool _populatedSystemsExpanded = true;
            private bool _solExpanded = true;

            public CategoriesViewInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.prodpop_categories_step1);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 4, 12, 4, 12))
                    _populatedSystemsExpanded = !_populatedSystemsExpanded;
                else if (Within(x, y, 20, 28, 21, 29) && _populatedSystemsExpanded)
                    _solExpanded = !_solExpanded;
                else
                    base.Click(x, y, wait);

                if (_populatedSystemsExpanded && _solExpanded)
                    SetScreen(Properties.Resources.prodpop_categories_step4);
                if (_populatedSystemsExpanded && !_solExpanded)
                    SetScreen(Properties.Resources.prodpop_categories_step3);
                if (!_populatedSystemsExpanded)
                    SetScreen(Properties.Resources.prodpop_categories_step2);
            }
        }

        private class SimpleViewInputDevice : HijackableInputDevice
        {
            private bool _populatedSystemsExpanded = true;
            private bool _solExpanded = true;

            public SimpleViewInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.prodpop_simple_step1);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 4, 12, 4, 12))
                    _populatedSystemsExpanded = !_populatedSystemsExpanded;
                else if (Within(x, y, 20, 28, 21, 29) && _populatedSystemsExpanded)
                    _solExpanded = !_solExpanded;
                else
                    base.Click(x, y, wait);

                if (_populatedSystemsExpanded && _solExpanded)
                    SetScreen(Properties.Resources.prodpop_simple_step4);
                if (_populatedSystemsExpanded && !_solExpanded)
                    SetScreen(Properties.Resources.prodpop_simple_step3);
                if (!_populatedSystemsExpanded)
                    SetScreen(Properties.Resources.prodpop_simple_step2);
            }
        }

        [Test]
        public void CorrectlyReadsEmptyTreeList()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new EmptyViewInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var treelist = new TreeList(screen, inputDevice, ocrReader, 0, 707, 0, 340);

            Assert.AreEqual("No children.\n", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsSamplePopulatedSystemsCategoriesViewTreeList()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CategoriesViewInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var treelist = new TreeList(screen, inputDevice, ocrReader, 0, 707, 0, 340);

            Assert.AreEqual("+PopulatedSystems\n+AutomatedMiningColonies\n+CivilianMiningColonies\n+ListeningPosts\n+ArcheologicalDigs\n+TerraformingSites\n+OtherColonies\n", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsSamplePopulatedSystemsSimpleViewTreeList()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new SimpleViewInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));
            var ocrReader = new OCRReader(new OCRSplitter());

            var treelist = new TreeList(screen, inputDevice, ocrReader, 0, 707, 0, 340);

            Assert.AreEqual("+PopulatedSystems\n +Sol(500m)\n  +Earth-Human(Capital):500m\n", treelist.Text);
        }
    }
}
