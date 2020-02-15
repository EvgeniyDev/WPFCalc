using System;
using System.Windows;
using System.Windows.Controls;
using WPFCalc.Repository;

namespace WPFCalc
{
    public partial class MainWindow : Window
    {
        double? firstArgument = null;
        string operation = string.Empty;
        readonly ButtonValues _buttonValues;

        public MainWindow()
        {
            InitializeComponent();

            _buttonValues = new ButtonValues();

            foreach (var element in MainGrid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += Button_Click;
                }
            }

            Input = _buttonValues.ZERO;
        }

        public string Input
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }
        public object Output
        {
            get => label.Content;
            set => label.Content = value;
        }


        /// <summary>
        /// All buttons' onClick event handler
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Removing
            if (Input == _buttonValues.NAN ||
                Input == _buttonValues.INFINITE ||
                Input == _buttonValues.MINUS)
            {
                Input = _buttonValues.ZERO;
            }

            string input = (string)((Button)e.OriginalSource).Content;

            // Clearing the texblock
            if (input == _buttonValues.CE)
            {
                Input = _buttonValues.ZERO;
                return;
            }

            // Clearing all data and textbox
            if (input == _buttonValues.C)
            {
                firstArgument = null;
                operation = string.Empty;

                Input = _buttonValues.ZERO;
                Output = string.Empty;

                return;
            }

            // Backspacing input
            if (input == _buttonValues.BACKSPACE)
            {
                if (Input.Length == 1)
                {
                    Input = _buttonValues.ZERO;
                }
                else
                {
                    Input = Input.Remove(Input.Length - 1);
                }

                return;
            }

            if (double.TryParse(input, out double inputDigit))
            {
                if (Input.Contains(".") &&
                   (Input.Length - Input.IndexOf(".")) > 5)
                {
                    return;
                }

                if (Input == _buttonValues.ZERO)
                {
                    Input = inputDigit.ToString();
                }
                else
                {
                    Input += inputDigit.ToString();
                }

                LimitsCheck(-2e6 + 1, 4e6 - 1);
            }
            else
            {
                if (input == _buttonValues.PLUSMINUS)
                {
                    Input = Input.Replace('.', ',');

                    if (double.TryParse(Input, out double inputNumber))
                    {
                        inputNumber = -inputNumber;
                        Input = inputNumber.ToString().Replace(',', '.');

                        LimitsCheck(-2e6 + 1, 4e6 - 1);

                        return;
                    }
                }

                if (input == "." && !Input.Contains("."))
                {
                    Input += ".";

                    return;
                }

                if (input == "." && Input.Contains("."))
                {
                    return;
                }

                operation = input;

                Input = Input.Replace('.', ',');
                Count();
                Input = Input.Replace(',', '.');
            }
        }

        /// <summary>
        /// Performs inputed operation
        /// </summary>
        private void Count()
        {
            LimitsCheck(-2e6 + 1, 4e6 - 1);
            double temp = double.Parse(Input);

            if (firstArgument == null)
            {
                if (operation == _buttonValues.COS)
                {
                    Input = Math.Cos(temp).ToString("G5");
                    Output = $"cos ({temp})";
                } 
                else if (operation == _buttonValues.HALF)
                {
                    Input = (1d / temp).ToString("G5");
                    Output = "1 / " + temp;
                } 
                else if (operation == _buttonValues.SQRT)
                {
                    Input = Math.Sqrt(temp).ToString("G5");
                    Output = operation + temp;
                } 
                else if (operation == _buttonValues.PLUS
                    || operation == _buttonValues.MINUS
                    || operation == _buttonValues.MULTIPY
                    || operation == _buttonValues.DIVIDE)
                {
                    Output = temp + operation;
                    firstArgument = temp;
                }
            }
            else if (operation != _buttonValues.EQUALS)
            {
                if (operation == _buttonValues.COS ||
                    operation == _buttonValues.SQRT ||
                    operation == _buttonValues.HALF)
                {
                    return;
                }

                Output = firstArgument + operation;
            }


            if (operation == _buttonValues.EQUALS && (Output.ToString().Length > 0))
            {
                Output += Input;

                var operation = Output.ToString()[Output.ToString().Length - Input.Length - 1].ToString();
                if (operation == _buttonValues.PLUS)
                {
                    Input = (firstArgument + double.Parse(Input)).ToString();
                }
                else if (operation == _buttonValues.MINUS)
                {
                    Input = (firstArgument - double.Parse(Input)).ToString();
                }
                else if (operation == _buttonValues.MULTIPY)
                {
                    double.TryParse((firstArgument * double.Parse(Input)).ToString(), out double FormattedResult);
                    Input = FormattedResult.ToString("G5");
                }
                else if (operation == _buttonValues.DIVIDE)
                {
                    double.TryParse((firstArgument / double.Parse(Input)).ToString(), out double FormattedResult);
                    Input = FormattedResult.ToString("G5");
                }
                else
                {
                    Output = Input.Replace(Input, string.Empty);
                }

                firstArgument = null;
            }
        }

        /// <summary>
        /// Сhecking out textbox input on going beyond boundaries
        /// </summary>
        private void LimitsCheck(double lowerBound, double upperBound)
        {
            if (double.TryParse(Input, out double inputNumber))
            {
                if (inputNumber > upperBound)
                {
                    Input = _buttonValues.ZERO;
                    MessageBox.Show("Values more than 4000000 are not allowed!");
                    return;
                }

                if (inputNumber < lowerBound)
                {
                    Input = _buttonValues.ZERO;
                    MessageBox.Show("Values less than -2000000 are not allowed!");
                }
            }
        }
    }
}
