using CodeBase.ConnectLetters;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Model.Base.Private;
using CodeBase.Model.Base.Public;
using CodeBase.Model.Private;
using CodeBase.Model.Public;
using CodeBase.Services.AdsService;
using CodeBase.Services.AnalyticsService;
using CodeBase.Services.InputService;
using CodeBase.Services.LocalizationService;
using CodeBase.Services.LogService;
using CodeBase.Services.PublicModelProvider;
using CodeBase.Services.RandomizerService;
using CodeBase.Services.ServerConnectionService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.Overlays;
using CodeBase.UI.Services.Factories;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();

            BindServices();
            
            BindComponents();

            BindPublicModels();

            BindPrivateModels();
            
            BindGameStateMachine();
        }

        private void BindComponents()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
        }

        private void BindGameStateMachine() => 
            GameStateMachineInstaller.Install(Container);

        private void BindPublicModels()
        {
            Container.Bind<IPublicModel>().To<LevelPublicModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PublicModelProvider>().AsSingle();
        }

        private void BindPrivateModels()
        {
            Container.Bind<IPrivateModel>().To<LevelPrivateModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PrivateModelProvider>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<LogService>().AsSingle();
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
            Container.BindInterfacesTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesTo<LocalizationService>().AsSingle();
            Container.BindInterfacesTo<ServerConnectionService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingCurtainProxy>().AsSingle();
            Container.BindInterfacesAndSelfTo<AwaitingOverlayProxy>().AsSingle();
        }

        private void BindFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstraper);
            
            Container
                .Bind<IGameFactory>()
                .FromSubContainerResolve()
                .ByInstaller<GameFactoryInstaller>()
                .AsSingle();
            
            Container
                .Bind<IUIFactory>()
                .FromSubContainerResolve()
                .ByInstaller<UIFactoryInstaller>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CrosswordFactory>()
                .AsSingle();
            
            Container
                .BindFactory<string, UniTask<AwaitingOverlay>, AwaitingOverlay.Factory>()
                .FromFactory<PrefabFactoryAsync<AwaitingOverlay>>();
            
            Container.BindFactory<string, UniTask<LoadingCurtain>, LoadingCurtain.Factory>()
                .FromFactory<PrefabFactoryAsync<LoadingCurtain>>();
        }
    }
}