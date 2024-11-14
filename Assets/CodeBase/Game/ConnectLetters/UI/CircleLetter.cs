using System;
using ConnectLetters.Input;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConnectLetters.UI
{
    public class CircleLetter : MonoBehaviour
    {
        [Space, Header("Components")] 
        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private InputUIListener inputUIListener;

        [Space, Header("Settings")] 
        [SerializeField] private float speedAnimation = 0.8f;
        
        public event Action<string> OnSelect;
        
        private bool _isSelected;
        

        public void Initialize(InputUISpeaker inputUISpeaker, string letter)
        {
            InitializationLetter(letter);
            InitializationInputListener(inputUISpeaker);
        }

        private  void Select()
        {
            if (_isSelected)
                return;
            
            _isSelected = true;
            OnSelect?.Invoke(text.text);
            ChangeColorBackground(Color.white);
        }

        private void Reject()
        {
            if (!_isSelected)
                return;
            
            _isSelected = false;
            ChangeColorBackground(Color.clear);
        }

        private void ChangeColorBackground(Color color) => 
            background.DOColor(color, speedAnimation);

        private void InitializationLetter(string letter) => 
            text.text = letter;

        private void InitializationInputListener(InputUISpeaker inputUISpeaker)
        {
            inputUIListener.Initialize(inputUISpeaker);
            
            inputUIListener.OnSelected += Select;
            inputUIListener.OnRejected += Reject;
        }
    }
}