using LearnMath.UI;

namespace LearnMath.Learn
{
    public interface IOperationView
    {
        void SetupOperation(Number numberPrefab, Sign signPrefab, string operationSign);
        void StartOperation(int number1, int number2);
        void Reset();
    }
}