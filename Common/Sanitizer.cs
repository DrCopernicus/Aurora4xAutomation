namespace Common
{
    public class Sanitizer
    {
        public string Sanitize(string inputString)
        {
            return inputString.Replace("\0", "").Replace("\n", "").Replace("\b", "").Replace("\r", "");
        }
    }
}