using CodeBase.UI.PopUps.ErrorPopup;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using CodeBase.UI.Windows.WinGameWindow;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly  PolicyAcceptPopup.Factory _privatePolicyWindowFactory;
        private readonly WinGameWindow.Factory _winGameWindowFactory;
        private readonly ErrorPopup.Factory _errorPopupFactory;

        public UIFactory(PolicyAcceptPopup.Factory privatePolicyWindowFactory, ErrorPopup.Factory errorPopupFactory, WinGameWindow.Factory winGameWindowFactory)
        {
            _privatePolicyWindowFactory = privatePolicyWindowFactory;
            _winGameWindowFactory = winGameWindowFactory;
            _errorPopupFactory = errorPopupFactory;
        }

        public UniTask<PolicyAcceptPopup> CreatePolicyAskingPopup() => _privatePolicyWindowFactory.Create(UIFactoryAssets.PolicyAcceptPopup);
        public UniTask<WinGameWindow> CreateWinGameWindow() => _winGameWindowFactory.Create(UIFactoryAssets.WinGameWidow);
        public UniTask<ErrorPopup> CreateErrorPopup() => _errorPopupFactory.Create(UIFactoryAssets.ErrorPopup);
        
        public void Cleanup()
        {
            
        }
    }
}