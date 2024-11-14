using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Model.Private;

namespace CodeBase.Scheme.Private
{
    [Serializable]
    public class LevelsPrivateScheme : IPrivateScheme
    {
        private List<LevelPrivateScheme> _levels;
        
        public LevelPrivateScheme GetLevel(int id)
        {
            CheckIsHaveScheme(id);
            return _levels.First(scheme => scheme.Id == id);
        }

        public void SetOpenLevel(int id)
        {
            CheckIsHaveScheme(id);
            _levels.First(scheme => scheme.Id == id).SetOpened();
        }

        public void SetCompleteLevel(int id)
        {
            CheckIsHaveScheme(id);
            _levels.First(scheme => scheme.Id == id).SetCompleted();
        }
        
        private void CheckIsHaveScheme(int id)
        {
            if (_levels.FirstOrDefault(scheme => scheme.Id == id) == null)
                _levels.Add(new LevelPrivateScheme(id));
        }
    }
    
    [Serializable]
    public class LevelPrivateScheme
    {
        public int Id { get; private set; }
        public bool IsOpened { get; private set; }
        public bool IsCompleted { get; private set; }
        
        public LevelPrivateScheme(int id) => 
            Id = id;

        public void SetOpened() => 
            IsOpened = true;
        
        public void SetCompleted() =>
            IsCompleted = true;
    }
}