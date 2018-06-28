﻿using Calc.Helpers;
using Calc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                TotalValue = 0;
                LastOperation = Operation.None;
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

            DivisionCommand = new RelayCommand(() =>
            {
                if (LastOperation == Operation.None || LastOperation == Operation.Undeterminated)
                {
                    LastOperation = Operation.Division;
                    TotalValue = Convert.ToDouble(CurrentValue);
                    CurrentValue = "0";
                    return;
                }

                if (CurrentValue == "0")
                    return;

                TotalValue /= Convert.ToDouble(CurrentValue);
                RefreshTotalValue();
            });

            MultiplicationCommand = new RelayCommand(() =>
            {
                TotalValue *= Convert.ToDouble(CurrentValue);
                RefreshTotalValue();
            });

            SubtractionCommand = new RelayCommand(() =>
            {
                TotalValue -= Convert.ToDouble(CurrentValue);
                RefreshTotalValue();
            });

            AdditionCommand = new RelayCommand(() =>
            {
                if (LastOperation != Operation.None)
                {
                    if (LastOperation == Operation.Addition)
                        return;

                    ChangeOperationStackLastSign(Operation.Addition);
                    return;
                }

                TotalValue += Convert.ToDouble(CurrentValue);
                LastOperation = Operation.Addition;
                RefreshTotalValue();
            });

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

                //CurrentValue.Insert(CurrentValue.Length - 1, ",");
                CurrentValue += ",";
            });

            EqualsCommand = new RelayCommand(() =>
            {
                //TotalValue =
            });

            DigitCommand = new RelayCommand<string>((digit) =>
            {
                if (CurrentValue == "0")
                    CurrentValue = "";

                if (LastOperation == Operation.None)
                {
                    CurrentValue = "";
                    LastOperation = Operation.Undeterminated;
                }
                else if (LastOperation != Operation.Undeterminated)
                {
                    CurrentValue = "";
                    LastOperation = Operation.Undeterminated;
                }

                CurrentValue += digit;
            });
        }

        public string CurrentValue
        {
            get { return _model.CurrentValue; }
            set { _model.CurrentValue = value; RaisePropertyChanged(nameof(CurrentValue)); }
        }

        public double TotalValue
        {
            get { return _model.TotalValue; }
            set
            {
                _model.TotalValue = value;
                RaisePropertyChanged(nameof(TotalValue)); }
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

        private void RefreshTotalValue()
        {
            CurrentValue = TotalValue.ToString();
            LastOperation = Operation.None;
        }
    }
}