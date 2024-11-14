using CodeBase.Infrastructure.States;
using CodeBase.Model.Private;
using CodeBase.Model.Public;
using CodeBase.Services.PublicModelProvider;
using ConnectLetters.UI;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.States
{
    public class StartGameplayState : IState
    {
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly LevelPrivateModel _levelPrivateModel;
        private readonly LevelPublicModel _levelPublicModel;
        private readonly Crossword _crossword;
        private readonly Circle _circle;

        public StartGameplayState(SceneStateMachine sceneStateMachine, Crossword crossword, Circle circle, PublicModelProvider publicModelProvider, PrivateModelProvider privateModelProvider)
        {
            _levelPrivateModel = privateModelProvider.Get<LevelPrivateModel>();
            _levelPublicModel = publicModelProvider.Get<LevelPublicModel>();
            
            _sceneStateMachine = sceneStateMachine;
            _crossword = crossword;
            _circle = circle;
        }

        public async UniTask Enter()
        {
            int selectedLevel = _levelPrivateModel.SelectedLevel;

            _crossword.Initialize(_levelPublicModel.Data.GetLevel(selectedLevel).Words);
            _circle.Initialize(_levelPublicModel.Data.GetLevel(selectedLevel).Letters);
            
            _sceneStateMachine.Enter<PlayGameplayState>().Forget();
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}