using System.Collections.Generic;

namespace Aurora4xAutomation.IO.OCR
{
    public interface ISplitter
    {
        List<byte[,]> Split(byte[,] pixels, int characterHeight = 20);
    }
}
