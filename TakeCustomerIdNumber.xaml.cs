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
        public TakeCustomerIdNumber ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void SearchButtonClick (object sender, RoutedEventArgs e)
        {
            // if customer is already a member
            if (SsnInputBox.Text == "a")
            {
                // go to the order-taking menu
                Order order = new Order();
                order.Show();
            }
            // else, sign them up
            else
            {
                CustomerSignUpWindow customerSignUpWindow = new CustomerSignUpWindow ();
                customerSignUpWindow.Show ();
            }
            Close();
        }

        private void SsnInputBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            Regex SsnValidation = new Regex(@"^00\d{8}$");

            if (!SsnValidation.IsMatch(SsnInputBox.Text))
                SsnInputBox.Style = (Style)FindResource("TextBoxError");
            else if (SsnValidation.IsMatch(SsnInputBox.Text))
                SsnInputBox.Style = (Style)FindResource("SignUpPageTextBox");
        }
    }
}
