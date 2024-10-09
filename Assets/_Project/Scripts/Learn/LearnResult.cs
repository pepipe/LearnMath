using System;
using System.Collections.Generic;
using LearnMath.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LearnMath.Learn
{
    public class LearnResult : IDisposable
    {
        public event Action<bool> OnAnswer;

        readonly LearnSettings _settings;
        readonly ILearnResultView _view;
        readonly LearnOperation _operation;

        public LearnResult(LearnSettings settings, ILearnResultView view, LearnOperation operation)
        {
            _settings = settings;
            _view = view;
            _operation = operation;

            _view.SetupOperation(_settings.NumberPrefab);
            _view.OnAnswer += HandleAnswer;
        }

        public void StartOperation()
        {
            var answers = new HashSet<int> { _operation.Result };
            while (answers.Count < _settings.AnswersCount)
            {
                answers.Add(GetAnswer(_settings.Difficult, _operation.Result));
            }

            _view.StartOperation(_operation, answers.Shuffle());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        void Dispose(bool disposing)
        {
            if (!disposing) return;
            _view.OnAnswer -= HandleAnswer;
        }

        int GetAnswer(LearnSettings.LearnDifficult difficult, int result)
        {
            int min = Mathf.Max(result - 5, 0);
            int max = Mathf.Max(min + 7, min + _settings.AnswersCount + 1);
            switch (difficult)
            {
                default:
                case LearnSettings.LearnDifficult.Easy:
                case LearnSettings.LearnDifficult.Medium:
                case LearnSettings.LearnDifficult.Hard:
                    return RandomRangeExcept(min, max, result);
            }
        }

        static int RandomRangeExcept(int min, int max, int exclude)
        {
            int randomValue = Random.Range(min, max);
            if (randomValue == exclude) randomValue++;
            
            return randomValue;
        }

        void HandleAnswer(bool isCorrect)
        {
            OnAnswer?.Invoke(isCorrect);
        }
    }
}