using NUnit.Framework;
using LearnMath.Learn;
using Moq;

namespace LearnMath.EditorTests
{
    public class OperationTests
    {
        LearnSettings _settings;
        ILearnResultView _resultViewMock;
        IOperationView _operationViewMock;

        [SetUp]
        public virtual void SetUp()
        {
            _settings = new Mock<LearnSettings>().Object;
            _resultViewMock = new Mock<ILearnResultView>().Object;
            _operationViewMock = new Mock<IOperationView>().Object;
        }

        public class OperationAdditionTests : OperationTests
        {
            //LearnSettings _settings;
            OperationAddition _operation;
            
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
                _operation = new OperationAddition(_settings, _resultViewMock, _operationViewMock);
            }
            
            [Test]
            public void TestOperation()
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation();

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = _operation.Number1 + _operation.Number2;
                Assert.AreEqual(expectedResult, actualResult);
            }

            [Test]
            [TestCase(0, 0)]
            [TestCase(1, 1)]
            [TestCase(0, 6)]
            [TestCase(3, 6)]
            [TestCase(-10, 10)]
            public void TestOperationWithNumber(int number1, int number2)
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation(number1, number2);

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = number1 + number2;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        public class OperationSubtractionTests : OperationTests
        {
            OperationSubtraction _operation;

            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
                _operation = new OperationSubtraction(_settings, _resultViewMock, _operationViewMock);
            }
            
            [Test]
            public void TestOperation()
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation();

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = _operation.Number1 - _operation.Number2;
                Assert.AreEqual(expectedResult, actualResult);
            }
            
            [Test]
            [TestCase(0, 0)]
            [TestCase(1, 1)]
            [TestCase(0, 6)]
            [TestCase(3, 6)]
            [TestCase(-10, 10)]
            public void TestOperationWithNumber(int number1, int number2)
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation(number1, number2);

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = number1 - number2;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
        
        public class OperationMultiplicationTests : OperationTests
        {
            //LearnSettings _settings;
            OperationMultiplication _operation;
            
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
                _operation = new OperationMultiplication(_settings, _resultViewMock, _operationViewMock);
            }
            
            [Test]
            public void TestOperation()
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation();

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = _operation.Number1 * _operation.Number2;
                Assert.AreEqual(expectedResult, actualResult);
            }

            [Test]
            [TestCase(0, 0)]
            [TestCase(1, 1)]
            [TestCase(0, 6)]
            [TestCase(3, 6)]
            [TestCase(-10, 10)]
            public void TestOperationWithNumber(int number1, int number2)
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation(number1, number2);

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = number1 * number2;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
        
        public class OperationDivisionTests : OperationTests
        {
            //LearnSettings _settings;
            OperationDivision _operation;
            
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
                _operation = new OperationDivision(_settings, _resultViewMock, _operationViewMock);
            }
            
            [Test]
            public void TestOperation()
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation();

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = _operation.Number1 / _operation.Number2;
                Assert.AreEqual(expectedResult, actualResult);
            }

            [Test]
            [TestCase(0, 0)]
            [TestCase(1, 0)]
            [TestCase(1, 1)]
            [TestCase(0, 6)]
            [TestCase(6, 3)]
            [TestCase(5, -1)]
            [TestCase(10, -10)]
            public void TestOperationWithNumber(int number1, int number2)
            {
                //Arrange
                //[SetUp]

                //Act
                _operation.StartOperation(number1, number2);

                //Assert
                int expectedResult = _operation.Result;
                int actualResult = _operation.Number2 != 0 ? _operation.Number1 / _operation.Number2 : 0;
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
    }
}