using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.States
{
    public class FinishGameplayState : IState
    {
        private readonly GameStateMachine gameStateMachine;

        public FinishGameplayState(GameStateMachine gameStateMachine) => 
            this.gameStateMachine = gameStateMachine;

        public async UniTask Exit()
        {
            gameStateMachine.Enter<GameHubState>().Forget();
        }

        public UniTask Enter()
        {
            return default;
        }
    }
}