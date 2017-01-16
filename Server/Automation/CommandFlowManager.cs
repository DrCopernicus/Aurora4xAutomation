using System.Collections.Generic;
using Server.Command.Parser;
using Server.Evaluators;
using Server.Events;
using Server.IO;
using Server.Messages;
using Server.REST;
using Server.Settings;

namespace Server.Automation
{
    public class CommandFlowManager
    {
        public CommandFlowManager(ISettingsStore settings, IUIMap uiMap, IMessageManager messageManager, IEventManager eventManager, ILogger logger)
        {
            _logger = logger;
            _settings = settings;
            _auroraUI = uiMap;
            _messages = messageManager;
            _eventManager = eventManager;
            _commandParser = new CommandParser(_auroraUI, _settings, _messages, _eventManager, _logger);

            RESTManager.CommandFlowManager = this;
        }

        public void QueueCommand(string command, Time time = null)
        {
            _eventManager.AddEvent(_commandParser.Parse(command), time);
        }

        public void QueueCommand(IEvaluator evaluator, Time time = null)
        {
            _eventManager.AddEvent(evaluator, time);
        }

        public void Begin()
        {
            _eventManager.Begin(_logger);
        }

        public void Stop()
        {
            _eventManager.Stop();
        }

        public List<string> GetMessages(long startId, long endId = long.MaxValue)
        {
            return _messages.GetMessagesAfterId(startId, endId);
        }

        public long GetLastMessageId()
        {
            return _messages.GetLastId();
        }

        private readonly ILogger _logger;
        private readonly IUIMap _auroraUI;
        private readonly ISettingsStore _settings;
        private readonly IMessageManager _messages;
        private readonly IEventManager _eventManager;
        private readonly CommandParser _commandParser;
    }
}
