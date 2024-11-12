using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ConnectLetters.Input
{
    public class InputUISpeaker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnStartDrag;
        public event Action OnEndDrag;
        
        public void OnPointerDown(PointerEventData eventData) => 
            OnStartDrag?.Invoke();

        public void OnPointerUp(PointerEventData eventData) => 
            OnEndDrag?.Invoke();
    }
}