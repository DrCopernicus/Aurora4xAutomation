﻿using Server.Evaluators;
using Server.Events;
using Server.IO;
using Server.Messages;
using Server.Settings;
using System;

namespace Server.Command.Parser
{
    public class CommandLexer
    {
        public CommandLexer(IUIMap uiMap, ISettingsStore settings, IMessageManager messages, IEventManager eventManager)
        {
            UIMap = uiMap;
            Settings = settings;
            Messages = messages;
            EventManager = eventManager;
            Registry = new EvaluatorRegistry(uiMap, settings, messages, eventManager);
        }

        private IUIMap UIMap { get; set; }
        private ISettingsStore Settings { get; set; }
        private IMessageManager Messages { get; set; }
        private IEventManager EventManager { get; set; }
        private EvaluatorRegistry Registry { get; set; }

        private void SkipSpaces(ref string command)
        {
            while (true)
            {
                if (command.Length == 0)
                    return;

                var c = command[0];
                if (c != ' ' && c != '\t' && c != '\n')
                    return;

                command = command.Remove(0, 1);
            }
        }

        private CommandToken GetNext(ref string command)
        {
            SkipSpaces(ref command);

            if (command.Length == 0)
                return new CommandToken("", CommandTokenType.EOF);

            var str = "";
            while (true)
            {
                if (command.Length == 0)
                    break;

                var c = command[0];
                if (c == ' ' || c == '\t' || c == '\n')
                    break;

                if (str.Length > 0
                    && (c == '(' || c == ')' || c == '{' || c == '}' || c == '=' || c == ';'))
                    break;

                str += c;
                command = command.Remove(0, 1);

                if (str == "(")
                    return new CommandToken(str, CommandTokenType.LeftParenthesis);
                if (str == ")")
                    return new CommandToken(str, CommandTokenType.RightParenthesis);
                if (str == "{")
                    return new CommandToken(str, CommandTokenType.LeftBrace);
                if (str == "}")
                    return new CommandToken(str, CommandTokenType.RightBrace);
                if (str == "=>")
                    return new CommandToken(str, CommandTokenType.Arrow);
                if (str == ";")
                    return new CommandToken(str, CommandTokenType.Semicolon);
            }
            return new CommandToken(str, CommandTokenType.Text);
        }

        private void UngetToken(ref string command, CommandToken token)
        {
            command = token.Text + " " + command;
        }

        public IEvaluator Lex(string command)
        {
            var statement = Statement(ref command);
            if (statement == null)
                throw new Exception("Failed to parse command correctly: Expected ( or Text");
            return statement;
        }

        private IEvaluator Statement(ref string command)
        {
            var token = GetNext(ref command);
            IEvaluator eval;

            if (token.Type == CommandTokenType.LeftParenthesis)
            {
                UngetToken(ref command, token);
                eval = Timer(ref command);
            }
            else if (token.Type == CommandTokenType.Text)
            {
                UngetToken(ref command, token);
                eval = Action(ref command);
            }
            else
            {
                UngetToken(ref command, token);
                return null;
            }

            token = GetNext(ref command);
            if (token.Type != CommandTokenType.Semicolon)
            {
                UngetToken(ref command, token);
                return eval;
            }

            eval.Next = Statement(ref command);
            
            return eval;
        }

        private IEvaluator Timer(ref string command)
        {
            var token = GetNext(ref command);
            if (token.Type != CommandTokenType.LeftParenthesis)
                throw new Exception("Failed to parse Timer token correctly: Expected (");

            token = GetNext(ref command);
            if (token.Type != CommandTokenType.Text)
                throw new Exception("Failed to parse Timer token correctly: Expected Text");

            var eval = new TimerEvaluator(token.Text, UIMap, EventManager);

            token = GetNext(ref command);
            if (token.Type != CommandTokenType.RightParenthesis)
                throw new Exception("Failed to parse Timer token correctly: Expected )");

            token = GetNext(ref command);
            if (token.Type != CommandTokenType.Arrow)
                throw new Exception("Failed to parse Timer token correctly: Expected =>");

            token = GetNext(ref command);
            if (token.Type != CommandTokenType.LeftBrace)
                throw new Exception("Failed to parse Timer token correctly: Expected {");

            eval.Body = Statement(ref command);

            token = GetNext(ref command);
            if (token.Type != CommandTokenType.RightBrace)
                throw new Exception("Failed to parse Timer token correctly: Expected }");

            return eval;
        }

        private IEvaluator Action(ref string command)
        {
            var token = GetNext(ref command);
            if (token.Type != CommandTokenType.Text)
                throw new Exception("Failed to parse Action token correctly: Expected Text");

            var eval = TextToCommand(token.Text);
            if (eval.GetEvaluatorType() == CommandEvaluatorType.Help)
                eval.Body = HelpParameter(ref command);
            else
                eval.Body = Parameters(ref command);

            return eval;
        }

        private IEvaluator HelpParameter(ref string command)
        {
            var token = GetNext(ref command);
            if (token.Type != CommandTokenType.Text)
            {
                UngetToken(ref command, token);
                return null;
            }
            return TextToCommand(token.Text);
        }

        private IEvaluator Parameters(ref string command)
        {
            var token = GetNext(ref command);
            if (token.Type != CommandTokenType.Text)
            {
                UngetToken(ref command, token);
                return null;
            }
            var eval = new ParameterEvaluator(token.Text);
            eval.Next = Parameters(ref command);

            return eval;
        }

        private IEvaluator TextToCommand(string text)
        {
            try
            {
                return Registry.ParseCommand(text);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Threw an exception attempting to convert text <{0}> to a command. Exception message: {1}", text, e.Message));
            }
        }
    }
}
