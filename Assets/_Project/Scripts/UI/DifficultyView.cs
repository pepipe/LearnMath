using LearnMath.Learn;
using TMPro;
using UnityEngine;

namespace LearnMath.UI
{
    public class DifficultyView : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown DifficultyDropdown;
        [SerializeField] TextMeshProUGUI DifficultyText;

        public TMP_Dropdown Difficulty => DifficultyDropdown;

        public void SetDifficulty(LearnSettings.LearnDifficult difficult)
        {
            DifficultyText.text = difficult.ToString();
            DifficultyDropdown.SetValueWithoutNotify((int)difficult);
        }
    }
}
