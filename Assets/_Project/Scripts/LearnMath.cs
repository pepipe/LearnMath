using System;
using LearnMath.Learn;
using LearnMath.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LearnMath
{
    public class LearnMath : MonoBehaviour
    {
        [Header("Learn Math")]
        [SerializeField] LearnSettings LearnSettings;
        [SerializeField] LearnResultView LearnResultView;
        [SerializeField] LearnOperationView LearnOperationView;
        [SerializeField] bool AutoStart = true;
        [Header("UI")]
        [SerializeField] DifficultyView DifficultyView;

        public LearnSettings Settings => LearnSettings;
        public LearnOperationView OperationView => LearnOperationView;
        public LearnResultView ResultView => LearnResultView;
        public LearnOperation CurrentOperation { get; private set; }

        void Start()
        {
            if(AutoStart) StartLearnOperation(LearnSettings.OperationType);
            
            DifficultyView.SetDifficulty(Settings.Difficult);
            DifficultyView.Difficulty.onValueChanged.AddListener(DifficultyChangeHandler);
        }

        void OnDestroy()
        {
            DifficultyView.Difficulty.onValueChanged.RemoveListener(DifficultyChangeHandler);

            if (CurrentOperation == null) return;
            CurrentOperation.OnStartAgain -= StartAgainHandler;
            CurrentOperation.Dispose();
        }

        public void ResetAndStartLearnOperation(int operationType)
        {
            if(operationType > Enum.GetValues(typeof(LearnSettings.LearnOperationType)).Length) return;

            Settings.OperationType = (LearnSettings.LearnOperationType)operationType;
            CurrentOperation.Reset();
            StartAgainHandler();
        }

        public void StartLearnOperation(LearnSettings.LearnOperationType operationType)
        {
            CurrentOperation = operationType switch
            {
                LearnSettings.LearnOperationType.Addition => 
                    new OperationAddition(LearnSettings, LearnResultView, LearnOperationView),
                LearnSettings.LearnOperationType.Subtraction => 
                    new OperationSubtraction(LearnSettings, LearnResultView, LearnOperationView),
                LearnSettings.LearnOperationType.Multiplication => 
                    new OperationMultiplication(LearnSettings, LearnResultView, LearnOperationView),
                LearnSettings.LearnOperationType.Division => 
                    new OperationDivision(LearnSettings, LearnResultView, LearnOperationView),

                _ => throw new NotSupportedException($"Operation {operationType} not supported.")
            };
            CurrentOperation.OnStartAgain += StartAgainHandler;
            CurrentOperation.StartOperation();
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void DifficultyChangeHandler(int difficultyDropdownValue)
        {
            if(difficultyDropdownValue > Enum.GetValues(typeof(LearnSettings.LearnDifficult)).Length) return;
            
            Settings.Difficult = (LearnSettings.LearnDifficult)difficultyDropdownValue;
            DifficultyView.SetDifficulty(Settings.Difficult);
            ResetAndStartLearnOperation((int)Settings.OperationType);
        }

        void StartAgainHandler()
        {
            CurrentOperation.OnStartAgain -= StartAgainHandler;
            CurrentOperation.Dispose();
            StartLearnOperation(LearnSettings.OperationType);
        }
    }
}