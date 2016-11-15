using System;
using System.Drawing;
using WindowsInput.Native;
using Aurora4xAutomation.IO.OCR;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class TreeListTests
    {
        private class HijackableScreenDataRetriever : IScreenDataRetriever
        {
            public Color GetPixel(int x, int y)
            {
                return CurrentScreen.GetPixel(x, y);
            }

            public Bitmap CurrentScreen { get; set; }
            public void Dirty()
            {

            }
        }

        private class TestCategoriesViewInputDevice : IInputDevice
        {
            private bool PopulatedSystemsExpanded { get; set; }
            private bool SolExpanded { get; set; }

            public TestCategoriesViewInputDevice()
            {
                PopulatedSystemsExpanded = true;
                SolExpanded = true;
            }

            public void Click(int x, int y, int wait)
            {
                if (Within(x, y, 4, 4, 12, 12))
                    PopulatedSystemsExpanded = !PopulatedSystemsExpanded;
                if (Within(x, y, 21, 20, 29, 28) && PopulatedSystemsExpanded)
                    SolExpanded = !SolExpanded;

                if (PopulatedSystemsExpanded && SolExpanded)
                    HijackableScreen.CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_categories_step4;
                if (PopulatedSystemsExpanded && !SolExpanded)
                    HijackableScreen.CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_categories_step3;
                if (!PopulatedSystemsExpanded)
                    HijackableScreen.CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_categories_step2;
            }

            public void SendKeys(string text)
            {
                throw new NotImplementedException();
            }

            public void PressKey(VirtualKeyCode key)
            {
                throw new NotImplementedException();
            }

            public HijackableScreenDataRetriever HijackableScreen { get; set; }

            private bool Within(int x, int y, int left, int top, int right, int bottom)
            {
                return x >= left && x <= right && y >= top && y <= bottom;
            }
        }

        private class TestSimpleViewInputDevice : IInputDevice
        {
            private bool PopulatedSystemsExpanded { get; set; }
            private bool SolExpanded { get; set; }

            public TestSimpleViewInputDevice()
            {
                PopulatedSystemsExpanded = true;
                SolExpanded = true;
            }

            public void Click(int x, int y, int wait)
            {
                if (Within(x, y, 4, 4, 12, 12))
                    PopulatedSystemsExpanded = !PopulatedSystemsExpanded;
                if (Within(x, y, 21, 20, 29, 28) && PopulatedSystemsExpanded)
                    SolExpanded = !SolExpanded;

                if (PopulatedSystemsExpanded && SolExpanded)
                    HijackableScreen.CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_simple_step4;
                if (PopulatedSystemsExpanded && !SolExpanded)
                    HijackableScreen.CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_simple_step3;
                if (!PopulatedSystemsExpanded)
                    HijackableScreen.CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_simple_step2;
            }

            public void SendKeys(string text)
            {
                throw new NotImplementedException();
            }

            public void PressKey(VirtualKeyCode key)
            {
                throw new NotImplementedException();
            }

            public HijackableScreenDataRetriever HijackableScreen { get; set; }

            private bool Within(int x, int y, int left, int top, int right, int bottom)
            {
                return x >= left && x <= right && y >= top && y <= bottom;
            }
        }

        private class TestEmptyScreenInputDevice : IInputDevice
        {
            public void Click(int x, int y, int wait)
            {
            }

            public void SendKeys(string text)
            {
                throw new NotImplementedException();
            }

            public void PressKey(VirtualKeyCode key)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void CorrectlyReadsEmptyTreeList()
        {
            var screenDataRetriever = new HijackableScreenDataRetriever { CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_empty };
            var inputDevice = new TestEmptyScreenInputDevice();
            var screen = new Screen(screenDataRetriever);

            var treelist = new TreeList(screen, inputDevice, new OCRReader(new OCRSplitter()), 0, 707, 0, 340);
            Assert.AreEqual("No children.\n", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsSamplePopulatedSystemsCategoriesViewTreeList()
        {
            var screenDataRetriever = new HijackableScreenDataRetriever { CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_categories_step1 };
            var inputDevice = new TestCategoriesViewInputDevice {HijackableScreen = screenDataRetriever};
            var screen = new Screen(screenDataRetriever);

            var treelist = new TreeList(screen, inputDevice, new OCRReader(new OCRSplitter()), 0, 707, 0, 340);
            Assert.AreEqual("+PopulatedSstems\n+AutomatedMiningColonies\n+CivilianMiningColonies\n+ListeningPosts\n+ArcheologicalDigs\n+TerraformingSites\n+OtherColonies\n", treelist.Text);
        }

        [Test]
        public void CorrectlyReadsSamplePopulatedSystemsSimpleViewTreeList()
        {
            var screenDataRetriever = new HijackableScreenDataRetriever { CurrentScreen = Aurora4xAutomation.Properties.Resources.prodpop_simple_step1 };
            var inputDevice = new TestSimpleViewInputDevice {HijackableScreen = screenDataRetriever};
            var screen = new Screen(screenDataRetriever);

            var treelist = new TreeList(screen, inputDevice, new OCRReader(new OCRSplitter()), 0, 707, 0, 340);
            Assert.AreEqual("+PopulatedSstems\n +Sol(500m)\n  +Earth-Human(Capital):500m\n", treelist.Text);
        }
    }
}
