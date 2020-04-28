using System.Windows;
using System.Windows.Controls;
using WPFCalc.Repository;
using WPFCalc.Services;

namespace WPFCalc
{
    public partial class CalculatorForm : Window
    {
        readonly ButtonValues _buttonValues;
        readonly Calculator _calculator;

        public CalculatorForm()
        {
            InitializeComponent();

            _buttonValues = new ButtonValues();
            _calculator = new Calculator();

            foreach (var element in MainGrid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += Button_Click;
                }
            }

            CalculationInput = _buttonValues.ZERO;
            CalculationHistory = string.Empty;
        }

        public string CalculationInput
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }
        public object CalculationHistory
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
            if (CalculationInput == double.NaN.ToString() ||
                CalculationInput == double.PositiveInfinity.ToString() || CalculationInput == double.NegativeInfinity.ToString() ||
                CalculationInput == _buttonValues.MINUS)
            {
                CalculationInput = _buttonValues.ZERO;
            }

            string buttonValue = (string)((Button)e.OriginalSource).Content;

            // Clearing the texblock
            if (buttonValue == _buttonValues.CE)
            {
                CalculationInput = _buttonValues.ZERO;
                return;
            }

            // Clearing all data and textbox
            if (buttonValue == _buttonValues.C)
            {
                _calculator.firstArgument = null;

                CalculationInput = _buttonValues.ZERO;
                CalculationHistory = string.Empty;

                return;
            }
            
            // Exponential notation check
            if (buttonValue == _buttonValues.BACKSPACE && CalculationInput.Contains("E"))
            {
                CalculationInput = _buttonValues.ZERO;
                return;
            }

            // Backspacing input
            if (buttonValue == _buttonValues.BACKSPACE)
            {
                if (CalculationInput.Length == 1)
                {
                    CalculationInput = _buttonValues.ZERO;
                }
                else
                {
                    CalculationInput = CalculationInput.Remove(CalculationInput.Length - 1);
                }

                return;
            }

            if (double.TryParse(buttonValue, out double inputDigit))
            {
                if (CalculationInput.Contains(".") &&
                   (CalculationInput.Length - CalculationInput.IndexOf(".")) > 5)
                {
                    return;
                }

                if (CalculationInput == _buttonValues.ZERO)
                {
                    CalculationInput = inputDigit.ToString();
                }
                else
                {
                    CalculationInput += inputDigit.ToString();
                }

                LimitsCheck(-2e6 + 1, 4e6 - 1);
            }
            else
            {
                if (buttonValue == _buttonValues.PLUSMINUS)
                {
                    CalculationInput = CalculationInput.Replace('.', ',');

                    if (double.TryParse(CalculationInput, out double inputNumber))
                    {
                        inputNumber = -inputNumber;
                        CalculationInput = inputNumber.ToString().Replace(',', '.');

                        LimitsCheck(-2e6 + 1, 4e6 - 1);

                        return;
                    }
                }

                if (buttonValue == "." && !CalculationInput.Contains("."))
                {
                    CalculationInput += ".";

                    return;
                }
                else if (buttonValue == "." && CalculationInput.Contains("."))
                {
                    return;
                }

                LimitsCheck(-2e6 + 1, 4e6 - 1);
                (CalculationInput, CalculationHistory) = _calculator.Calculate(CalculationInput, CalculationHistory.ToString(), buttonValue);
            }
        }

        /// <summary>
        /// Сhecking out textbox input on going beyond boundaries
        /// </summary>
        private void LimitsCheck(double lowerBound, double upperBound)
        {
            var caption = "The value is beyond limits";

            if (double.TryParse(CalculationInput, out double inputNumber))
            {
                if (inputNumber > upperBound)
                {
                    CalculationInput = _buttonValues.ZERO;
                    MessageBox.Show($"Values more than {upperBound} are not allowed!", caption);
                    return;
                }

                if (inputNumber < lowerBound)
                {
                    CalculationInput = _buttonValues.ZERO;
                    MessageBox.Show($"Values less than {lowerBound} are not allowed!", caption);
                }
            }
        }
    }
}
