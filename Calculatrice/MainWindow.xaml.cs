using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string current_input = "0";
        private string current_operation = ""; // +-*/%
        
        /// <summary>
        /// to stock the value of the first number input in the calculation
        /// note: may replace it by a string and only change into a double during active calculation
        /// </summary>
        private double first_number = 0;

        private bool Is_new_calculation = true;

        private bool Is_operation_pressed = false;


        public MainWindow()
        {
            InitializeComponent();
            ResultTextBlock.Text = current_input;
        }

        /// <summary>
        /// event when a button associated to the event is clicked this one function will not be associated
        /// to any button to keep an example
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, RoutedEventArgs e) {}

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content?.ToString() ?? "";

            if (Is_new_calculation || current_input == "0" || Is_operation_pressed)
            {
                current_input = digit;
                Is_new_calculation = false;
                Is_operation_pressed = false;
            }
            else
                current_input += digit;

            ResultTextBlock.Text = current_input;
        }

        private void OperationButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; // alternative to (Button)sender
            string operation = button.Content.ToString();

            if (!Is_operation_pressed) //operation button not pressed just before / alternative:  Is_operation_pressed == false
            {
                if(!String.IsNullOrEmpty(current_operation))   // ex: input sequence "123+654-789=" when "-" is input, the calculation of 123 + 654 resolve
                {                                              //     doest happent with "+" beacause "current_operation" is empty
                    CalculateResult();
                }
                else
                {
                    first_number = double.Parse(current_input);
                }

                current_operation = operation;
                Is_operation_pressed = true;
            }
        }

        private void EqualButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; // alternative to (Button)sender
            string operation = button.Content.ToString();

            if (!String.IsNullOrEmpty(current_operation) && !Is_operation_pressed)   
            {
                CalculateResult();
                current_operation = "";
                Is_new_calculation = true;
            }
        }

        private void CalculateResult()
        {
            double second_number = double.Parse(current_input);
            double result = 0;

            switch (current_operation)
            {
                case "+":
                    result = first_number + second_number;
                    break;
                case "-":
                    result = first_number - second_number;
                    break;
                case "×":
                    result = first_number * second_number;
                    break;
                case "÷":
                    if (current_input != "0")
                    {
                        result = first_number / second_number;
                    }
                    else
                        MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            ResultTextBlock.Text = result.ToString();
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            current_input = "0";
            current_operation = "";
            first_number = 0;
            Is_new_calculation = true;
            Is_operation_pressed = false;
            ResultTextBlock.Text = current_input;
        }

        private void DecimalButtonClick(object sender, RoutedEventArgs e)
        {
            if (Is_operation_pressed)
            {
                current_input = "0.";
                Is_operation_pressed = false;
            }
            else if (!current_input.Contains("."))
            {
                Is_new_calculation = false;
                current_input += ".";
            }

            ResultTextBlock.Text = current_input;
        }
        
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Is_operation_pressed && current_input.Length > 0 && !Is_new_calculation)
            {
                //current_input = current_input[..^1]; not avaiable for all C#
                current_input = current_input.Substring(0, current_input.Length - 1);
                if (string.IsNullOrEmpty(current_input) || current_input == "-")
                {
                    current_input = "0";
                }
                ResultTextBlock.Text = current_input;
            }
        }

        private void PercentButtonClick(object sender, RoutedEventArgs e)
        {
            if(!Is_new_calculation && !Is_operation_pressed)
            {
                double number = double.Parse(current_input);
                number /= 100;
                current_input = number.ToString();
                ResultTextBlock.Text = current_input;
            }
        }

        private void PlusMinusButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Is_operation_pressed && !Is_new_calculation&& current_input != "0")
            {
                current_input = current_input.StartsWith("-") ? current_input.Substring(1) : "-" + current_input;
                ResultTextBlock.Text = current_input;
            }
        }


    }
}
