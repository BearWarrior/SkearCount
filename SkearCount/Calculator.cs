using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkearCount
{
    public class Calculator
    {
        public List<Operation> Operations { get;  set; }

        public Calculator()
        {
            Operations = new List<Operation>();
        }

        public IEnumerable<Operation> Process(IReadOnlyList<Operation> operations)
        {
            Operations = SubdivideOperations(operations);

            while (Optimization1(Operations)) { }
            while (ReduceOperations(Operations)) { }

            return Operations;
        }

        public static List<Operation> SubdivideOperations(IReadOnlyList<Operation> operations)
        {
            var newOperations = new List<Operation>();
            foreach (var op in operations)
            {
                float amount = op.Amount / op.Crediters.Count();
                foreach (var debiter in op.Crediters.Where(c => c != op.Debiter))
                {
                    var newOperation = new Operation(debiter, op.Debiter, amount);
                    newOperations.Add(newOperation);
                }
            }
            return newOperations;
        }

        public static bool Optimization1(List<Operation> operations)
        {
            foreach (var op1 in operations)
            {
                foreach (var op2 in operations)
                {
                    if (op1 == op2)
                        continue;

                    if (op1.Crediters.First() != op2.Debiter)
                        continue;

                    ReduceChasle(op1, op2);
                    if (op2.Amount == 0)
                        operations.Remove(op2);
                    return true;
                }
            }
            return false;
        }

        public static void ReduceChasle(Operation op1, Operation op2)
        {
            if (op1.Amount < op2.Amount)
            {
                op1.Crediters = new List<User>() { op2.Crediters.First() };
                op2.Amount = op2.Amount - op1.Amount;
            }
            else if (op1.Amount > op2.Amount)
            {
                op1.Amount = op1.Amount - op2.Amount;
                op2.Debiter = op1.Debiter;
            }
            else
            {
                op1.Debiter = op1.Debiter;
                op1.Crediters = op2.Crediters;
                op1.Amount = op1.Amount;

                op2.Amount = 0;
            }
        }

        public static bool ReduceOperations(List<Operation> operations)
        {
            foreach (var op1 in operations)
            {
                foreach (var op2 in operations)
                {
                    if (op1 == op2)
                        continue;

                    if (op1.Debiter != op2.Debiter || op1.Crediters.First() != op2.Crediters.First())
                        continue;

                    operations.Add(MergeOperations(op1, op2));
                    operations.Remove(op1);
                    operations.Remove(op2);
                    return true;
                }
            }
            return false;
        }

        public static Operation MergeOperations(Operation op1, Operation op2)
        {
            var operation = new Operation(op1.Debiter, op1.Crediters.First(), op1.Amount + op2.Amount);
            return operation;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var operation in Operations)
                stringBuilder.AppendLine(operation.ToString());
            return stringBuilder.ToString();
        }
    }
}