using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SkearCount.Tests
{
    [TestFixture]
    class CalculatorTest
    {
        [Test]
        public void Test_Process()
        {
            var userA = new User("userA");
            var userB = new User("userB");
            var userC = new User("userC");
            var op1 = new Operation(userA, userB, 100);
            var op2 = new Operation(userB, userC, 50);

            var calculator = new Calculator();
            calculator.Process(new List<Operation>() { op1, op2 });

            var current = calculator.ToString();
            var expected = "userB -> userA : 50,00\r\nuserC -> userA : 50,00\r\n";

            Assert.AreEqual(current, expected);
        }

        [Test]
        public void Test_MergeOperations()
        {
            var userA = new User("userA");
            var userB = new User("userB");
            var op1 = new Operation(userA, userB, 100);
            var op2 = new Operation(userA, userB, 50);

            var final = Calculator.MergeOperations(op1, op2);

            Assert.AreEqual(final.Debiter, userA);
            Assert.AreEqual(final.Crediters.First(), userB);
            Assert.AreEqual(final.Amount, 150);
        }
        
        [Test]
        public void Test_ReduceOperation()
        {
            var userA = new User("userA");
            var userB = new User("userB");
            var userC = new User("userC");
            var op1 = new Operation(userA, userB, 10);
            var op2 = new Operation(userA, userB, 20);
            var op3 = new Operation(userB, userC, 30);
            var op4 = new Operation(userB, userC, 40);

            var operations = new List<Operation>() { op1, op2, op3, op4};

            while (Calculator.ReduceOperations(operations)){ }

            Assert.AreEqual(operations.Count, 2);
            Assert.AreEqual(operations[0].Debiter, userA);
            Assert.AreEqual(operations[0].Crediters.First(), userB);
            Assert.AreEqual(operations[0].Amount, 30);
            Assert.AreEqual(operations[1].Debiter, userB);
            Assert.AreEqual(operations[1].Crediters.First(), userC);
            Assert.AreEqual(operations[1].Amount, 70);
        }

        [Test]
        public void Test_ReduceChaslesInf()
        {
            var userA = new User("userA");
            var userB = new User("userB");
            var userC = new User("userC");
            var op1 = new Operation(userA, userB, 10);
            var op2 = new Operation(userB, userC, 20);

            Calculator.ReduceChasle(op1, op2);

            Assert.AreEqual(op1.Debiter, userA);
            Assert.AreEqual(op1.Crediters.First(), userC);
            Assert.AreEqual(op1.Amount, 10);
            Assert.AreEqual(op2.Debiter, userB);
            Assert.AreEqual(op2.Crediters.First(), userC);
            Assert.AreEqual(op2.Amount, 10);
        }

        [Test]
        public void Test_ReduceChaslesSup()
        {
            var userA = new User("userA");
            var userB = new User("userB");
            var userC = new User("userC");
            var op1 = new Operation(userA, userB, 20);
            var op2 = new Operation(userB, userC, 10);

            Calculator.ReduceChasle(op1, op2);

            Assert.AreEqual(op1.Debiter, userA);
            Assert.AreEqual(op1.Crediters.First(), userB);
            Assert.AreEqual(op1.Amount, 10);
            Assert.AreEqual(op2.Debiter, userA);
            Assert.AreEqual(op2.Crediters.First(), userC);
            Assert.AreEqual(op2.Amount, 10);
        }

        [Test]
        public void Test_ReduceChaslesEqu()
        {
            var userA = new User("userA");
            var userB = new User("userB");
            var userC = new User("userC");
            var op1 = new Operation(userA, userB, 20);
            var op2 = new Operation(userB, userC, 20);

            Calculator.ReduceChasle(op1, op2);

            Assert.AreEqual(op1.Debiter, userA);
            Assert.AreEqual(op1.Crediters.First(), userC);
            Assert.AreEqual(op1.Amount, 20);
            Assert.AreEqual(op2.Amount, 0);
        }

        [Test]
        public void Test_Optimization1()
        {
            var userA = new User("A");
            var userB = new User("B");
            var userC = new User("C");
            var userD = new User("D");
            var userE = new User("E");

            var op1 = new Operation(userA, userB, 15);
            var op2 = new Operation(userB,userC, 50);
            var op3 = new Operation(userA, userC, 10);
            var operations = new List<Operation>() { op1, op2, op3 };

            while (Calculator.Optimization1(operations)) { }

            Assert.AreEqual(operations.Count, 3);

            Assert.AreEqual(operations[0].Debiter, userA);
            Assert.AreEqual(operations[0].Crediters.Count(), 1);
            Assert.AreEqual(operations[0].Crediters.First(), userC);
            Assert.AreEqual(operations[0].Amount, 15);

            Assert.AreEqual(operations[1].Debiter, userB);
            Assert.AreEqual(operations[1].Crediters.Count(), 1);
            Assert.AreEqual(operations[1].Crediters.First(), userC);
            Assert.AreEqual(operations[1].Amount, 35);

            Assert.AreEqual(operations[2].Debiter, userA);
            Assert.AreEqual(operations[2].Crediters.Count(), 1);
            Assert.AreEqual(operations[2].Crediters.First(), userC);
            Assert.AreEqual(operations[2].Amount, 10);
        }

        [Test]
        public void Test_SubdivideOperations()
        {
            var userA = new User("userA");
            var userB = new User("userB");
            var userC = new User("userC");
            var op1 = new Operation(userA, new List<User>() { userB, userC }, 20);

            var oldOperations = new List<Operation>() { op1 };

            var operations = Calculator.SubdivideOperations(oldOperations);

            Assert.AreEqual(operations.Count, 2);
            Assert.AreEqual(operations[0].Debiter, userB);
            Assert.AreEqual(operations[0].Crediters.Count(), 1);
            Assert.AreEqual(operations[0].Crediters.First(), userA);
            Assert.AreEqual(operations[0].Amount, 10);
            Assert.AreEqual(operations[1].Debiter, userC);
            Assert.AreEqual(operations[1].Crediters.Count(), 1);
            Assert.AreEqual(operations[1].Crediters.First(), userA);
            Assert.AreEqual(operations[1].Amount, 10);
        }
    }
}
