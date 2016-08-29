using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.REST;
using Aurora4xAutomation.Settings;
using System.Collections.Generic;

namespace Aurora4xAutomation.Automation
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
            _commandParser = new CommandParser(_auroraUI, _settings, _messages, _eventManager);

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
