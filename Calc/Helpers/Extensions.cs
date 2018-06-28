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
    }
}
