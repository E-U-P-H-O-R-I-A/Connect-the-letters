using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using CodeBase.Services.PublicModelProvider;
using CodeBase.UI.Overlays;
using Cysharp.Threading.Tasks;

namespace CodeBase.GameLoading.States
{
    public class LoadPlayerProgressState : IState
    {
        private readonly PrivateModelProvider _privateModelProvider;
        private readonly SceneStateMachine _sceneStateMachine;
        
        private readonly IAwaitingOverlay _awaitingOverlay;
        private readonly ILogService _log;

        public LoadPlayerProgressState(SceneStateMachine sceneStateMachine, PrivateModelProvider privateModelProvider, IAwaitingOverlay awaitingOverlay, ILogService log)
        {
            _privateModelProvider = privateModelProvider;
            _sceneStateMachine = sceneStateMachine;
            _awaitingOverlay = awaitingOverlay;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("LoadPlayerProgressState enter");
            
            _awaitingOverlay.Show("Loading player progress...");

            FillModels();
            
            _awaitingOverlay.Hide();
            
            _sceneStateMachine.Enter<FinishGameLoadingState>().Forget();
        }

        private void FillModels() => 
            _privateModelProvider.GetAllModels().ForEach(model => model.LoadProgress());

        public UniTask Exit()
        {
            _log.Log("LoadPlayerProgressState exit");
            return default;
        }
    }
}