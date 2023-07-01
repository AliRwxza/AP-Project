using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        static bool IsValuable = false;
        public OrderWindow ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        /////////////////////////////////////////////////////////

        private void ObjectOptionCheck (object sender, EventArgs e)
        {
            if (ObjectOption.IsChecked == true)
            {

                DocumentOption.IsChecked = false;
                FragileOption.IsChecked = false;
                MainMenu.Header = "Object";
            }
            else
            {
                MainMenu.Header = "Not Selected";
            }
        }

        private void DocumentOptiUnCheck (object sender, EventArgs e)
        {
            if (DocumentOption.IsChecked == true)
            {
                ObjectOption.IsChecked = false;
                FragileOption.IsChecked= false;
                MainMenu.Header = "Document";
            }
            else
            {
                MainMenu.Header = "Not Selected";
            }
        }

        private void FragileOptionCheck (object sender, EventArgs e)
        {
            if (FragileOption.IsChecked == true)
            {
                ObjectOption.IsChecked = false;
                DocumentOption.IsChecked = false;
                MainMenu.Header = "Fragile";
            }
            else
            {
                MainMenu.Header = "Not Selected";
            }
        }

        /////////////////////////////////////////////////////////

        private void ValuableCheckBoxCheck (object sender, RoutedEventArgs e)
        {
            if (ValuableCheckBox.IsChecked == true)
            {
                IsValuable = true;
            }
            else
            {
                IsValuable = false;
            }
        }

        /////////////////////////////////////////////////////////
        
        private void OrdinaryOptionCheck(object sender, RoutedEventArgs e)
        {
            if (OrdinaryOption.IsChecked == true)
            {
                ExpressOption.IsChecked = false;
                MainMenu2.Header = "Ordinary";
            }
            else
            {
                MainMenu2.Header = "Not Selected";
            }
        }

        private void ExpressOptionCheck(object sender, RoutedEventArgs e)
        {
            if (ExpressOption.IsChecked == true)
            {
                OrdinaryOption.IsChecked = false;
                MainMenu2.Header = "Express";
            }
            else
            {
                MainMenu2.Header = "Not Selected";
            }
        }

        private void CalculatePriceBottonClick (object sender, RoutedEventArgs e)
        {
            string errors = string.Empty;
            double ratio = 1;

            // base value is 10k IRR
            switch (MainMenu.Header)
            {
                case "Document":
                    ratio += 0.5;
                    break;
                case "Fragile":
                    ratio += 1;
                    break;
                case "Not Selected":
                    errors += "PackageType, ";
                    break;
            }
            if (ValuableCheckBox.IsChecked == true)
                ratio += 1;
            if (double.TryParse(WeightBox.Text, out double weight) && weight > 0.5)
                ratio += Math.Ceiling((weight - 0.5) * 2) * 0.2;
            else
                errors += "Weight, ";
            switch (MainMenu2.Header)
            {
                case "Not Selected":
                    errors += "Delivery Type, ";
                    break;
                case "Express":
                    ratio += 0.5;
                    break;
            }

            PricaTag.Text = "Price : " + Math.Floor(10 * ratio);

            if (errors !=  string.Empty)
            {
                MessageBox.Show("The mentioned fields have invalid values: " + errors.Remove(errors.Length - 2, 2));
            }
            else if (errors == string.Empty)
            {
                CaculatePriceButton.Visibility = Visibility.Collapsed;
                SubmitOrderButton.Visibility = Visibility.Visible;

                PricaTag.Visibility = Visibility.Visible;
            }
        }

        private void SubmitOrderButtonClick (object sender, RoutedEventArgs e)
        {
            double price = double.Parse(PricaTag.Text);

            // check the customer's wallet
            // if there were no problems, assign an ID number (based on the order's number)
            // and take the order's money from customer's wallet
            // set the order status to submitted
        }

        private void PhoneNumberField_TextChanged (object sender, TextChangedEventArgs e)
        {
            // check the phone number format
            Regex phonenumberPattern = new Regex(@"^09\d{9}$");

            if (PhoneNumberField.Text != string.Empty && !phonenumberPattern.IsMatch(PhoneNumberField.Text))
                PhoneNumberField.Style = (Style)FindResource("TextBoxError");
            else if (PhoneNumberField.Text == string.Empty || phonenumberPattern.IsMatch(PhoneNumberField.Text))
                PhoneNumberField.Style = (Style)FindResource("TextBox");
        }

        private void WeightBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(WeightBox.Text, out double temp))
            {
                WeightBox.Style = (Style)FindResource("TextBox");
            }
            else { WeightBox.Style = (Style)FindResource("TextBoxError"); }
        }
    }
}
