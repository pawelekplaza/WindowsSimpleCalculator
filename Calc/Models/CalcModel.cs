using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calc.Models
{
    public class CalcModel
    {
        public string CurrentValue { get; set; } = "0";
        public double TotalValue { get; set; }
        public string OperationStack { get; set; } = "";
        public ICommand ClearAllCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand DivisionCommand { get; set; }
        public ICommand DigitCommand { get; set; }
        public ICommand MultiplicationCommand { get; set; }
        public ICommand SubtractionCommand { get; set; }
        public ICommand AdditionCommand { get; set; }
        public ICommand NegationCommand { get; set; }
        public ICommand CommaCommand { get; set; }
        public ICommand EqualsCommand { get; set; }
        public Operation LastOperation { get; set; }        
    }
}
