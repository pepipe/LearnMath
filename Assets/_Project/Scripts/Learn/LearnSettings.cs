using LearnMath.UI;
using UnityEngine;

namespace LearnMath.Learn
{
    [CreateAssetMenu(fileName = "LearnSettings", menuName = "LearnMath/LearnSettings")]
    public class LearnSettings : ScriptableObject
    {
        public enum LearnDifficult
        {
            Easy,
            Medium,
            Hard
        }

        public enum LearnOperationType
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }
        
        public LearnDifficult Difficult;
        public LearnOperationType OperationType;
        public Number NumberPrefab;
        public Sign SignPrefab;
        public int AnswersCount = 4;
        [Tooltip("After answering correctly how long we want to wait until show next question? (in milliseconds)")]
        public int WaitTimeForNextQuestion = 3000;
    }
}
