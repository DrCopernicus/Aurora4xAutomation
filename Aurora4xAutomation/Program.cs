using Aurora4xAutomation.Automation;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.REST;
using Aurora4xAutomation.Settings;
using System.Threading;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.OCR;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Display;

namespace Aurora4xAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
        }

        public Program()
        {
            var logger = new Logger();
            var settings = new SettingsStore();
            var uiMap = new UIMap(settings, new WindowFinder(), new OCRReader(new OCRSplitter()), new InputDevice(), new Screen(new ScreenDataRetriever(new Sleeper(), new ScreenshotCapturer())));
            var messages = new MessageManager();
            var eventManager = new EventManager(uiMap, settings, messages);

            new Thread(new CommandFlowManager(settings, uiMap, messages, eventManager, logger).Begin).Start();
            RESTManager.Begin();
        }
    }
}
