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
    /// Interaction logic for TakeCustomerIdNumber.xaml
    /// </summary>
    public partial class TakeCustomerIdNumber : Window
    {
        static bool validation = false;
        Customer customer;
        public TakeCustomerIdNumber ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void SearchButtonClick (object sender, RoutedEventArgs e)
        {
            // if customer is already a member
            if (validation)
            {
                // if (ssn exists) :
                List<Customer> Customers = SQL.ReadCustomersData(); 
                for (int i = 0; i < Customers.Count(); i++)
                {
                    if (Customers[i].SSN == SsnInputBox.Text)
                    {
                        customer = Customers[i];
                        // go to the order-taking menu
                        OrderWindow order = new OrderWindow(customer);
                        order.Show();
                        Close();
                        return;
                    }
                }
                MessageBox.Show("No customer was found with this SSN!\nTry registering the customer.");
                CustomerSignUpWindow customerSignUpWindow = new CustomerSignUpWindow();
                customerSignUpWindow.Show();
            }
            else
            {
                MessageBox.Show("Invalid SSN!");
            }
        }

        private void SsnInputBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            SsnInputBox.Style = Validation.SSN(SsnInputBox.Text) ? (Style)FindResource("CustomerSsnField") : (Style)FindResource("TextBoxError");
            validation = Validation.SSN(SsnInputBox.Text);


            //Regex SsnValidation = new Regex(@"^00\d{8}$");

            //if (!SsnValidation.IsMatch(SsnInputBox.Text))
            //{
            //    SsnInputBox.Style = (Style)FindResource("TextBoxError");
            //    validation = false;
            //}
            //else if (SsnValidation.IsMatch(SsnInputBox.Text))
            //{
            //    SsnInputBox.Style = (Style)FindResource("CustomerSsnField");
            //    validation = true;
            //}
        }
    }
}
