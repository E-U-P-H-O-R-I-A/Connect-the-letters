using CodeBase.Game.ConnectLetters.Controllers;
using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Gameplay.States
{
    public class PlayGameplayState : IState
    {
        private ProgressController _progressController;
        private SceneStateMachine _sceneStateMachine;

        [Inject]
        public void Construct(SceneStateMachine sceneStateMachine, ProgressController progressController)
        {
            _progressController = progressController;
            _sceneStateMachine = sceneStateMachine;
        }

        public UniTask Enter()
        {
            _progressController.Initialize();
            _progressController.OnComplete += ChangeState;
            
            return default;
        }

        public UniTask Exit()
        {
            _progressController.Release();
            _progressController.OnComplete -= ChangeState;
            
            return default;
        }

        private void ChangeState() => 
            _sceneStateMachine.Enter<WinGameplayState>();
    }
}