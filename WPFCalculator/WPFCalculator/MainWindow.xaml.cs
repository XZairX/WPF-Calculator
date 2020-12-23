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

namespace WPFCalculator
{
    public partial class MainWindow : Window
    {
        private readonly List<object> _listHistory = new List<object>();

        private double _input;
        private double _sum;
        string _operation;

        public MainWindow()
        {
            InitializeComponent();
            ResetAll();
        }

        private void ResetAll()
        {
            _listHistory.Clear();
            _input = 0;
            _sum = 0;
            _operation = string.Empty;
            SetOutputText(_input);
        }

        private void SetOutputText<T>(T text)
        {
            TextOutput.Text = text.ToString();
        }

        private void Button_Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            double number = Convert.ToDouble(button.Content.ToString());
            _input = (_input * 10) + number;
            SetOutputText(_input);
        }

        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            _listHistory.Add(_input);

            Button button = (Button)sender;
            _operation = button.Content.ToString();
            if (_operation != "=")
            {
                _listHistory.Add(_operation);

                _input = 0;
                _operation = string.Empty;
                SetOutputText(_input);

            }
            else
            {
                if (_listHistory.Count < 3)
                {
                    _sum = 0;
                }
                else
                {
                    _sum = Convert.ToDouble(_listHistory[0]);

                    double number;
                    string operation = null;
                    for (int i = 1; i < _listHistory.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            operation = Convert.ToString(_listHistory[i]);
                        }
                        else
                        {
                            number = Convert.ToDouble(_listHistory[i]);

                            switch (operation)
                            {
                                case "+":
                                    _sum += number;
                                    break;
                                case "-":
                                    _sum -= number;
                                    break;
                            }
                        }
                    }
                    _input = 0;
                    _listHistory.Clear();
                    SetOutputText(_sum);
                }
            }
        }

        private void Button_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        private void Button_ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            _input = 0;
        }
    }
}
