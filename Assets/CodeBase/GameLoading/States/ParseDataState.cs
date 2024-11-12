using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using CodeBase.Model;
using CodeBase.Services.LogService;
using CodeBase.UI.Overlays;
using Cysharp.Threading.Tasks;

namespace CodeBase.GameLoading.States
{
    public class ParseDataState : IState
    {
        private SceneStateMachine _sceneStateMachine;
        private List<IPublicModel> _models;

        private IAwaitingOverlay _awaitingOverlay;
        private ILogService _log;

        public ParseDataState(SceneStateMachine sceneStateMachine, List<IPublicModel> models, ILogService log, IAwaitingOverlay awaitingOverlay)
        {
            _sceneStateMachine = sceneStateMachine;
            _awaitingOverlay = awaitingOverlay;
            _models = models;
            _log = log;
        }
        
        public async UniTask Enter()
        {
            _log.Log("ParseDataState enter");
            
            _awaitingOverlay.Show("Parse data...");

            InitializeModels();
            
            await UniTask.WaitForSeconds(1f); 
            
            _awaitingOverlay.Hide();

            _sceneStateMachine.Enter<FinishGameLoadingState>();
        }
          
        public UniTask Exit() => default;

        private void InitializeModels()
        {
            foreach (var model in _models)
            {
                string json = model.ReadJson();
                model.Parse(json);
            }
        }
    }
}