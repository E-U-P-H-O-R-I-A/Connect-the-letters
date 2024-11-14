using System;
using System.Collections.Generic;
using ConnectLetters.Input;
using TMPro;
using UnityEngine;
using Zenject;

namespace ConnectLetters.UI
{
    public class Circle : MonoBehaviour
    {
        [Space, Header("Components")]
        [SerializeField] private TextMeshProUGUI textSelectedLetters;
        [SerializeField] private InputUISpeaker inputUISpeaker;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CircleLetter prefab;
        
        [Space, Header("Settings")]
        
        [SerializeField] private float offset;

        public event Action<string> OnCheck;

        public void Initialize(List<string> letter)
        {
            InitializeLettersInCircle(letter);
            InitializeInputUI();
        }

        private float FindRadiusCircle() => 
            rectTransform.rect.width / 2;

        private void RejectString()
        {
            OnCheck?.Invoke(textSelectedLetters.text);
            textSelectedLetters.text = string.Empty;
        }

        private void SelectLetter(string letter) => 
            textSelectedLetters.text += letter;

        private void InitializeInputUI() => 
            inputUISpeaker.OnEndDrag += RejectString;

        private void InitializeLettersInCircle(List<string> letters)
        {
            float radius = FindRadiusCircle() - offset;
            float angleStep = 360f / letters.Count;

            for (int i = 0; i < letters.Count; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                float x = Mathf.Sin(angle) * radius;
                float y = Mathf.Cos(angle) * radius;

             
                CircleLetter letterObject = Instantiate(prefab, transform);
                letterObject.transform.localPosition = new Vector3(x, y, 0);
                letterObject.Initialize(inputUISpeaker, letters[i]);
                letterObject.OnSelect += SelectLetter;
            }
        }
    }
}
