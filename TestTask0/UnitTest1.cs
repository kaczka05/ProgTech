using NUnit.Framework;
using LogicLayer; 
using DataLayer;

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
            _taskManager.SetNumbers(2, 5);
            int result = _taskManager.AddNumbers();  
            Assert.AreEqual(7, result);
        }

        [Test]
        public void AddNumbers_ZeroInput_ReturnsSameNumber()
        {
            _taskManager.SetNumbers(2, 0);
            int result = _taskManager.AddNumbers();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddNumbers_NegativeNumbers_ReturnsCorrectSum()
        {
            _taskManager.SetNumbers(-5, -7);
            int result = _taskManager.AddNumbers();
            Assert.AreEqual(-12, result);
        }

        [Test]
        public void SubstractNumbers_ValidInput_ReturnsCorrectSum()
        {
            _taskManager.SetNumbers(2, 5);
            int result = _taskManager.SubstractNumbers();
            Assert.AreEqual(-3, result);
        }

        [Test]
        public void SubstractNumbers_ZeroInput_ReturnsSameNumber()
        {
            _taskManager.SetNumbers(2, 0);
            int result = _taskManager.SubstractNumbers();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void SubstractNumbers_NegativeNumbers_ReturnsCorrectSum()
        {
            _taskManager.SetNumbers(-5, -3);
            int result = _taskManager.SubstractNumbers();
            Assert.AreEqual(-2, result);
        }

        [Test]
        public void MultiplyNumbers_ValidInput_ReturnsCorrectvalue()
        {
            _taskManager.SetNumbers(5, 7);
            int result = _taskManager.MultiplyNumbers();
            Assert.AreEqual(35, result);
        }

        [Test]
        public void MultiplyNumbers_InputWithOne()
        {
            _taskManager.SetNumbers(2, 1);
            int result = _taskManager.MultiplyNumbers();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void MultiplyNumbers_InputWithZero()
        {
            _taskManager.SetNumbers(2, 0);
            int result = _taskManager.MultiplyNumbers();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void MultiplyNumbers_OneNegativeNumber()
        {
            _taskManager.SetNumbers(2, -1);
            int result = _taskManager.MultiplyNumbers();
            Assert.AreEqual(-2, result);
        }

        [Test]
        public void MultiplyNumbers_TwoNegativeNumbers()
        {
            _taskManager.SetNumbers(-2, -5);
            int result = _taskManager.MultiplyNumbers();
            Assert.AreEqual(10, result);
        }

        [Test]
        public void DivideNumbers_ValidInput()
        {
            _taskManager.SetNumbers(15, 5);
            int result = _taskManager.DivideNumbers();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void DivideNumbers_InputWithOne()
        {
            _taskManager.SetNumbers(15, 1);
            int result = _taskManager.DivideNumbers();
            Assert.AreEqual(15, result);
        }

        [Test]
        public void DivideNumbers_InputWithZero()
        {
            _taskManager.SetNumbers(2, 0);
            int result = _taskManager.DivideNumbers();
            Assert.AreEqual(0, result);
        }
    }
}
