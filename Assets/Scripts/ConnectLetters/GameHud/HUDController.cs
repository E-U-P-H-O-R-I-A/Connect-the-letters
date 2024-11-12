using System.Collections.Generic;
using CodeBase.Model.Public;
using CodeBase.Services.PublicModelProvider;
using UnityEngine;
using Zenject;

namespace ConnectLetters.GameHud
{
    public class HUDController : MonoBehaviour
    {
        [Space, Header("Components")]
        [SerializeField] private Transform container;
        [SerializeField] private LevelButton prefabButton;
        
        private LevelPublicModel _levelModel;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(PublicModelProvider publicModelProvider, DiContainer diContainer)
        {
            _levelModel = publicModelProvider.Get<LevelPublicModel>();
            
            _diContainer = diContainer;
        }

        private void Start() => 
            InitializeContent();

        private void InitializeContent()
        {
            List<LevelScheme> schemes = _levelModel.Data.Levels;

            foreach (var scheme in schemes)
            {
                LevelButton button = CreateButton();
                button.Initialize(scheme);
            }
        }

        private LevelButton CreateButton() => 
            _diContainer.InstantiatePrefabForComponent<LevelButton>(prefabButton, container.transform);
    }
}