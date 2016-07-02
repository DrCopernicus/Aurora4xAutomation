using System;
using System.Collections.Generic;
using Aurora4xAutomation.Command.Evaluators;

namespace Aurora4xAutomation.Command.Parser
{
    public class CommandLexer
    {
        private static void SkipSpaces(ref string command)
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

        private static CommandToken GetNext(ref string command)
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

        private static void UngetToken(ref string command, CommandToken token)
        {
            command = token.Text + " " + command;
        }

        public static Evaluator Lex(string command)
        {
            var statement = Statement(ref command);
            if (statement == null)
                throw new Exception("Failed to parse command correctly: Expected ( or Text");
            return statement;
        }

        private static Evaluator Statement(ref string command)
        {
            var token = GetNext(ref command);
            Evaluator eval;

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

        private static Evaluator Timer(ref string command)
        {
            var token = GetNext(ref command);
            if (token.Type != CommandTokenType.LeftParenthesis)
                throw new Exception("Failed to parse Timer token correctly: Expected (");

            token = GetNext(ref command);
            if (token.Type != CommandTokenType.Text)
                throw new Exception("Failed to parse Timer token correctly: Expected Text");

            var eval = new TimerEvaluator(token.Text);

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

        private static Evaluator Action(ref string command)
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

        private static Evaluator HelpParameter(ref string command)
        {
            var token = GetNext(ref command);
            if (token.Type != CommandTokenType.Text)
            {
                UngetToken(ref command, token);
                return null;
            }
            return TextToCommand(token.Text);
        }

        private static Evaluator Parameters(ref string command)
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

        private static Evaluator TextToCommand(string text)
        {
                try
                {
                    return (Evaluator) Activator.CreateInstance(TextRepresentationOfCommands[text], text);
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("Did not recognize command {0}.", text));
                }
        }

        private static readonly Dictionary<string, Type> TextRepresentationOfCommands = new Dictionary<string, Type>
        {
            {"adv", typeof(AdvanceEvaluator)},
            {"build-installation", typeof(BuildInstallationEvaluator)},
            {"contract", typeof(ContractEvaluator)},
            {"help", typeof(HelpEvaluator)},
            {"move", typeof(MoveEvaluator)},
            {"open", typeof(OpenWindowEvaluator)},
            {"print", typeof(PrintEvaluator)},
            {"read", typeof(ReadDataEvaluator)},
            {"set-pop", typeof(SetPopulationEvaluator)},
            {"open-pop", typeof(OpenPopulationEvaluator)},
            {"stop", typeof(StopEvaluator)},
        };
    }
}
