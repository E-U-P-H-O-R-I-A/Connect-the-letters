using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Model.Private;
using UnityEngine.Serialization;

namespace CodeBase.Scheme.Private
{
    [Serializable]
    public class LevelsPrivateScheme : IPrivateScheme
    {
        public List<LevelPrivateScheme> Levels;
        
        public LevelPrivateScheme GetLevel(int id)
        {
            CheckIsHaveScheme(id);
            return Levels.First(scheme => scheme.Id == id);
        }

        public void SetOpenLevel(int id)
        {
            CheckIsHaveScheme(id);
            Levels.First(scheme => scheme.Id == id).SetOpened();
        }

        public void SetCompleteLevel(int id)
        {
            CheckIsHaveScheme(id);
            Levels.First(scheme => scheme.Id == id).SetCompleted();
        }
        
        private void CheckIsHaveScheme(int id)
        {
            if (Levels.FirstOrDefault(scheme => scheme.Id == id) == null)
                Levels.Add(new LevelPrivateScheme(id));
        }
    }
    
    [Serializable]
    public class LevelPrivateScheme
    {
        public int Id;
        public bool IsOpened;
        public bool IsCompleted;
        
        public LevelPrivateScheme(int id) => 
            Id = id;

        public void SetOpened() => 
            IsOpened = true;
        
        public void SetCompleted() =>
            IsCompleted = true;
    }
}