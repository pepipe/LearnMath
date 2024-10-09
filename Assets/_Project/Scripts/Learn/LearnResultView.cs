using System;
using System.Collections.Generic;
using System.Linq;
using LearnMath.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace LearnMath.Learn
{
    public class LearnResultView : MonoBehaviour, ILearnResultView
    {
        [SerializeField] TMP_Text ResultText;
        [SerializeField] Transform ResultsParent;
        [Tooltip("Space between answers digits")]
        [SerializeField] float AnswersSpacing = 20f;
        [SerializeField] Sign SignSymbol;
        [Header("Effects")]
        [SerializeField] Image Mouth;
        [SerializeField] Sprite Happy;
        [SerializeField] Sprite Sad;

        public event Action<bool> OnAnswer;

        int _result;
        IObjectPool<Number> _answersDigitsPool;
        RectTransform _rectTransform;
        float _digitWidth;

        public void SetupOperation(Number numberPrefab)
        {
            _digitWidth = numberPrefab.GetComponent<RectTransform>().rect.width;
            _answersDigitsPool = new ObjectPool<Number>(
                createFunc: () => CreateDigit(numberPrefab),
                actionOnGet: digit => digit.gameObject.SetActive(true),
                actionOnRelease: digit => digit.gameObject.SetActive(false),
                actionOnDestroy: digit => Destroy(digit.gameObject),
                collectionCheck: false,
                defaultCapacity: 4,
                maxSize: 10
            );
        }

        public void StartOperation(LearnOperation operation, IEnumerable<int> answers)
        {
            int[] answersArray = answers.ToArray();
            SetWidth(answersArray.Length);
            _result = operation.Result;
            SignSymbol.SetSign("=");
            SignSymbol.gameObject.SetActive(true);
            foreach (int answer in answersArray)
            {
                var digit = _answersDigitsPool.Get();
                digit.InitDigit(true, answer);
            }
        }

        public void Reset()
        {
            SignSymbol.gameObject.SetActive(false);
            ResultText.text = "";
            foreach (Transform child in ResultsParent)
            {
                var digit = child.GetComponent<Number>();
                if(digit) _answersDigitsPool.Release(digit);
            }
        }

        void SetWidth(int answersCount)
        {
            _rectTransform ??= GetComponent<RectTransform>();
            float width = _digitWidth * answersCount;
            width += AnswersSpacing * (answersCount - 1);
            _rectTransform.sizeDelta = new Vector2(width, _rectTransform.sizeDelta.y);
        }

        Number CreateDigit(Number numberPrefab)
        {
            var digit = Instantiate(numberPrefab, ResultsParent);
            digit.OnDigitClick += HandleDigitClick;
            return digit;
        }
        
        void HandleDigitClick(int digitValue)
        {
            bool isCorrect = IsCorrectAnswer(digitValue);
            CorrectAnswerEffects(isCorrect);
            OnAnswer?.Invoke(isCorrect);
        }

        bool IsCorrectAnswer(int answerValue)
        {
            return _result == answerValue;
        }

        void CorrectAnswerEffects(bool isCorrect)
        {
            ResultText.text =  isCorrect ? "Correct" : "Wrong";
            Mouth.sprite = isCorrect ? Happy : Sad;
        }
    }
}