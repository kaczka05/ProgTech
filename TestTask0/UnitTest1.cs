using NUnit.Framework;
using LogicLayer; // Ensure this namespace matches your logic layer

namespace TaskTests
{
    [TestFixture]
    public class TaskManagerTests
    {
        private TaskLogic _taskManager;

        [SetUp]
        public void Setup()
        {
            _taskManager = new TaskLogic();
        }

        [Test]
        public void AddNumbers_ValidInput_ReturnsCorrectSum()
        {
            int result = _taskManager.AddNumbers(5, 7);  
            Assert.AreEqual(12, result);
        }

        [Test]
        public void AddNumbers_ZeroInput_ReturnsSameNumber()
        {
            int result = _taskManager.AddNumbers(0, 10);  
            Assert.AreEqual(10, result);
        }

        [Test]
        public void AddNumbers_NegativeNumbers_ReturnsCorrectSum()
        {
            int result = _taskManager.AddNumbers(-3, -7);  
            Assert.AreEqual(-10, result);
        }

        [Test]
        public void SubstractNumbers_ValidInput_ReturnsCorrectSum()
        {
            int result = _taskManager.SubstractNumbers(5, 2);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void SubstractNumbers_ZeroInput_ReturnsSameNumber()
        {
            int result = _taskManager.SubstractNumbers(5, 0);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void SubstractNumbers_NegativeNumbers_ReturnsCorrectSum()
        {
            int result = _taskManager.SubstractNumbers(-5, -3);
            Assert.AreEqual(-2, result);
        }

        [Test]
        public void MultiplyNumbers_ValidInput_ReturnsCorrectvalue()
        {
            int result = _taskManager.MultiplyNumbers(3, 2);
            Assert.AreEqual(6, result);
        }

        [Test]
        public void MultiplyNumbers_InputWithOne()
        {
            int result = _taskManager.MultiplyNumbers(3, 1);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void MultiplyNumbers_InputWithZero()
        {
            int result = _taskManager.MultiplyNumbers(3, 0);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void MultiplyNumbers_OneNegativeNumber()
        {
            int result = _taskManager.MultiplyNumbers(3, -1);
            Assert.AreEqual(-3, result);
        }

        [Test]
        public void MultiplyNumbers_TwoNegativeNumbers()
        {
            int result = _taskManager.MultiplyNumbers(-3, -1);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void DivideNumbers_ValidInput()
        {
            int result = _taskManager.DivideNumbers(15, 5);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void DivideNumbers_InputWithOne()
        {
            int result = _taskManager.DivideNumbers(15, 1);
            Assert.AreEqual(15, result);
        }

        [Test]
        public void DivideNumbers_InputWithZero()
        {
            int result = _taskManager.DivideNumbers(15, 0);
            Assert.AreEqual(0, result);
        }
    }
}
