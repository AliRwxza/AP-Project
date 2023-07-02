using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            //Regex nameValidationPattern = new Regex(@"^[a-zA-Z]{3,32}$");
            //if (!nameValidationPattern.IsMatch(FirstNameBox.Text))
            //{
            //    FirstNameBox.Style = (Style)FindResource("TextBoxError");
            //    bools[0] = false;
            //}
            //else
            //{
            //    FirstNameBox.Style = (Style)FindResource("SignUpPageTextBox");
            //    bools[0] = true;
            //}
        }

        private void NameBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNameBox.Style = Validation.Name(LastNameBox.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[1] = Validation.Name(LastNameBox.Text);

            //Regex nameValidationPattern = new Regex(@"^[a-zA-Z]{3,32}$");
            //if (!nameValidationPattern.IsMatch(LastNameBox.Text))
            //{
            //    LastNameBox.Style = (Style)FindResource("TextBoxError");
            //    bools[1] = false;
            //}
            //else if (nameValidationPattern.IsMatch(LastNameBox.Text))
            //{
            //    LastNameBox.Style = (Style)FindResource("SignUpPageTextBox");
            //    bools[1] = true;
            //}
        }

        private void Ssn_TextChanged (object sender, TextChangedEventArgs e)
        {
            SsnField.Style = Validation.SSN(SsnField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[2] = Validation.SSN(SsnField.Text);


            //Regex SsnValidation = new Regex(@"^00\d{8}$");

            //if (!SsnValidation.IsMatch(SsnField.Text))
            //{
            //    SsnField.Style = (Style)FindResource("TextBoxError");
            //    bools[2] = false;
            //}
            //else if (SsnValidation.IsMatch(SsnField.Text))
            //{
            //    // hmmm
            //    SsnField.Style = (Style)FindResource("SignUpPageTextBox");
            //    bools[2] = true;
            //}
        }

        private void EmailField_TextChanged (object sender, TextChangedEventArgs e)
        {
            EmailField.Style = Validation.Email(EmailField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[3] = Validation.Email(EmailField.Text);

            //Regex emailValidation = new Regex(@"^[a-zA-Z]{3,32}@[a-zA-Z]{3,32}.[a-zA-Z]{2,3}$");
            //if (!emailValidation.IsMatch(EmailField.Text))
            //{
            //    EmailField.Style = (Style)FindResource("TextBoxError");
            //    bools[4] = false;
            //}
            //else if (emailValidation.IsMatch(EmailField.Text))
            //{
            //    EmailField.Style = (Style)FindResource("SignUpPageTextBox");
            //    bools[4] = true;
            //}
        }

        private void PhoneNumberField_TextChanged (object sender, TextChangedEventArgs e)
        {
            PhoneNumberField.Style = Validation.Phone(PhoneNumberField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[4] = Validation.Phone(PhoneNumberField.Text);

            //Regex phonenumberPattern = new Regex(@"^09\d{9}$");

            //if (!phonenumberPattern.IsMatch(PhoneNumberField.Text))
            //{
            //    PhoneNumberField.Style = (Style)FindResource("TextBoxError");
            //    bools[4] = false;
            //}
            //else if (phonenumberPattern.IsMatch(PhoneNumberField.Text))
            //{
            //    PhoneNumberField.Style = (Style)FindResource("SignUpPageTextBox");
            //    bools[4] = true;
            //}
        }

        private void CustomerSignUpPageSubmitButtonClick (object sender, RoutedEventArgs e)
        {
            
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

            Customer customer = new Customer(SsnField.Text, FirstNameBox.Text, LastNameBox.Text, EmailField.Text, PhoneNumberField.Text, 1000000, Username, Password);
            
            SQL.InsertIntoTable(customer);

            Email.SendEmail(GlobalVariables.SourceEmail, customer.Email, "Your username and password", $"Welcome to Post Company!\nUsername is: {Username}\nPassword is: {Password}");
            // generate a random and unique username and password(both must be unique)
            // E-mail the username and password to the customer
            // add the customer information

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
