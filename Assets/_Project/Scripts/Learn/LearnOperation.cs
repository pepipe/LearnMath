using System;
using Cysharp.Threading.Tasks;

namespace LearnMath.Learn
{
    public abstract class LearnOperation : IDisposable
    {
        public int Number1 { get; protected set; }
        public int Number2 { get; protected set; }
        public int Result { get; protected set; }
        public string Sign { get; protected set; }

        public Action OnStartAgain;

        protected readonly LearnSettings _settings;
        protected readonly LearnResult _learnResult;

        readonly ILearnResultView _resultView;
        readonly int _startAgainWaitTime;

        protected LearnOperation(LearnSettings settings, ILearnResultView resultView)
        {
            _settings = settings;
            _resultView = resultView;
            _startAgainWaitTime = settings.WaitTimeForNextQuestion;
            _learnResult = new LearnResult(_settings, _resultView, this);
            _learnResult.OnAnswer += HandleAnswer;
        }

        public abstract void StartOperation();

        public abstract void StartOperation(int number1, int number2);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Reset()
        {
            _resultView.Reset();
        }

        void Dispose(bool disposing)
        {
            if (!disposing) return;
            _learnResult.OnAnswer -= HandleAnswer;
            _learnResult.Dispose();
        }

        void HandleAnswer(bool isCorrect)
        {
            if(!isCorrect) return;

            StartAgain().Forget();
        }

        async UniTaskVoid StartAgain()
        {
            await UniTask.Delay(_startAgainWaitTime);

            Reset();
            await UniTask.NextFrame();
            OnStartAgain?.Invoke();
        }
    }
}