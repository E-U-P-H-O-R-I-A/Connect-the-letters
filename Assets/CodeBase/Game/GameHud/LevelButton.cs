using CodeBase.Infrastructure.States;
using CodeBase.Model.Private;
using CodeBase.Model.Public;
using CodeBase.Services.PublicModelProvider;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ConnectLetters.GameHud
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;

        private LevelPrivateModel _levelPrivateModel;
        private GameStateMachine _gameStateMachine;

        private int  _id;

        [Inject]
        void Construct(GameStateMachine gameStateMachine, PrivateModelProvider privateModelProvider)
        {
            _levelPrivateModel = privateModelProvider.Get<LevelPrivateModel>();
            
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize(LevelPublicScheme publicScheme)
        {
            SaveData(publicScheme.Id);
            InitText(publicScheme.Id);
        }
        
        private void OnEnable() => 
            button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            button.onClick.RemoveListener(OnClick);

        private void SaveData(int id) => 
            _id = id;

        private void InitText(int schemeLevelNumber) => 
            text.text = schemeLevelNumber.ToString();

        private void OnClick()
        {
            _levelPrivateModel.SelectedLevel = _id;
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}