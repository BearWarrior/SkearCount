using System;
using System.Collections.Generic;
using System.Linq;

namespace SkearCount
{
    public class SkearCount
    {
        public List<User> Users { get; }

        public List<Operation> Operations { get; }

        private Calculator _calculator { get; }

        public SkearCount(List<User> users)
        {
            Users = users;
            Operations = new List<Operation>();
            _calculator = new Calculator();
        }

        public bool AddOperation(Operation operation)
        {
            if (operation.Debiter == null)
                return false;

            if (operation.Crediters == null || !operation.Crediters.Any())
                return false;

            if (operation.Amount == 0)
                return false;

            Operations.Add(operation);
            return true;
        }

        public bool AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
                return false;

            Users.Add(user);
            return true;
        }

        public List<Operation> GetOperationsToBalance()
        {
            _calculator.Process(Operations);
            return _calculator.Operations;
        }

        public void DisplayAllUsers()
        {
            foreach(var user in Users)
                Console.WriteLine(user);
        }

        public IEnumerable<Operation> GetOperationsFromUser(User user)
        {
            foreach (var op in Operations)
            {
                if (op.Debiter != user)
                    continue;

                yield return op;
            }
        }

        public float GetCurrentBalanceForUser(User user)
        {
            return Operations.Where(u => u.Debiter == user).Select(u => u.Amount).Sum();
        }
    }
}
