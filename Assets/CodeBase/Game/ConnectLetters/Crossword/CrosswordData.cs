using System.Collections.Generic;

namespace CodeBase.ConnectLetters
{
    public class CrosswordData
    {
        public Dictionary<string, PositionWord> Words;
        public char[,] Matrix;

        public CrosswordData()
        {
            Words = new Dictionary<string, PositionWord>();
        }
    }
}