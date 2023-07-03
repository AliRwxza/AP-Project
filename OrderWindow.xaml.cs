﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //static bool IsValuable = false;
        Customer customer;
        double Price;
        public OrderWindow (ref Customer customer)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            this.customer = customer;
        }

        /////////////////////////////////////////////////////////

        private void ObjectOptionCheck (object sender, EventArgs e)
        {
            if (Object.IsChecked == true)
            {

                Document.IsChecked = false;
                Fragile.IsChecked = false;
                MainMenu.Header = "Object";
            }
            else
            {
                MainMenu.Header = "Not Selected";
            }
        }

        private void DocumentOptiUnCheck (object sender, EventArgs e)
        {
            if (Document.IsChecked == true)
            {
                Object.IsChecked = false;
                Fragile.IsChecked= false;
                MainMenu.Header = "Document";
            }
            else
            {
                MainMenu.Header = "Not Selected";
            }
        }

        private void FragileOptionCheck (object sender, EventArgs e)
        {
            if (Fragile.IsChecked == true)
            {
                Object.IsChecked = false;
                Document.IsChecked = false;
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
            //if (ValuableCheckBox.IsChecked == true)
            //{
            //    IsValuable = true;
            //}
            //else
            //{
            //    IsValuable = false;
            //}
        }

        /////////////////////////////////////////////////////////
        
        private void OrdinaryOptionCheck(object sender, RoutedEventArgs e)
        {
            if (Ordinary.IsChecked == true)
            {
                Express.IsChecked = false;
                MainMenu2.Header = "Ordinary";
            }
            else
            {
                MainMenu2.Header = "Not Selected";
            }
        }

        private void ExpressOptionCheck(object sender, RoutedEventArgs e)
        {
            if (Express.IsChecked == true)
            {
                Ordinary.IsChecked = false;
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
            {
                ratio += Math.Floor(weight/0.5) * 0.2;
            }
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
            Price = 10000 * ratio;
            PricaTag.Text = "Price : " + Price;
            if (SenderAddressBox.Text.Length == 0)
            {
                errors += "Sender Address, ";
            }
            if (ReceiverAddressBox.Text.Length == 0)
            {
                errors += "Receiver Address, ";
            }
            if (errors != string.Empty)
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
            if (customer.Wallet >= Price)
            {
                customer.Wallet -= Price;
                SQL.UpdateTable(customer);
                PostType postType = Enum.Parse<PostType>(MainMenu2.Name);
                PackageContent Content = Enum.Parse<PackageContent>(MainMenu.Name);
                List<Order> orders = SQL.ReadOrdersData();
                if (orders.Count() != 0)
                {
                    Order.LastOrderID = orders.Select(x => x.OrderID).Max();
                }
                Order order = new Order(Order.LastOrderID + 1, SenderAddressBox.Text, ReceiverAddressBox.Text, Content, ValuableCheckBox.IsChecked, double.Parse(WeightBox.Text), postType, PhoneNumberField.Text, PackageStatus.Submitted, customer.SSN, DateTime.Now);
                SQL.InsertIntoTable(order);
                //MessageBox.Show("Order registered!");
            }
            else
            {
                MessageBox.Show("Not enough balance in your wallet!");
                // go to charging page
                
            }
            Close();
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
