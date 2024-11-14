using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.PopUps
{
    public abstract class PopUpBase<TResult, TInitializeData> : MonoBehaviour
    {
        [SerializeField] private Canvas popupCanvas;

        private UniTaskCompletionSource<TResult> taskCompletionSource;
        
        private void Awake() => 
            OnAwake();

        public UniTask<TResult> Show(TInitializeData with)
        {
            taskCompletionSource = new UniTaskCompletionSource<TResult>();
            Initialize(with);
            SubscribeUpdates();
            popupCanvas.enabled = true;
            return taskCompletionSource.Task;
        }

        public void Hide() => popupCanvas.enabled = false;
        
        protected void SetPopUpResult(TResult result) =>
            taskCompletionSource.TrySetResult(result);

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => Hide();
        protected virtual void Initialize(TInitializeData with){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
}