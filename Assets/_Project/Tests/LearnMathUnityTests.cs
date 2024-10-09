using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using LearnMath.Learn;
using LearnMath.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace LearnMath.UnityTests
{
    [TestFixture]
    public class LearnMathUnityTests : MonoBehaviour
    {
        const float WaitTimeEachTest = 0f;
        LearnMath _learnMath;

        [OneTimeSetUp]
        public void SetUp()
        {
            const string sceneName = "LearnMathTest";
            SceneManager.sceneLoaded += OnSceneLoaded;
            var scene = SceneManager.LoadSceneAsync(sceneName);
            Assert.IsNotNull(scene, $"Scene {sceneName} not found");
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new WaitForSeconds(WaitTimeEachTest);
            _learnMath.CurrentOperation.Reset();
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            _learnMath = FindFirstObjectByType<LearnMath>();
            Assert.IsNotNull(_learnMath, "LearnMath component should be present in the scene.");
        }

        [UnityTest]
        [TestCase(LearnSettings.LearnOperationType.Addition, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Subtraction, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Multiplication, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Division, ExpectedResult = null)]
        public IEnumerator StartLearnOperation_ShouldStartCorrectOperation(LearnSettings.LearnOperationType operationType)
        {
            // Arrange
            //SetUp

            // Act
            _learnMath.StartLearnOperation(operationType);

            // Wait for the operation to start
            yield return null;

            // Assert
            Assert.IsNotNull(_learnMath.CurrentOperation);

            var expectedType = operationType switch
            {
                LearnSettings.LearnOperationType.Addition => typeof(OperationAddition),
                LearnSettings.LearnOperationType.Subtraction => typeof(OperationSubtraction),
                LearnSettings.LearnOperationType.Multiplication => typeof(OperationMultiplication),
                LearnSettings.LearnOperationType.Division => typeof(OperationDivision),
                _ => throw new NotSupportedException($"Operation type {operationType} not supported.")
            };
            Assert.IsInstanceOf(expectedType, _learnMath.CurrentOperation);
        }

        [UnityTest]
        [TestCase(LearnSettings.LearnOperationType.Addition, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Subtraction, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Multiplication, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Division, ExpectedResult = null)]
        public IEnumerator LearnOperation_OperationHaveCorrectDigits(LearnSettings.LearnOperationType operationType)
        {
            _learnMath.StartLearnOperation(operationType);
            
            yield return null;
            yield return null;

            var operationDigits = _learnMath.OperationView.GetComponentsInChildren<Number>();
            Assert.IsNotNull(operationDigits);
            Assert.AreEqual(_learnMath.CurrentOperation.Number1, operationDigits[0].GetDigit());
            Assert.AreEqual(_learnMath.CurrentOperation.Number2, operationDigits[1].GetDigit());
        }

        [UnityTest]
        [TestCase(LearnSettings.LearnOperationType.Addition, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Subtraction, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Multiplication, ExpectedResult = null)]
        [TestCase(LearnSettings.LearnOperationType.Division, ExpectedResult = null)]
        public IEnumerator LearnOperation_OperationHaveAValidAnswer(LearnSettings.LearnOperationType operationType)
        {
            _learnMath.StartLearnOperation(operationType);
            
            yield return null;
            yield return null;

            var operationDigits = _learnMath.ResultView.GetComponentsInChildren<Number>();
            Assert.IsNotNull(operationDigits);
            Assert.IsTrue(operationDigits.Any(digit => digit.GetDigit() == _learnMath.CurrentOperation.Result));
        }
    }
}