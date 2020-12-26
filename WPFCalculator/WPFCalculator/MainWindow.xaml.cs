﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CalculatorApp;

namespace WPFCalculator
{
    public partial class MainWindow : Window
    {
        private readonly List<object> _listHistory = new List<object>();

        private string _input;
        private string _operation;
        private bool _isNewEquation;

        public MainWindow()
        {
            InitializeComponent();
            ClearAll();
        }

        private void ClearAll()
        {
            _listHistory.Clear();
            ResetInput();
            UpdateOutputText();
            ClearOperation();
            ClearHistoryText();
        }

        private void ResetInput() => _input = "0";

        private void UpdateOutputText() => _textOutput.Text = _input;

        private void ClearOperation() => _operation = null;
        
        private void ClearHistoryText() => _textHistory.Text = null;

        private void Button_SignChange_Click(object sender, RoutedEventArgs e)
        {
            if (_input[0] == '-')
            {
                _input = _input.Substring(1, _input.Length - 1);
            }
            else
            {
                _input = "-" + _input;
            }

            if (_listHistory.Count > 0)
            {
                _listHistory.Remove(_listHistory.Last());
                _listHistory.Add(_input);
            }
            UpdateOutputText();
        }

        private void Button_Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            double number = Convert.ToDouble(button.Content.ToString());

            if (_isNewEquation)
            {
                PrepareNewEquationEnvironment(number);
            }

            if (_input == "0")
            {
                _input = null;
            }

            _input += number;
            UpdateOutputText();
        }

        private void PrepareNewEquationEnvironment(object buttonClick)
        {
            if (buttonClick is double)
            {
                ClearAll();
            }
            else
            {
                ClearHistoryText();
            }
            _isNewEquation = false;
        }

        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _operation = button.Content.ToString();

            if (_isNewEquation)
            {
                PrepareNewEquationEnvironment(_operation);
            }
            _listHistory.Add(_input);
            _listHistory.Add(_operation);
            _textHistory.Text += $"{_input} {_operation} ";

            if (_operation != "=")
            {
                ResetInput();
                ClearOperation();
                UpdateOutputText();
            }
            else
            {
                CalculateSum(_listHistory);
            }
        }

        private void CalculateSum(List<object> listHistory)
        {
            double sum = Convert.ToDouble(listHistory[0]);
            string operation = Convert.ToString(listHistory[1]);
            double number;
            bool hasException = false;
            for (int i = 2; i < listHistory.Count; i++)
            {
                if (hasException)
                {
                    listHistory.Clear();
                    ResetInput();
                    break;
                }

                if (i % 2 == 0)
                {
                    number = Convert.ToDouble(listHistory[i]);
                    switch (operation)
                    {
                        case "+":
                            sum = Calculator.Add(sum, number);
                            break;
                        case "-":
                            sum = Calculator.Subtract(sum, number);
                            break;
                        case "*":
                            sum = Calculator.Multiply(sum, number);
                            break;
                        case "/":
                            try
                            {
                                sum = Calculator.Divide(sum, number);
                            }
                            catch (DivideByZeroException e)
                            {
                                _textHistory.Text = e.Message;
                                hasException = true;
                            }
                            break;
                        case "%":
                            try
                            {
                                sum = Calculator.Modulo(sum, number);
                            }
                            catch (DivideByZeroException e)
                            {
                                _textHistory.Text = e.Message;
                                hasException = true;
                            }
                            break;
                    }
                }
                else
                {
                    operation = Convert.ToString(listHistory[i]);
                }
            }
            _isNewEquation = true;
            UpdateOutputText();
        }

        private void Button_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void Button_ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            ResetInput();
            UpdateOutputText();
        }

        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            double input = Convert.ToDouble(_input);
            if (input >= 10 || input <= -10)
            {
                string inputString = _input.ToString();
                inputString = inputString.Substring(0, inputString.Length - 1);
                _input = inputString;
            }
            else
            {
                ResetInput();
            }
            UpdateOutputText();
        }
    }
}
