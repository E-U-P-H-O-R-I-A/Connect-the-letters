using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.States
{
    public class FinishGameplayState : IState
    {
        private readonly GameStateMachine gameStateMachine;

        public FinishGameplayState(GameStateMachine gameStateMachine) => 
            this.gameStateMachine = gameStateMachine;

        public UniTask Exit() => 
            default;

        public UniTask Enter()
        {
            gameStateMachine.Enter<GameHubState>().Forget();
            return default;
        }
    }
}