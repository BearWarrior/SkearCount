using NUnit.Framework;
using System.Linq;

namespace SkearCount.Tests
{
    [TestFixture]
    class OperationTest
    {
        [Test]
        public void Test_Constructor()
        {
            var debiter = new User("debiter");
            var crediter = new User("crediter");
            Operation op = new Operation(debiter, crediter, 100);

            Assert.AreEqual(op.Debiter.Name, "debiter");
            Assert.AreEqual(op.Crediters.First().Name, "crediter");
            Assert.AreEqual(op.Amount, 100);
        }

        [Test]
        public void Test_ToString()
        {
            var debiter = new User("debiter");
            var crediter = new User("crediter");
            Operation op = new Operation(debiter, crediter, 100);

            var tostring = op.ToString();
            var expected = "debiter -> crediter : 100,00";
            Assert.AreEqual(tostring, expected);
        }
    }
}
