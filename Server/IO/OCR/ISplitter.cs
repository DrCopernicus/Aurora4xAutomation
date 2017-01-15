using System.Collections.Generic;

namespace Server.IO.OCR
{
    public interface ISplitter
    {
        List<byte[,]> Split(byte[,] pixels, int characterHeight = 20);
    }
}
