using Random = UnityEngine.Random;

namespace LearnMath.Learn
{
    public sealed class OperationMultiplication : LearnOperation
    {
        readonly IOperationView _view;

        public OperationMultiplication(LearnSettings settings, ILearnResultView resultView, IOperationView view) 
            : base(settings, resultView)
        {
            Sign = "x";
            _view = view;
            _view.SetupOperation(_settings.DigitPrefab, _settings.SignPrefab, Sign);
        }

        public override void StartOperation()
        {
            Number1 = GetNumber(_settings.Difficult);
            Number2 = GetNumber(_settings.Difficult);
            Result = Number1 * Number2;

            _view.StartOperation(Number1, Number2);
            _learnResult.StartOperation();
        }

        public override void StartOperation(int number1, int number2)
        {
            Number1 = number1;
            Number2 = number2;
            Result = Number1 * Number2;

            _view.StartOperation(Number1, Number2);
            _learnResult.StartOperation();
        }

        public override void Reset()
        {
            base.Reset();
            _view.Reset();
        }

        static int GetNumber(LearnSettings.LearnDifficult difficult)
        {
            switch (difficult)
            {
                default:
                case LearnSettings.LearnDifficult.Easy:
                    return Random.Range(1, 10);
                case LearnSettings.LearnDifficult.Medium:
                    return Random.Range(1, 20);
                case LearnSettings.LearnDifficult.Hard:
                    return Random.Range(1, 31);
            }
        }
    }
}