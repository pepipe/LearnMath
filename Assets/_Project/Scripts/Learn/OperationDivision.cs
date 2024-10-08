using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LearnMath.Learn
{
    public sealed class OperationDivision : LearnOperation
    {
        readonly IOperationView _view;

        public OperationDivision(LearnSettings settings, ILearnResultView resultView, IOperationView view) 
            : base(settings, resultView)
        {
            Sign = "\u00F7"; //ASCII Code 247, division sign
            _view = view;
            _view.SetupOperation(_settings.DigitPrefab, _settings.SignPrefab, Sign);
        }

        public override void StartOperation()
        {
            Number1 = GetNumber(_settings.Difficult);
            Number2 = GetNumber(_settings.Difficult, Number1);
            Result = Number1 / Number2;

            _view.StartOperation(Number1, Number2);
            _learnResult.StartOperation();
        }

        public override void StartOperation(int number1, int number2)
        {
            if(number2 == 0) return;

            Number1 = number1;
            Number2 = number2;
            Result = Number1 / Number2;

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
            if(number1.HasValue)
            {
                var divisors = GetDivisors(number1.Value);
                int minDivisor = difficult == LearnSettings.LearnDifficult.Easy ? 0 : 1;
                return divisors[Random.Range(minDivisor, divisors.Count)];
            }

            switch (difficult)
            {
                default:
                case LearnSettings.LearnDifficult.Easy:
                    return GetRandomCompositeNumber(4, 25);
                case LearnSettings.LearnDifficult.Medium:
                    return GetRandomCompositeNumber(4, 50);
                case LearnSettings.LearnDifficult.Hard:
                    return GetRandomCompositeNumber(4, 100);
            }
        }
        
        static List<int> GetDivisors(int number)
        {
            var divisors = new List<int>();
            for (int i = 1; i <= number; i++)
            {
                if (number % i == 0)
                {
                    divisors.Add(i);
                }
            }
            return divisors;
        }
        
        static int GetRandomCompositeNumber(int min, int max)
        {
            int randomNumber;
            do
            {
                randomNumber = Random.Range(min, max + 1);
            } while (IsPrime(randomNumber));

            return randomNumber;
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number % 2 == 0) return false;
            if (number == 2) return true;

            for (int i = 3; i <= Mathf.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}