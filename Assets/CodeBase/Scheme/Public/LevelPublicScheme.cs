using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace CodeBase.Model.Public
{
    [Serializable]
    public class LevelsPublicScheme : IPublicScheme
    {
        public  List<LevelPublicScheme> Levels;

        public LevelPublicScheme GetLevel(int id) => 
            Levels.FirstOrDefault(scheme => scheme.Id == id);

        public List<LevelPublicScheme> GetLevels() => 
            Levels.ToList();
    }
    
    [Serializable]
    public class LevelPublicScheme
    {
        public int Id;
        public List<string> Words;
        public List<string> Letters;
    }
}