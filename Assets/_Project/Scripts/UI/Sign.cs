using TMPro;
using UnityEngine;

namespace LearnMath.UI
{
    public class Sign : MonoBehaviour
    {
        [SerializeField] TMP_Text SignText;
        
        public void SetSign(string sign)
        {
            SignText.text = sign;
        }

        public string GetSign()
        {
            return SignText.text;
        }
    }
}