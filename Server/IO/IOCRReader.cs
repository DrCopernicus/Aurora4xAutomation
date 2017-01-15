using System.Collections.Generic;

namespace Server.IO
{
    public interface IOCRReader
    {
        string ReadTableRow(byte[,] pixels, Dictionary<string, byte[,]> alphabet);
    }
}
