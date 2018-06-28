using Calc.Models;

namespace Calc.Helpers
{
    public static class Extensions
    {
        public static string GetSign(this Operation operation)
        {
            switch (operation)
            {
                case Operation.Addition: return "+";
                case Operation.Subtraction: return "-";
                case Operation.Multiplication: return "*";
                case Operation.Division: return "/";
                default: return null;
            }
        }

        public static double Operate(this Operation operation, double a, double b)
        {
            switch (operation)
            {
                case Operation.Addition: return a + b;
                case Operation.Subtraction: return a - b;
                case Operation.Multiplication: return a * b;
                case Operation.Division: return a / b;
                default: return 0;
            }
        }
    }
}
