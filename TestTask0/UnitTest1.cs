using NUnit.Framework;
using LogicLayer; // Ensure this namespace matches your logic layer
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
            _dataContainer = new Numbers();
        }

        [Test]
        public void AddNumbers_ValidInput_ReturnsCorrectSum()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = 7;
            int result = _taskManager.AddNumbers(num_1,num_2);  
            Assert.AreEqual(12, result);
        }

        [Test]
        public void AddNumbers_ZeroInput_ReturnsSameNumber()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = 0;
            int result = _taskManager.AddNumbers(num_1, num_2);  
            Assert.AreEqual(0, result);
        }

        [Test]
        public void AddNumbers_NegativeNumbers_ReturnsCorrectSum()
        {
            Numbers.num_1 = -5;
            Numbers.num_2 = -7;
            int result = _taskManager.AddNumbers(num_1, num_2);  
            Assert.AreEqual(-12, result);
        }

        [Test]
        public void SubstractNumbers_ValidInput_ReturnsCorrectSum()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = 2;
            int result = _taskManager.SubstractNumbers(num_1, num_2);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void SubstractNumbers_ZeroInput_ReturnsSameNumber()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = 0;
            int result = _taskManager.SubstractNumbers(num_1, num_2);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void SubstractNumbers_NegativeNumbers_ReturnsCorrectSum()
        {
            Numbers.num_1 = -5;
            Numbers.num_2 = -3;
            int result = _taskManager.SubstractNumbers(num_1, num_2);
            Assert.AreEqual(-2, result);
        }

        [Test]
        public void MultiplyNumbers_ValidInput_ReturnsCorrectvalue()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = 7;
            int result = _taskManager.MultiplyNumbers(num_1, num_2);
            Assert.AreEqual(35, result);
        }

        [Test]
        public void MultiplyNumbers_InputWithOne()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = 1;
            int result = _taskManager.MultiplyNumbers(num_1, num_2);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void MultiplyNumbers_InputWithZero()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = 0;
            int result = _taskManager.MultiplyNumbers(num_1, num_2);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void MultiplyNumbers_OneNegativeNumber()
        {
            Numbers.num_1 = 5;
            Numbers.num_2 = -1;
            int result = _taskManager.MultiplyNumbers(num_1, num_2);
            Assert.AreEqual(-5, result);
        }

        [Test]
        public void MultiplyNumbers_TwoNegativeNumbers()
        {
            Numbers.num_1 = -5;
            Numbers.num_2 = -7;
            int result = _taskManager.MultiplyNumbers(num_1, num_2);
            Assert.AreEqual(35, result);
        }

        [Test]
        public void DivideNumbers_ValidInput()
        {
            Numbers.num_1 = 15;
            Numbers.num_2 = 5;
            int result = _taskManager.DivideNumbers(num_1, num_2);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void DivideNumbers_InputWithOne()
        {
            Numbers.num_1 = 15;
            Numbers.num_2 = 1;
            int result = _taskManager.DivideNumbers(num_1, num_2);
            Assert.AreEqual(15, result);
        }

        [Test]
        public void DivideNumbers_InputWithZero()
        {
            Numbers.num_1 = 15;
            Numbers.num_2 = 0;
            int result = _taskManager.DivideNumbers(num_1, num_2);
            Assert.AreEqual(0, result);
        }
    }
}
