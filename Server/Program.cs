﻿using Server.Automation;
using Server.Common;
using Server.Events;
using Server.IO;
using Server.IO.DB;
using Server.IO.OCR;
using Server.IO.UI;
using Server.IO.UI.Display;
using Server.Messages;
using Server.REST;
using Server.Settings;
using System.Threading;

namespace Server
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
            var executor = new QueryExecutor();
            var database = new AuroraDatabase(executor);
            var eventManager = new EventManager(uiMap, settings, messages, database, executor);

            new Thread(new CommandFlowManager(settings, uiMap, messages, eventManager, logger).Begin).Start();
            RESTManager.Begin();
        }
    }
}
