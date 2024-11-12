using CodeBase.Infrastructure.States;
using CodeBase.Model.Public;
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

        private GameStateMachine _gameStateMachine;
        private LevelScheme _scheme;

        [Inject]
        void Construct(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Initialize(LevelScheme scheme)
        {
            SaveData(scheme);
            InitText(scheme.LevelNumber);
        }
        
        private void OnEnable() => 
            button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            button.onClick.RemoveListener(OnClick);

        private void SaveData(LevelScheme scheme) => 
            _scheme = scheme;

        private void InitText(int schemeLevelNumber) => 
            text.text = schemeLevelNumber.ToString();

        private void OnClick() => 
            _gameStateMachine.Enter<GameplayState>();
    }
}