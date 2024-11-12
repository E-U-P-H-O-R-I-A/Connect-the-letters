using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ConnectLetters.Input
{
    public class InputUIListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnSelected;
        public event Action OnRejected;

        private InputUISpeaker _speaker;
        
        private bool _isEntered;
        private bool _isActivate;
        
        
        public void Initialize(InputUISpeaker speaker)
        {
            _speaker = speaker;
            
            _speaker.OnStartDrag += Activate;
            _speaker.OnEndDrag += Deactivate;
        }

        public void Release()
        {
            _speaker.OnStartDrag -= Activate;
            _speaker.OnEndDrag -= Deactivate;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isEntered = true;
            
            if (_isActivate)
                OnSelected?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData) => 
            _isEntered = false;

        private void Activate()
        {
            _isActivate = true;
                
            if (_isEntered)
                OnSelected?.Invoke();
        }

        private void Deactivate()
        {
            _isActivate = false;
            
            OnRejected?.Invoke();
        }

      
    }
}