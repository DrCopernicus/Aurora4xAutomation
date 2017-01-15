using System;

namespace Server.Common.Exceptions
{
    public class WindowNotFoundException : Exception
    {
        public WindowNotFoundException(string title)
            : base(string.Format("{0} window not found!", title))
        {
        }
    }
}
