using System.Collections.Generic;
using CodeBase.Model.Public;
using CodeBase.Services.PublicModelProvider;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace ConnectLetters.GameHud
{
    public class HUDController : MonoBehaviour
    {
        [Space, Header("Components")]
        [SerializeField] private Transform container;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private LevelButton prefabButton;


        [Space, Header("Settings")] 
        [SerializeField] private float durationShow = 0.8f;
        [SerializeField] private float offsetDuration = 0.5f;

        private LevelPublicModel _levelModel;
        private DiContainer _diContainer;
        
        private List<LevelButton> _buttons = new();
        
        [Inject]
        public void Construct(PublicModelProvider publicModelProvider, DiContainer diContainer)
        {
            _levelModel = publicModelProvider.Get<LevelPublicModel>();
            
            _diContainer = diContainer;
        }

        public void Start() => 
            InitializeContent();

        public void InitializeContent()
        {
            List<LevelPublicScheme> schemes = _levelModel.Data.GetLevels();
            
            foreach (LevelPublicScheme scheme in schemes)
            {
                LevelButton button = CreateButton();
                button.Initialize(scheme);
            }
            
            ShowButtons();
        }

        private void ShowButtons()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(() => canvasGroup.interactable = false);

            for (int index = 0; index < _buttons.Count; index++) 
                sequence.Insert(index * offsetDuration, _buttons[index].transform.DOScale(Vector3.one, durationShow));

            sequence.OnComplete(() => canvasGroup.interactable = true); 
        }
        
        private LevelButton CreateButton()
        {
            LevelButton button = _diContainer.InstantiatePrefabForComponent<LevelButton>(prefabButton, container.transform);
            button.transform.localScale = Vector3.zero;
            _buttons.Add(button);
            return button;
        }
    }
}