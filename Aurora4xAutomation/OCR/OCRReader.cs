using System.Collections.Generic;
using System.Linq;

namespace Aurora4xAutomation.OCR
{
    public static class OCRReader
    {
        public static Dictionary<string, byte[,]> Alphabet = new Dictionary<string, byte[,]>
        {
            {"", new byte[11,0]},
            #region Symbols
            {":", MakeChar(" \n" +
                           " \n" +
                           " \n" +
                           "x\n" +
                           " \n" +
                           " \n" +
                           " \n" +
                           " \n" +
                           "x\n" +
                           " \n" +
                           " ")},
            {".", MakeChar(" \n" +
                           " \n" +
                           " \n" +
                           " \n" +
                           " \n" +
                           " \n" +
                           " \n" +
                           " \n" +
                           "x\n" +
                           " \n" +
                           " ")},
            {"-", MakeChar("  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "xx\n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "  ")},
            {")", MakeChar("x \n" +
                           " x\n" +
                           " x\n" +
                           " x\n" +
                           " x\n" +
                           " x\n" +
                           " x\n" +
                           " x\n" +
                           " x\n" +
                           " x\n" +
                           "x ")},
            {"(", MakeChar(" x\n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           " x")},
            {",", MakeChar("  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           "  \n" +
                           " x\n" +
                           "x \n" +
                           "  ")},
            {"+", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "xxxxx\n" +
                           "  x  \n" +
                           "  x  \n" +
                           "     \n" +
                           "     \n" +
                           "     ")},
            {"%", MakeChar(" xx    \n" +
                           "x  x  x\n" +
                           " xx  x \n" +
                           "    x  \n" +
                           "   x   \n" +
                           "  x    \n" +
                           " x  xx \n" +
                           "x  x  x\n" +
                           "    xx \n" +
                           "       \n" +
                           "       ")},
            {"/", MakeChar("   x\n" +
                           "   x\n" +
                           "   x\n" +
                           "  x \n" +
                           "  x \n" +
                           " x  \n" +
                           " x  \n" +
                           "x   \n" +
                           "x   \n" +
                           "    \n" +
                           "    ")},
            #endregion
            #region Lowercase
            {"a", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           " xxx \n" +
                           "    x\n" +
                           " xxxx\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxxx\n" +
                           "     \n" +
                           "     ")},
            {"b", MakeChar("x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "xxxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "xxxx \n" +
                           "     \n" +
                           "     ")},
            {"c", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           " xxx \n" +
                           "x   x\n" +
                           "x    \n" +
                           "x    \n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"d", MakeChar("    x\n" +
                           "    x\n" +
                           "    x\n" +
                           " xxxx\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxxx\n" +
                           "     \n" +
                           "     ")},
            {"e", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           " xxx \n" +
                           "x   x\n" +
                           "xxxxx\n" +
                           "x    \n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"f", MakeChar(" x\n" +
                           "x \n" +
                           "x \n" +
                           "xx\n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "  \n" +
                           "  ")},
            {"g", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           " xxxx\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxxx\n" +
                           "    x\n" +
                           "xxxx ")},
            {"gy", MakeChar("          \n" +
                            "          \n" +
                            "          \n" +
                            " xxxx x  x\n" +
                            "x   x x  x\n" +
                            "x   x x  x\n" +
                            "x   x x  x\n" +
                            "x   x  xx \n" +
                            " xxxx  x  \n" +
                            "    x  x  \n" +
                            "xxxx xx   ")},
            {"h", MakeChar("x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x xx \n" +
                           "xx  x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "     \n" +
                           "     ")},
            {"i", MakeChar("x\n" +
                           " \n" +
                           " \n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           " \n" +
                           " ")},
            {"k", MakeChar("x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x  x \n" +
                           "x x  \n" +
                           "xx   \n" +
                           "x x  \n" +
                           "x  x \n" +
                           "x   x\n" +
                           "     \n" +
                           "     ")},
            {"l", MakeChar("x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           " \n" +
                           " ")},
            {"m", MakeChar("       \n" +
                           "       \n" +
                           "       \n" +
                           "xxx xx \n" +
                           "x  x  x\n" +
                           "x  x  x\n" +
                           "x  x  x\n" +
                           "x  x  x\n" +
                           "x  x  x\n" +
                           "       \n" +
                           "       ")},
            {"n", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           "x xx \n" +
                           "xx  x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "     \n" +
                           "     ")},
            {"o", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           " xxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"p", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           "xxxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "xxxx \n" +
                           "x    \n" +
                           "x    ")},
            {"q", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           " xxxx\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxxx\n" +
                           "    x\n" +
                           "    x")},
            {"r", MakeChar("  \n" +
                           "  \n" +
                           "  \n" +
                           "xx\n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "  \n" +
                           "  ")},
            {"s", MakeChar("    \n" +
                           "    \n" +
                           "    \n" +
                           " xx \n" +
                           "x  x\n" +
                           " x  \n" +
                           "  x \n" +
                           "x  x\n" +
                           " xx \n" +
                           "    \n" +
                           "    ")},
            {"t", MakeChar("  \n" +
                           "x \n" +
                           "x \n" +
                           "xx\n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           "x \n" +
                           " x\n" +
                           "  \n" +
                           "  ")},
            {"u", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x  xx\n" +
                           " xx x\n" +
                           "     \n" +
                           "     ")},
            {"v", MakeChar("     \n" +
                           "     \n" +
                           "     \n" +
                           "x   x\n" +
                           "x   x\n" +
                           " x x \n" +
                           " x x \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "     \n" +
                           "     ")},
            {"w", MakeChar("       \n" +
                           "       \n" +
                           "       \n" +
                           "x  x  x\n" +
                           "x  x  x\n" +
                           "x x x x\n" +
                           "x x x x\n" +
                           " x   x \n" +
                           " x   x \n" +
                           "       \n" +
                           "       ")},
            {"x", MakeChar("    \n" +
                           "    \n" +
                           "    \n" +
                           "x  x\n" +
                           "x  x\n" +
                           " xx \n" +
                           " xx \n" +
                           "x  x\n" +
                           "x  x\n" +
                           "    \n" +
                           "    ")},
            {"y", MakeChar("    \n" +
                           "    \n" +
                           "    \n" +
                           "x  x\n" +
                           "x  x\n" +
                           "x  x\n" +
                           "x  x\n" +
                           " xx \n" +
                           " x  \n" +
                           " x  \n" +
                           "x   ")},
            {"ty", MakeChar("       \n" +
                            "x      \n" +
                            "x      \n" +
                            "xx x  x\n" +
                            "x  x  x\n" +
                            "x  x  x\n" +
                            "x  x  x\n" +
                            "x   xx \n" +
                            " x  x  \n" +
                            "    x  \n" +
                            "  xx   ")},
            {"vy", MakeChar("          \n" +
                            "          \n" +
                            "          \n" +
                            "x   x x  x\n" +
                            "x   x x  x\n" +
                            " x x  x  x\n" +
                            " x x  x  x\n" +
                            "  x    xx \n" +
                            "  x    x  \n" +
                            "       x  \n" +
                            "     xx   ")},
            {"xy", MakeChar("         \n" +
                            "         \n" +
                            "         \n" +
                            "x  x x  x\n" +
                            "x  x x  x\n" +
                            " xx  x  x\n" +
                            " xx  x  x\n" +
                            "x  x  xx \n" +
                            "x  x  x  \n" +
                            "      x  \n" +
                            "    xx   ")},
            {"z", MakeChar("    \n" +
                           "    \n" +
                           "    \n" +
                           "xxxx\n" +
                           "   x\n" +
                           "  x \n" +
                           " x  \n" +
                           "x   \n" +
                           "xxxx\n" +
                           "    \n" +
                           "    ")},
            #endregion
            #region Uppercase
            {"A", MakeChar("   x   \n" +
                           "   x   \n" +
                           "  x x  \n" +
                           "  x x  \n" +
                           " x   x \n" +
                           " x   x \n" +
                           " xxxxx \n" +
                           "x     x\n" +
                           "x     x\n" +
                           "       \n" +
                           "       ")},
            {"B", MakeChar("xxxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "xxxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "xxxx \n" +
                           "     \n" +
                           "     ")},
            {"C", MakeChar(" xxxx \n" +
                           "x    x\n" +
                           "x     \n" +
                           "x     \n" +
                           "x     \n" +
                           "x     \n" +
                           "x     \n" +
                           "x    x\n" +
                           " xxxx \n" +
                           "      \n" +
                           "      ")},
            {"D", MakeChar("xxxx  \n" +
                           "x   x \n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x   x \n" +
                           "xxxx  \n" +
                           "      \n" +
                           "      ")},
            {"E", MakeChar("xxxxx\n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "xxxx \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "xxxxx\n" +
                           "     \n" +
                           "     ")},
            {"F", MakeChar("xxxxx\n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "xxxx \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "     \n" +
                           "     ")},
            {"G", MakeChar(" xxxx \n" +
                           "x    x\n" +
                           "x     \n" +
                           "x     \n" +
                           "x  xxx\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x   xx\n" +
                           " xxx x\n" +
                           "      \n" +
                           "      ")},
            {"H", MakeChar("x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "xxxxxx\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "      \n" +
                           "      ")},
            {"I", MakeChar("x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           "x\n" +
                           " \n" +
                           " ")},
            {"J", MakeChar("   x\n" +
                           "   x\n" +
                           "   x\n" +
                           "   x\n" +
                           "   x\n" +
                           "   x\n" +
                           "   x\n" +
                           "x  x\n" +
                           " xx \n" +
                           "    \n" +
                           "     ")},
            {"L", MakeChar("x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "x    \n" +
                           "xxxxx\n" +
                           "     \n" +
                           "     ")},
            {"M", MakeChar("x     x\n" +
                           "x     x\n" +
                           "xx   xx\n" +
                           "xx   xx\n" +
                           "x x x x\n" +
                           "x x x x\n" +
                           "x  x  x\n" +
                           "x  x  x\n" +
                           "x     x\n" +
                           "       \n" +
                           "       ")},
            {"O", MakeChar(" xxxx \n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           " xxxx \n" +
                           "      \n" +
                           "      ")},
            {"P", MakeChar("xxxxx \n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "xxxxx \n" +
                           "x     \n" +
                           "x     \n" +
                           "x     \n" +
                           "x     \n" +
                           "      \n" +
                           "      ")},
            {"R", MakeChar("xxxxx \n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "xxxxx \n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "x    x\n" +
                           "      \n" +
                           "      ")},
            {"S", MakeChar(" xxx \n" +
                           "x   x\n" +
                           "x    \n" +
                           "x    \n" +
                           " xxx \n" +
                           "    x\n" +
                           "    x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"T", MakeChar("xxxxx\n" +
                           "  x  \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "  x  \n" +
                           "     \n" +
                           "     ")},
            #endregion
            #region Numbers
            {"0", MakeChar(" xxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"1", MakeChar("  x\n" +
                           "xxx\n" +
                           "  x\n" +
                           "  x\n" +
                           "  x\n" +
                           "  x\n" +
                           "  x\n" +
                           "  x\n" +
                           "  x\n" +
                           "   \n" +
                           "   ")},
            {"2", MakeChar(" xxx \n" +
                           "x   x\n" +
                           "    x\n" +
                           "    x\n" +
                           "   x \n" +
                           "  x  \n" +
                           " x   \n" +
                           "x    \n" +
                           "xxxxx\n" +
                           "     \n" +
                           "     ")},
            {"3", MakeChar(" xxx \n" +
                           "x   x\n" +
                           "    x\n" +
                           "    x\n" +
                           "  xx \n" +
                           "    x\n" +
                           "    x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"4", MakeChar("   x \n" +
                           "  xx \n" +
                           "  xx \n" +
                           " x x \n" +
                           " x x \n" +
                           "x  x \n" +
                           "xxxxx\n" +
                           "   x \n" +
                           "   x \n" +
                           "     \n" +
                           "     ")},
            {"5", MakeChar("xxxxx\n" +
                           "x    \n" +
                           "x    \n" +
                           "xxxx \n" +
                           "x   x\n" +
                           "    x\n" +
                           "    x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"6", MakeChar(" xxx \n" +
                           "x   x\n" +
                           "x    \n" +
                           "x    \n" +
                           "xxxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"7", MakeChar("xxxxx\n" +
                           "    x\n" +
                           "   x \n" +
                           "   x \n" +
                           "  x  \n" +
                           "  x  \n" +
                           " x   \n" +
                           " x   \n" +
                           " x   \n" +
                           "     \n" +
                           "     ")},
            {"8", MakeChar(" xxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")},
            {"9", MakeChar(" xxx \n" +
                           "x   x\n" +
                           "x   x\n" +
                           "x   x\n" +
                           " xxxx\n" +
                           "    x\n" +
                           "    x\n" +
                           "x   x\n" +
                           " xxx \n" +
                           "     \n" +
                           "     ")}
            #endregion
        };

        private static byte[,] MakeChar(string str)
        {
            var spl = str.Split('\n');
            var bytes = new byte[spl.Length, spl[0].Length];

            for (int x = 0; x < spl[0].Length; x++)
            {
                for (int y = 0; y < spl.Length; y++)
                {
                    bytes[y, x] = (byte) (spl[y][x].Equals(' ') ? 0 : 1);
                }
            }

            return bytes;
        }

        public static string ReadTableRow(byte[,] pixels, Dictionary<string, byte[,]> alphabet)
        {
            return pixels.Split().Select(x => ReadCharacter(x, alphabet)).Aggregate("", (x, str) => x + str);
        }

        public static List<byte[,]> Split(this byte[,] pixels, int characterHeight = 20)
        {
            var currentPosition = 0;
            var list = new List<byte[,]>();
            var charInProgress = new List<byte[]>();

            while (currentPosition < pixels.GetLength(1))
            {
                if (!IsBlankColumn(pixels, currentPosition))
                {
                    CopyColumnToEndOfList(charInProgress, pixels, currentPosition);
                }
                else
                {
                    CopyCharacterBytesToList(list, charInProgress, characterHeight);
                    charInProgress = new List<byte[]>();
                }
                currentPosition++;
            }

            return list;
        }

        private static void CopyColumnToEndOfList(List<byte[]> charInProgress, byte[,] pixels, int column)
        {
            var pixelColumn = new byte[pixels.GetLength(0)];
            for (int i = 0; i < pixelColumn.Length; i++)
            {
                pixelColumn[i] = pixels[i, column];
            }
            charInProgress.Add(pixelColumn);
        }

        private static void CopyCharacterBytesToList(List<byte[,]> list, List<byte[]> charInProgress, int characterHeight)
        {
            var twodbytes = new byte[characterHeight, charInProgress.Count];
            for (int x = 0; x < charInProgress.Count; x++)
            {
                for (int y = 0; y < charInProgress[0].Length; y++)
                {
                    twodbytes[y, x] = charInProgress[x][y];
                }
            }
            list.Add(twodbytes);
        }

        private static bool IsBlankColumn(byte[,] pixels, int column)
        {
            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                if (pixels[i, column] != 0)
                    return false;
            }
            return true;
        }

        public static string ReadCharacter(byte[,] pixels, Dictionary<string, byte[,]> alphabet)
        {
            return alphabet.FirstOrDefault(x => x.Value.EqualsPixels(pixels)).Key;
        }

        public static bool EqualsPixels(this byte[,] pixels, byte[,] otherPixels)
        {
            if (pixels.GetLength(1) != otherPixels.GetLength(1))
                return false;

            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    if (pixels[x,y] != otherPixels[x,y])
                        return false;
                }
            }
            return true;
        }
    }
}
