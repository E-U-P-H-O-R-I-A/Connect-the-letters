
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ConnectLetters.UI
{
    public class CellCrossword : MonoBehaviour
    {
        [Space, Header("Components")]
        [Space(5)]
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;

        [Space, Header("Setting")]
        [Space(5)]
        [SerializeField, Range(0, 360f)] private float angelRotate = 360f;
        [SerializeField] private float valueScale = 1.2f;
        [SerializeField] private float durationAnimation = 0.8f;

        public void Initialize(char letter) => 
            InitializeText(letter);

        public void Show()
        {
            ReleaseButton();
            
            transform.DORotate(angelRotate * Vector3.up, durationAnimation, RotateMode.FastBeyond360).SetEase(Ease.OutQuad);
            transform.DOScale(valueScale * Vector3.one, durationAnimation).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
            text.DOColor(Color.white, durationAnimation).SetEase(Ease.OutQuad);
        }

        private void ReleaseButton() => 
            button.onClick.RemoveAllListeners();

        private void InitializeText(char letter) => 
            text.text = $"{letter}".ToUpper();

        private void InitializeButton()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Show);
        }
    }
}