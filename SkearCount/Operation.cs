using System.Collections.Generic;
using System.Linq;

namespace SkearCount
{
    public class Operation
    {
        public User Debiter { get; set; }

        public IEnumerable<User> Crediters { get; set; }

        public float Amount { get; set; }

        public Operation(User debiter, IEnumerable<User> crediter, float amount)
        {
            Debiter = debiter;
            Crediters = crediter;
            Amount = amount;
        }

        public Operation(User debiter, User crediter, float amount)
        {
            Debiter = debiter;
            Crediters = new List<User>() { crediter };
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Debiter.Name} -> {string.Join(",", Crediters.Select(c => c.Name)) } : {Amount:0.00}";
        }
    }
}
