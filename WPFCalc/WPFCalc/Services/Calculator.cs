using System;
using WPFCalc.Repository;

namespace WPFCalc.Services
{
    public class Calculator
    {
        readonly ButtonValues _buttonValues;
        public double? firstArgument;

        public Calculator()
        {
            _buttonValues = new ButtonValues();
            firstArgument = null;
        }

        /// <summary>
        /// Performs inputed operation
        /// </summary>
        public (string Input, string Output) Calculate(string calculationInput, string calculationHistory, string operation)
        {
            calculationInput = calculationInput.Replace('.', ',');

            double temp = double.Parse(calculationInput);

            if (firstArgument == null)
            {
                if (operation == _buttonValues.COS)
                {
                    calculationInput = Math.Cos(temp).ToString("G5");
                    calculationHistory = $"cos ({temp})";
                }
                else if (operation == _buttonValues.INVERSE)
                {
                    calculationInput = (1d / temp).ToString("G5");
                    calculationHistory = "1 / " + temp;
                }
                else if (operation == _buttonValues.SQRT)
                {
                    calculationInput = Math.Sqrt(temp).ToString("G5");
                    calculationHistory = operation + temp;
                }
                else if (operation == _buttonValues.PLUS
                    || operation == _buttonValues.MINUS
                    || operation == _buttonValues.MULTIPY
                    || operation == _buttonValues.DIVIDE)
                     {
                         calculationHistory = temp + operation;
                         firstArgument = temp;
                     }
            }
            else if (operation != _buttonValues.EQUALS)
            {
                if (operation == _buttonValues.COS ||
                    operation == _buttonValues.SQRT ||
                    operation == _buttonValues.INVERSE)
                {
                    return (calculationInput, calculationHistory);
                }

                calculationHistory = firstArgument + operation;
            }


            if (operation == _buttonValues.EQUALS && (calculationHistory.ToString().Length > 0))
            {
                calculationHistory += calculationInput;

                operation = calculationHistory.ToString()[calculationHistory.ToString().Length - calculationInput.Length - 1].ToString();
                if (operation == _buttonValues.PLUS)
                {
                    calculationInput = (firstArgument + double.Parse(calculationInput)).ToString();
                }
                else if (operation == _buttonValues.MINUS)
                {
                    calculationInput = (firstArgument - double.Parse(calculationInput)).ToString();
                }
                else if (operation == _buttonValues.MULTIPY)
                {
                    double.TryParse((firstArgument * double.Parse(calculationInput)).ToString(), out double FormattedResult);
                    calculationInput = FormattedResult.ToString("G5");
                }
                else if (operation == _buttonValues.DIVIDE)
                {
                    double.TryParse((firstArgument / double.Parse(calculationInput)).ToString(), out double FormattedResult);
                    calculationInput = FormattedResult.ToString("G5");
                }
                else
                {
                    calculationHistory = calculationInput.Replace(calculationInput, string.Empty);
                }

                firstArgument = null;
            }

            calculationInput = calculationInput.Replace(',', '.');

            return (calculationInput, calculationHistory);
        }
    }
}
