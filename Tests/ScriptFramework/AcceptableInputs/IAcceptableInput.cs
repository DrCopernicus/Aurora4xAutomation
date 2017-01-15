using Tests.ScriptFramework.ScriptedInputs;

namespace Tests.ScriptFramework.AcceptableInputs
{
    public interface IAcceptableInput
    {
        bool Accepts(IScriptedInput input);
    }
}