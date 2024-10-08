using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearnMath.UI
{
    [RequireComponent(typeof(Button))]
    public class Digit : MonoBehaviour
    {
        [SerializeField] TMP_Text DigitText;

        public event Action<int> OnDigitClick;

        Button _button;

        void Awake()
        {
            _button = GetComponent<Button>();
        }

        void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void InitDigit(bool isInteractable, int digit)
        {
            DigitText.text = digit.ToString(CultureInfo.InvariantCulture);

            if (!isInteractable)
            {
                _button.transition = Selectable.Transition.None;
                _button.interactable = false;
            }
            else
            {
                _button.onClick.AddListener(delegate { OnDigitClick?.Invoke(digit); });
            }
        }

        public int GetDigit()
        {
            return int.Parse(DigitText.text);
        }
    }
}
