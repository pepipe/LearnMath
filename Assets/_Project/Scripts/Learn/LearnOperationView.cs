using LearnMath.UI;
using UnityEngine;
using UnityEngine.Pool;

namespace LearnMath.Learn
{
    public class LearnOperationView : MonoBehaviour, IOperationView
    {
        [SerializeField] Transform DigitsParent;

        IObjectPool<Digit> _digitsPool;
        IObjectPool<Sign> _signPool;
        string _operationSign;
        
        public void SetupOperation(Digit digitPrefab, Sign signPrefab, string operationSign)
        {
            _operationSign = operationSign;
            _digitsPool = new ObjectPool<Digit>(
                createFunc: () => CreateDigit(digitPrefab),
                actionOnGet: digit => digit.gameObject.SetActive(true),
                actionOnRelease: digit => digit.gameObject.SetActive(false),
                actionOnDestroy: digit => Destroy(digit.gameObject),
                collectionCheck: false,
                defaultCapacity: 2,
                maxSize: 2
            );

            _signPool = new ObjectPool<Sign>(
                createFunc: () => CreateSign(signPrefab),
                actionOnGet: digit => digit.gameObject.SetActive(true),
                actionOnRelease: digit => digit.gameObject.SetActive(false),
                actionOnDestroy: digit => Destroy(digit.gameObject),
                collectionCheck: false,
                defaultCapacity: 1,
                maxSize: 1
            );
        }

        public void StartOperation(int number1, int number2)
        {
            var digit = _digitsPool.Get();
            digit.InitDigit(false, number1);

            _signPool.Get();

            digit = _digitsPool.Get();
            digit.InitDigit(false, number2);
        }

        public void Reset()
        {
            foreach (Transform child in DigitsParent)
            {
                var digit = child.GetComponent<Digit>();
                if (digit) _digitsPool.Release(digit);
                else
                {
                    var sign = child.GetComponent<Sign>();
                    if (sign) _signPool.Release(sign);
                }
            }
        }

        Digit CreateDigit(Digit digitPrefab)
        {
            return Instantiate(digitPrefab, DigitsParent);
        }

        Sign CreateSign(Sign signPrefab)
        {
            var sign = Instantiate(signPrefab, DigitsParent);
            sign.SetSign(_operationSign);
            return sign;
        }
    }
}