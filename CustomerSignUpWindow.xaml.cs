using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// Interaction logic for CustomerSignUpWindow.xaml
    /// </summary>
    public partial class CustomerSignUpWindow : Window
    {
        static bool[] bools = { false, false, false, false, false };
        public CustomerSignUpWindow ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstNameBox.Style = Validation.Name(FirstNameBox.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[0] = Validation.Name(FirstNameBox.Text);
        }

        private void NameBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNameBox.Style = Validation.Name(LastNameBox.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[1] = Validation.Name(LastNameBox.Text);
        }

        private void Ssn_TextChanged (object sender, TextChangedEventArgs e)
        {
            SsnField.Style = Validation.SSN(SsnField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[2] = Validation.SSN(SsnField.Text);
        }

        private void EmailField_TextChanged (object sender, TextChangedEventArgs e)
        {
            EmailField.Style = Validation.Email(EmailField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[3] = Validation.Email(EmailField.Text);
        }

        private void PhoneNumberField_TextChanged (object sender, TextChangedEventArgs e)
        {
            PhoneNumberField.Style = Validation.Phone(PhoneNumberField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[4] = Validation.Phone(PhoneNumberField.Text);
        }

        private void CustomerSignUpPageSubmitButtonClick (object sender, RoutedEventArgs e)
        {
            if (SQL.ReadCustomersData().Where(x => x.SSN == SsnField.Text).ToList().Count != 0)
            {
                MessageBox.Show("Someone else has registered with this ssn!");
                return;
            }
            if (!Validation.UniqueEmail(EmailField.Text))
            {
                MessageBox.Show("This email is already in use! \nTry logging in.");
                return;
            }
            if (!Validation.UniquePhone(PhoneNumberField.Text))
            {
                MessageBox.Show("Phone number in use! \nTry logging in.");
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                if (bools[i] == false)
                {
                    MessageBox.Show("Problems remain. " + i);

                    return;
                }
            }
            string Username = RandomUsername();
            string Password = RandomPassword();

            Customer customer = new Customer(SsnField.Text, FirstNameBox.Text, LastNameBox.Text, EmailField.Text, PhoneNumberField.Text, 0, Username, Password);
            
            SQL.InsertIntoTable(customer);
            new Thread(() => Email.SendEmail(GlobalVariables.SourceEmail, customer.Email, "Your username and password", $"Welcome to Post Company!\nUsername is: {Username}\nPassword is: {Password}")).Start(); 

            Close();
        }
        private string RandomUsername()
        {
            Random random = new Random();
            string RandomUsername = "user";
            while (true)
            {
                RandomUsername = "user";
                for (int i = 0; i < 4; i++)
                {
                    RandomUsername += random.Next(10).ToString();
                }
                if (!SQL.UserExist(RandomUsername))
                {
                    return RandomUsername;
                }
            }
        }
        private string RandomPassword()
        {
            Random random = new Random();
            string RandomPassword = "";
            while (true)
            {
                RandomPassword = "";
                for (int i = 0; i < 8; i++)
                {
                    RandomPassword += random.Next(10).ToString();
                }
                if (!SQL.PasswordExist(RandomPassword))
                {
                    return RandomPassword;
                }
            }
        }
    }
}
