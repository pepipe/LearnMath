using LearnMath.UI;

namespace LearnMath.Learn
{
    public interface IOperationView
    {
        void SetupOperation(Digit digitPrefab, Sign signPrefab, string operationSign);
        void StartOperation(int number1, int number2);
        void Reset();
    }
}