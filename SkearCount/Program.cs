using SkearCount;
using System;
using System.Collections.Generic;

namespace Tricount
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init Users
            User _userA = new User("A");
            User _userB = new User("B");
            User _userC = new User("C");
            User _userD = new User("D");
            User _userE = new User("E");
            var users = new List<User>() {_userA,_userB,_userC,_userD,_userE};

            //Create skearCount instance
            var skearCount = new SkearCount.SkearCount(users);

            //Add operations
            skearCount.AddOperation(new Operation(_userB, new List<User>() { _userB, _userC, _userD }, 15));
            skearCount.AddOperation(new Operation(_userC, new List<User>() { _userC, _userD }, 50));
            skearCount.AddOperation(new Operation(_userA, new List<User>() { _userA, _userB, _userC }, 10));
            foreach (var op in skearCount.Operations)
                Console.WriteLine(op.ToString());
            Console.WriteLine();

            //Get the operations to balance the skearCount
            List<Operation> operations = skearCount.GetOperationsToBalance();
            foreach(var op in operations)
                Console.WriteLine(op.ToString());
            Console.WriteLine();

            //Get the current balance of the userA
            Console.WriteLine(skearCount.GetCurrentBalanceForUser(_userA));

            //Debug
            Console.Read();
        }
    }
}
