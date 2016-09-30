using System.Collections.Generic;

namespace Aurora4xAutomation.IO
{
    public interface IOCRReader
    {
        string ReadTableRow(byte[,] pixels, Dictionary<string, byte[,]> alphabet);
    }
}
