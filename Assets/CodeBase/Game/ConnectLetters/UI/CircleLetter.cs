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

        private string _char;

        public void Initialize(InputUISpeaker inputUISpeaker, string letter)
        {
            _char = letter;
            text.text = _char;

            InitializationInputListener(inputUISpeaker);
        }

        private  void Select() => 
            ChangeColorBackground(Color.white);

        private void Reject() => 
            ChangeColorBackground(Color.clear);

        private void ChangeColorBackground(Color color) => 
            background.DOColor(color, speedAnimation);

        private void InitializationInputListener(InputUISpeaker inputUISpeaker)
        {
            inputUIListener.Initialize(inputUISpeaker);
            
            inputUIListener.OnSelected += Select;
            inputUIListener.OnRejected += Reject;
        }
    }
}