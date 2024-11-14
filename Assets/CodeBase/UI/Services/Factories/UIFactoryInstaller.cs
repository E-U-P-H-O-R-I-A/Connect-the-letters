using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.PopUps.ErrorPopup;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using CodeBase.UI.Windows.WinGameWindow;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactoryInstaller : Installer<UIFactoryInstaller>
    {
        public override void InstallBindings()
        {
            // bind ui factories here
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

            // that an example of binding zenject factories working in async way
            Container
                .BindFactory<string, UniTask<PolicyAcceptPopup>, PolicyAcceptPopup.Factory>()
                .FromFactory<PrefabFactoryAsync<PolicyAcceptPopup>>();
            
            Container
                .BindFactory<string, UniTask<ErrorPopup>, ErrorPopup.Factory>()
                .FromFactory<PrefabFactoryAsync<ErrorPopup>>();
            
            Container
                .BindFactory<string, UniTask<WinGameWindow>, WinGameWindow.Factory>()
                .FromFactory<PrefabFactoryAsync<WinGameWindow>>();
        }
    }
}