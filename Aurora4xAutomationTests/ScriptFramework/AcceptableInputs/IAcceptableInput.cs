using Aurora4xAutomationTests.ScriptFramework.ScriptedInputs;

namespace Aurora4xAutomationTests.ScriptFramework.AcceptableInputs
{
    public interface IAcceptableInput
    {
        bool Accepts(IScriptedInput input);
    }
}