using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameHubState : IState
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IAssetProvider _assetProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILogService _log;

        public GameHubState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, ILogService log, IAssetProvider assetProvider)
        {
            _loadingCurtain = loadingCurtain;
            _assetProvider = assetProvider;
            _sceneLoader = sceneLoader;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("GameHub state exter");
            _loadingCurtain.Show();

            await _assetProvider.WarmupAssetsByLabel(AssetLabels.GameHubState);
            await _sceneLoader.Load(InfrastructureAssetPath.GameHubScene);
            
            _loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            _loadingCurtain.Show();
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.GameHubState);
        }
    }
}