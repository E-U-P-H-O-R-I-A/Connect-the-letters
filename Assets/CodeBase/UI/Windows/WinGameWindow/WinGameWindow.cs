using System;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.WinGameWindow
{
    public class WinGameWindow : WindowBase
    {
        [Space]
        
        [SerializeField] private Button nextGameButton;
        [SerializeField] private Button homeButton;

        public event Action OnSelectHome;
        public event Action OnSelectNext;

        public void OnEnable()
        {
            nextGameButton.onClick.AddListener(() => OnSelectNext?.Invoke());
            homeButton.onClick.AddListener(() => OnSelectHome?.Invoke());
            
            nextGameButton.onClick.AddListener(Close);
            homeButton.onClick.AddListener(Close);
        }

        public void OnDisable()
        {
            nextGameButton.onClick.RemoveListener(() => OnSelectNext?.Invoke());
            homeButton.onClick.RemoveListener(() => OnSelectHome?.Invoke());
            
            nextGameButton.onClick.RemoveListener(Close);
            homeButton.onClick.RemoveListener(Close);
        }
        
        public class Factory : PlaceholderFactory<string, UniTask<WinGameWindow>>
        {
        }
    }
}