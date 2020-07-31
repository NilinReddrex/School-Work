using System;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private float a;
        private string operation = string.Empty;
        bool clearOnEntry = false;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void CalculatorFunction_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Display.Text))
            {
                return;
            }

            switch ((sender as Button).Content)
            {
                case "AC":
                    this.Display.Text = string.Empty;
                    break;
                case "DEL":
                    
                    this.Display.Text = this.Display.Text.Remove(Display.Text.Length - 1, 1);
                    break;
                case "(-)":
                    if (this.Display.Text[0] == '-')
                    {
                        this.Display.Text = this.Display.Text.Substring(1, this.Display.Text.Length - 1);
                    }
                    else
                    {
                        this.Display.Text = string.Concat("-", this.Display.Text);
                    }
                    break;
            }
        }



        private void Char_Click(object sender, RoutedEventArgs e)
        {
            if (this.clearOnEntry)
            {
                this.clearOnEntry = false;
                this.Display.Text = string.Empty;
            }

            Display.Text += (sender as Button).Content;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.operation))
            {
                string oper = (sender as Button).Content.ToString();

                if (oper != "=")
                {
                    if (float.TryParse(Display.Text, out float nums))
                    {
                        this.a = nums;
                        this.operation = oper;
                    }
                    else
                    {
                        Display.Text = $"ERROR: '{Display.Text}' is not a valid number.";
                    }
                }
            }
            else
            {
                if (float.TryParse(Display.Text, out float b))
                {
                    Display.Text = this.PerformCalulation(this.a, b, this.operation).ToString();
                    this.operation = string.Empty;
                }
                else
                {
                    Display.Text = $"ERROR: '{Display.Text}' is not a valid number.";
                }
            }

            this.clearOnEntry = true;
        }

        private float PerformCalulation(float a, float b, string operation)
        {
            float result = 0;
            switch (operation)
            {
                case "+":
                    result = a + b;
                    break;

                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    result = a / b;
                    break;
            }

            return result;
        }
    }



}
