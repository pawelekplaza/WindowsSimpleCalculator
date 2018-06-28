using Calc.Helpers;
using Calc.Models;
using System;
using System.Windows.Input;

namespace Calc.ViewModels
{
    public class CalcViewModel : BaseViewModel
    {
        private CalcModel _model = new CalcModel();

        public CalcViewModel()
        {
            ClearAllCommand = new RelayCommand(() =>
            {
                CurrentValue = "0";
                LastCalcValue = 0;
                TotalValue = 0;
                SetBothOperations(Operation.None);
            });

            ClearCommand = new RelayCommand(() =>
            {
                CurrentValue = "0";
            });

            RemoveCommand = new RelayCommand(() =>
            {
                if (CurrentValue == "0")
                    return;

                if (CurrentValue.Length == 1)
                {
                    CurrentValue = "0";
                    return;
                }

                CurrentValue = CurrentValue.Remove(CurrentValue.Length - 1, 1);
            });

            DivisionCommand = new RelayCommand(() => DoOperation(Operation.Division));

            MultiplicationCommand = new RelayCommand(() => DoOperation(Operation.Multiplication));

            SubtractionCommand = new RelayCommand(() => DoOperation(Operation.Subtraction));

            AdditionCommand = new RelayCommand(() => DoOperation(Operation.Addition));

            NegationCommand = new RelayCommand(() =>
            {
                if (CurrentValue == "0")
                    return;

                if (CurrentValue.StartsWith("-"))
                    CurrentValue = CurrentValue.Remove(0, 1);
                else
                    CurrentValue = CurrentValue.Insert(0, "-");
            });

            CommaCommand = new RelayCommand(() =>
            {
                if (CurrentValue.Contains(","))
                    return;

                CurrentValue += ",";
            });

            EqualsCommand = new RelayCommand(() =>
            {
                if (LastOperation == Operation.None)
                    LastCalcValue = CurrentValueAsDouble;

                CalculateCurrentValue(LastCalcValue, LastCalcOperation);
                LastOperation = Operation.Equation;
                OperationStack = "";
            });

            DigitCommand = new RelayCommand<string>((digit) =>
            {
                if (CurrentValue == "0" || LastOperation != Operation.None)
                    CurrentValue = "";

                LastOperation = Operation.None;
                CurrentValue += digit;
            });
        }

        public string CurrentValue
        {
            get { return _model.CurrentValue; }
            set { _model.CurrentValue = value; RaisePropertyChanged(nameof(CurrentValue)); }
        }

        public double CurrentValueAsDouble => Convert.ToDouble(CurrentValue);

        public double TotalValue
        {
            get { return _model.TotalValue; }
            set
            {
                _model.TotalValue = value;
                RaisePropertyChanged(nameof(TotalValue)); }
        }

        public string TotalValueString => TotalValue.ToString();

        public double LastCalcValue
        {
            get => _model.LastCalcValue;
            set { _model.LastCalcValue = value; RaisePropertyChanged(nameof(LastCalcValue)); }
        }

        public string OperationStack
        {
            get => _model.OperationStack;
            set { _model.OperationStack = value; RaisePropertyChanged(nameof(OperationStack)); }
        }

        public Operation LastOperation
        {
            get { return _model.LastOperation; }
            set { _model.LastOperation = value; RaisePropertyChanged(nameof(LastOperation)); }
        }

        public Operation LastCalcOperation
        {
            get => _model.LastCalcOperation;
            set { _model.LastCalcOperation = value; RaisePropertyChanged(nameof(LastCalcOperation)); }
        }

        public ICommand ClearAllCommand
        {
            get { return _model.ClearAllCommand; }
            set { _model.ClearAllCommand = value; RaisePropertyChanged(nameof(ClearAllCommand)); }
        }

        public ICommand ClearCommand
        {
            get { return _model.ClearCommand; }
            set { _model.ClearCommand = value; RaisePropertyChanged(nameof(ClearCommand)); }
        }

        public ICommand RemoveCommand
        {
            get { return _model.RemoveCommand; }
            set { _model.RemoveCommand = value; RaisePropertyChanged(nameof(RemoveCommand)); }
        }

        public ICommand DivisionCommand
        {
            get { return _model.DivisionCommand; }
            set { _model.DivisionCommand = value; RaisePropertyChanged(nameof(DivisionCommand)); }
        }

        public ICommand DigitCommand
        {
            get { return _model.DigitCommand; }
            set { _model.DigitCommand = value; RaisePropertyChanged(nameof(DigitCommand)); }
        }

        public ICommand MultiplicationCommand
        {
            get { return _model.MultiplicationCommand; }
            set { _model.MultiplicationCommand = value; RaisePropertyChanged(nameof(MultiplicationCommand)); }
        }

        public ICommand SubtractionCommand
        {
            get { return _model.SubtractionCommand; }
            set { _model.SubtractionCommand = value; RaisePropertyChanged(nameof(SubtractionCommand)); }
        }

        public ICommand AdditionCommand
        {
            get { return _model.AdditionCommand; }
            set { _model.AdditionCommand = value; RaisePropertyChanged(nameof(AdditionCommand)); }
        }

        public ICommand NegationCommand
        {
            get { return _model.NegationCommand; }
            set { _model.NegationCommand = value; RaisePropertyChanged(nameof(NegationCommand)); }
        }

        public ICommand CommaCommand
        {
            get { return _model.CommaCommand; }
            set { _model.CommaCommand = value; RaisePropertyChanged(nameof(CommaCommand)); }
        }

        public ICommand EqualsCommand
        {
            get { return _model.EqualsCommand; }
            set { _model.EqualsCommand = value; RaisePropertyChanged(nameof(EqualsCommand)); }
        }

        private void UpdateOperationStack(string value, Operation operation)
        {
            OperationStack += $"{ value } { operation.GetSign() } ";
        }

        private void ChangeOperationStackLastSign(Operation operation)
        {
            OperationStack = OperationStack.Remove(OperationStack.Length - 1, 1) + operation.GetSign();
        }

        private void SetBothOperations(Operation operation)
        {
            LastOperation = operation;
            LastCalcOperation = operation;
        }

        private void DoOperation(Operation operation)
        {
            if (LastOperation == operation)
                return;

            else if (LastOperation == Operation.None)
            {
                DoBasicCalculation(operation);
                OperateTotalValue(operation);
                return;
            }
            else if (LastOperation == Operation.Equation)
            {
                DoBasicCalculation(operation);
                return;
            }

            SetBothOperations(operation);
            return;
        }

        /// <summary>
        /// Only for <see cref="DoOperation(Operation)"/> purposes.
        /// </summary>
        private void DoBasicCalculation(Operation operation)
        {
            UpdateOperationStack(CurrentValue, operation);
            SetBothOperations(operation);
            LastCalcValue = CurrentValueAsDouble;
        }

        /// <summary>
        /// Only for <see cref="DoOperation(Operation)"/> purposes.
        /// </summary>
        private void OperateTotalValue(Operation operation)
        {
            if (TotalValue == 0)
            {
                TotalValue = CurrentValueAsDouble;
                return;
            }

            TotalValue = operation.Operate(TotalValue, CurrentValueAsDouble);
            CurrentValue = TotalValue.ToString();
        }

        /// <summary>
        /// Only for <see cref="EqualsCommand"/> purposes.
        /// </summary>
        private void CalculateCurrentValue(double value, Operation operation)
        {
            var current = CurrentValueAsDouble;

            if (LastOperation == Operation.None)
            {
                CurrentValue = operation.Operate(TotalValue, current).ToString();
                return;
            }

            current = operation.Operate(current, value);
            CurrentValue = current.ToString();
        }
    }
}
