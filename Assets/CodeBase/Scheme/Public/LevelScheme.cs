using System;
using System.Collections.Generic;

namespace CodeBase.Model.Public
{
    [Serializable]
    public class LevelsScheme : IPublicScheme
    {
        public List<LevelScheme> Levels;
    }
    
    [Serializable]
    public class LevelScheme : IPublicScheme
    {
        public int LevelNumber;
        public List<string> Letters;
        public List<string> Words;
    }
}