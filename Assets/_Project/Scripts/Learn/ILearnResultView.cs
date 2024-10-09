using System;
using System.Collections.Generic;
using LearnMath.UI;

namespace LearnMath.Learn
{
    public interface ILearnResultView
    {
        event Action<bool> OnAnswer;

        void SetupOperation(Number numberPrefab);
        void StartOperation(LearnOperation operation, IEnumerable<int> answers);
        void Reset();
    }
}