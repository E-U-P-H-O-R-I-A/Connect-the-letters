using System.Collections.Generic;

namespace CodeBase.ConnectLetters
{
    public struct CrosswordData
    {
        public Dictionary<string, PositionWord> Words;
        public char[,] Matrix;
    }
}