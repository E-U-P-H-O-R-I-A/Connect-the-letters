using ConnectLetters.Input;
using UnityEngine;
using Zenject;

namespace ConnectLetters.UI
{
    public class Circle : MonoBehaviour
    {
        [Space, Header("Components")]
        [SerializeField] private InputUISpeaker inputUISpeaker;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CircleLetter prefab;
        
        [Space, Header("Settings")]
        [SerializeField] private float offset;
        
        [Space]
        public string[] letters = { "Л", "П", "О", "К", "Р"};

        private void Start() => 
            PlaceLettersInCircle();

        private float FindRadiusCircle() => 
            rectTransform.rect.width / 2;
        
        private void PlaceLettersInCircle()
        {
            float radius = FindRadiusCircle() - offset;
            float angleStep = 360f / letters.Length;

            for (int i = 0; i < letters.Length; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                float x = Mathf.Sin(angle) * radius;
                float y = Mathf.Cos(angle) * radius;

             
                CircleLetter letterObject = Instantiate(prefab, transform);
                letterObject.transform.localPosition = new Vector3(x, y, 0);
                letterObject.Initialize(inputUISpeaker, letters[i]);
            }
        }
    }
}
