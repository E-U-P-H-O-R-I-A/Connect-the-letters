
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
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
        [SerializeField] private float speedRotate = 0.8f;

        public void Awake()
        {
            Initialize('a');
        }

        public void Initialize(char letter)
        {
            InitializeText(letter);
            InitializeButton();
        }

        public void Show()
        {
            ReleaseButton();
            
            transform.DORotate(angelRotate * Vector3.up, speedRotate, RotateMode.FastBeyond360).SetEase(Ease.OutQuad);
            text.DOColor(Color.white, speedRotate).SetEase(Ease.OutQuad);
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