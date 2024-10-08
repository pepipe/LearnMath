using System;
using LearnMath.Learn;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace LearnMath
{
    public class LearnMath : MonoBehaviour
    {
        [SerializeField] LearnSettings LearnSettings;
        [SerializeField] LearnResultView LearnResultView;
        [SerializeField] LearnOperationView LearnOperationView;
        [SerializeField] bool AutoStart = true;

        public LearnSettings Settings => LearnSettings;
        public LearnOperationView OperationView => LearnOperationView;
        public LearnResultView ResultView => LearnResultView;
        public LearnOperation CurrentOperation { get; private set; }

        void Start()
        {
            if(AutoStart) StartLearnOperation(LearnSettings.OperationType);
        }

        void OnDestroy()
        {
            if (CurrentOperation == null) return;
            CurrentOperation.OnStartAgain -= StartAgainHandler;
            CurrentOperation.Dispose();
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

        void StartAgainHandler()
        {
            CurrentOperation.OnStartAgain -= StartAgainHandler;
            CurrentOperation.Dispose();
            StartLearnOperation(LearnSettings.OperationType);
        }
    }
}