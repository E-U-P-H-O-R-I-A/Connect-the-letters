using CodeBase.UI.PopUps.ErrorPopup;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using CodeBase.UI.Windows.WinGameWindow;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Factories
{
    public interface IUIFactory
    {
        
        void Cleanup();
        UniTask<PolicyAcceptPopup> CreatePolicyAskingPopup();
        UniTask<WinGameWindow> CreateWinGameWindow();
        UniTask<ErrorPopup> CreateErrorPopup();
    }
}