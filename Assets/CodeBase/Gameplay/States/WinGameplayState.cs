using System.Threading.Tasks;
using CodeBase.Infrastructure.States;
using CodeBase.Model.Private;
using CodeBase.Model.Public;
using CodeBase.Services.PublicModelProvider;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows.WinGameWindow;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Gameplay.States
{
    public class WinGameplayState : IState
    {
        private SceneStateMachine _sceneStateMachine;
        private LevelPrivateModel _levelPrivateModel;
        private GameStateMachine _gameStateMachine;
        private LevelPublicModel _levelPublicModel;
        private WindowService _windowService;

        private IUIFactory _uiFactory;

        [Inject]
        public void Construct(IUIFactory uiFactory, SceneStateMachine sceneStateMachine, GameStateMachine gameStateMachine, PrivateModelProvider privateModelProvider, PublicModelProvider publicModelProvider)
        {
            _levelPrivateModel = privateModelProvider.Get<LevelPrivateModel>();
            _levelPublicModel = publicModelProvider.Get<LevelPublicModel>();

            _sceneStateMachine = sceneStateMachine;
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public UniTask Enter()
        {
            SaveComplete(); 
            OpenWindowWin();
            
            return default;
        }

        public UniTask Exit() => 
            default;

        private void LoadHud() => 
            _sceneStateMachine.Enter<FinishGameplayState>();

        private void SaveComplete()
        {
            int selectedLevel = _levelPrivateModel.SelectedLevel;
            _levelPrivateModel.Data.SetCompleteLevel(selectedLevel);
            _levelPrivateModel.SaveProgress();
        }

        private void LoadNextLevel()
        {
            if (_levelPublicModel.Data.GetLevels().Count == _levelPrivateModel.SelectedLevel)
                LoadHud();

            _levelPrivateModel.SelectedLevel++;
            _gameStateMachine.Enter<GameplayState>();
        }

        private async void OpenWindowWin()
        {
            WinGameWindow window = await _uiFactory.CreateWinGameWindow();
            window.OnSelectNext += LoadNextLevel;
            window.OnSelectHome += LoadHud;
        }
    }
}