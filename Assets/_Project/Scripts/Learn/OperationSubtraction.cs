using Random = UnityEngine.Random;

namespace LearnMath.Learn
{
    public sealed class OperationSubtraction : LearnOperation
    {
        readonly IOperationView _view;

        public OperationSubtraction(LearnSettings settings, ILearnResultView resultView, IOperationView view) 
            : base(settings, resultView)
        {
            Sign = "-";
            _view = view;
            _view.SetupOperation(_settings.NumberPrefab, _settings.SignPrefab, Sign);
        }

        public override void StartOperation()
        {
            Number1 = GetNumber(_settings.Difficult);
            Number2 = GetNumber(_settings.Difficult, Number1);
            Result = Number1 - Number2;

            _view.StartOperation(Number1, Number2);
            _learnResult.StartOperation();
        }

        public override void StartOperation(int number1, int number2)
        {
            Number1 = number1;
            Number2 = number2;
            Result = Number1 - Number2;

            _view.StartOperation(Number1, Number2);
            _learnResult.StartOperation();
        }

        public override void Reset()
        {
            base.Reset();
            _view.Reset();
        }

        static int GetNumber(LearnSettings.LearnDifficult difficult, int? number1 = null)
        {
            switch (difficult)
            {
                default:
                case LearnSettings.LearnDifficult.Easy:
                    return number1.HasValue ? Random.Range(1, number1.Value) : Random.Range(2, 10);
                case LearnSettings.LearnDifficult.Medium:
                    return number1.HasValue ? Random.Range(1, number1.Value) : Random.Range(2, 30);
                case LearnSettings.LearnDifficult.Hard:
                    return number1.HasValue ? Random.Range(1, number1.Value) : Random.Range(2, 100);
            }
        }
    }
}