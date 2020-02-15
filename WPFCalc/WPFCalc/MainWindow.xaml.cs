using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double? firstArgument; 
        string operation = string.Empty; 

        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement c in MainGrid.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }

            firstArgument = null;
            Label.Content = string.Empty;
            textBox.Text = "0";
        }

        /// <summary>
        /// All buttons' onClick event handler
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Removing 
            if (textBox.Text == "NaN" || textBox.Text == "∞" || textBox.Text == "-")
            {
                textBox.Text = "0";
            }

            string input = (string)((Button)e.OriginalSource).Content;

            // Clearing the texblock
            if (input == "CE")
            {
                textBox.Text = "0";
                return;
            }

            // Clearing all data and textbox
            if (input == "C")
            {
                firstArgument = null;
                operation = string.Empty;

                textBox.Text = "0";
                Label.Content = string.Empty;

                return;
            }

            // Backspacing input 
            if (input == "←")
            {
                if (textBox.Text.Length == 1)
                {
                    textBox.Text = "0";
                }
                else
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                }

                return;
            }

            if (double.TryParse(input, out double inputDigit))
            {
                if (textBox.Text.Contains(".") &&
                   (textBox.Text.Length - textBox.Text.IndexOf(".")) > 5)
                {
                    return;
                }

                if (textBox.Text == "0")
                {
                    textBox.Text = inputDigit.ToString();
                }
                else
                {
                    textBox.Text += inputDigit.ToString();
                }

                LimitsCheck(-2e6 + 1, 4e6 - 1);
            }
            else
            {
                if (input == "±")
                {
                    textBox.Text = textBox.Text.Replace('.', ',');

                    if (double.TryParse(textBox.Text, out double inputNumber))
                    {
                        inputNumber = -inputNumber;
                        textBox.Text = inputNumber.ToString().Replace(',', '.');

                        LimitsCheck(-2e6 + 1, 4e6 - 1);

                        return;
                    }
                }

                if (input == "." && !textBox.Text.Contains("."))
                {
                    textBox.Text += ".";

                    return;
                }

                if (input == "." && textBox.Text.Contains("."))
                {
                    return;
                }

                operation = input;

                textBox.Text = textBox.Text.Replace('.', ',');
                Count();
                textBox.Text = textBox.Text.Replace(',', '.');
            }
        }

        /// <summary>
        /// Performs inputed operation
        /// </summary>
        private void Count()
        {
            LimitsCheck(-2e6 + 1, 4e6 - 1);
            double temp = double.Parse(textBox.Text);

            if (firstArgument == null)
            {
                switch (operation)
                {
                    case "cos(x)":
                        textBox.Text = Math.Cos(temp).ToString("G5");
                        Label.Content = $"cos ({temp})";
                        break;
                    case "1/x":
                        textBox.Text = (1d / temp).ToString("G5");
                        Label.Content = "1 / " + temp;
                        break;
                    case "√":
                        textBox.Text = Math.Sqrt(temp).ToString("G5");
                        Label.Content = operation + temp;
                        break;
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        Label.Content = temp + operation;
                        firstArgument = temp;
                        return;
                }

                firstArgument = null;
            }
            else if (operation != "=")
            {
                if (operation == "cos(x)" || operation == "√" || operation == "1/x")
                {
                    return;
                }

                Label.Content = firstArgument + operation;
            }

            if (operation == "=" && (Label.Content.ToString().Length > 0))
            {
                Label.Content += textBox.Text;

                switch (Label.Content.ToString()[Label.Content.ToString().Length - textBox.Text.Length - 1])
                {
                    case '+':
                        textBox.Text = (firstArgument + double.Parse(textBox.Text)).ToString();
                        break;
                    case '-':
                        textBox.Text = (firstArgument - double.Parse(textBox.Text)).ToString();
                        break;
                    case '*':
                        double.TryParse((firstArgument * double.Parse(textBox.Text)).ToString(), out double FormattedResult);
                        textBox.Text = FormattedResult.ToString("G5");
                        break;
                    case '/':
                        double.TryParse((firstArgument / double.Parse(textBox.Text)).ToString(), out FormattedResult);
                        textBox.Text = FormattedResult.ToString("G5");
                        break;
                    default:
                        Label.Content = textBox.Text.Replace(textBox.Text, string.Empty);
                        break;
                }

                firstArgument = null;
            }
        }

        /// <summary>
        /// Сhecking out textbox input on going beyond boundaries
        /// </summary>
        private void LimitsCheck(double lowerBound, double upperBound)
        {
            if (double.TryParse(textBox.Text, out double inputNumber))
            {
                if (inputNumber > upperBound)
                {
                    textBox.Text = upperBound.ToString();
                    MessageBox.Show("Values more than 4000000 are not allowed!");
                    return;
                }

                if (inputNumber < lowerBound)
                {
                    textBox.Text = lowerBound.ToString();
                    MessageBox.Show("Values less than -2000000 are not allowed!");
                }
            }
        }
    }
}
