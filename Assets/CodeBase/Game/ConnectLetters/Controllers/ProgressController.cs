using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Model.Private;
using CodeBase.Model.Public;
using CodeBase.Services.PublicModelProvider;
using ConnectLetters.UI;
using Zenject;

namespace CodeBase.Game.ConnectLetters.Controllers
{
    public class ProgressController 
    {
        private LevelPrivateModel _levelPrivateModel;
        private LevelPublicModel _levelPublicModel;
        private Crossword _crossword;
        private Circle _circle;

        private Dictionary<string, bool> _wordKeys;
        
        public event Action OnComplete;

        [Inject]
        public void Construct(PrivateModelProvider privateModelProvider, PublicModelProvider publicModelProvider, Crossword crossword, Circle circle)
        {
            _levelPrivateModel = privateModelProvider.Get<LevelPrivateModel>();
            _levelPublicModel = publicModelProvider.Get<LevelPublicModel>();
            
            _crossword = crossword;
            _circle = circle;
        }

        public void Initialize()
        {
            FillWordsKeys();
            AddChecker();
        }

        public void Release()
        {
            ReleaseCheaker();
        }

        private void ReleaseCheaker() => 
            _circle.OnCheck -= CheckWordKey;

        private void AddChecker() => 
            _circle.OnCheck += CheckWordKey;

        private void FillWordsKeys()
        {
            _wordKeys = new Dictionary<string, bool>();
            List<string> words = _levelPublicModel.Data.GetLevel(_levelPrivateModel.SelectedLevel).Words;
            words.ForEach(word => _wordKeys.Add(word.ToUpper(), false));
        }

        private void CheckIsComplete()
        {
            if (_wordKeys.All(wordKeys => wordKeys.Value))
                OnComplete?.Invoke();
        }
        
        private void CheckWordKey(string word)
        {
            if (_wordKeys.ContainsKey(word.ToUpper()))
            {
                _crossword.OpenWord(word);
                _wordKeys[word.ToUpper()] = true;
            }

            CheckIsComplete();
        }
    }
}