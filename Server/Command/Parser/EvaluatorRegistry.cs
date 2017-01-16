using Server.Evaluators;
using Server.Evaluators.Message;
using Server.Events;
using Server.IO;
using Server.Messages;
using Server.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Command.Parser
{
    public class EvaluatorRegistry
    {
        private IUIMap UIMap { get; set; }
        private ISettingsStore Settings { get; set; }
        private IMessageManager Messages { get; set; }
        private IEventManager EventManager { get; set; }

        private Dictionary<string, Func<IEvaluator>> _commands;

        public EvaluatorRegistry(IUIMap uiMap, ISettingsStore settings, IMessageManager messages, IEventManager events)
        {
            UIMap = uiMap;
            Settings = settings;
            Messages = messages;
            EventManager = events;

            _commands = new Dictionary<string, Func<IEvaluator>>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            Register("adv", () => new AdvanceEvaluator("adv", Settings));
            Register("build-installation", () => new BuildInstallationEvaluator("build-installation", UIMap));
            Register("contract", () => new ContractEvaluator("contract", UIMap));
            Register("help", () => new HelpEvaluator("help", Messages));
            Register("move", () => new MoveEvaluator("move", UIMap));
            Register("open", () => new OpenWindowEvaluator("open", UIMap));
            Register("print", () => new PrintEvaluator("print", Messages));
            Register("read", () => new ReadDataEvaluator("read", UIMap));
            Register("set-pop", () => new SetPopulationEvaluator("set-pop", UIMap));
            Register("open-pop", () => new OpenPopulationEvaluator("open-pop", UIMap));
            Register("stop", () => new StopEvaluator("stop", Settings));
            Register("list", () => new ListEvaluator("list", Messages, this));
        }

        private void Register(string name, Func<IEvaluator> constructor)
        {
            _commands.Add(name, constructor);
        }

        public IEvaluator ParseCommand(string commandName)
        {
            if (_commands.ContainsKey(commandName))
                return _commands[commandName]();
            throw new Exception(string.Format("Did not recognize command {0}. Did you mean: {1}?", commandName, string.Join(", ", ListCommands())));
        }

        public List<string> ListCommands()
        {
            return _commands.Select(kvp => kvp.Key).ToList();
        }

        public List<string> ListCommands(string startsWith)
        {
            return _commands.Select(kvp => kvp.Key).Where(command => command.StartsWith(startsWith)).ToList();
        }
    }
}