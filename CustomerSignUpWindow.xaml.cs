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

        private void NameBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            Regex nameValidationPattern = new Regex(@"^[a-zA-Z]{3,32}$");

            if (!nameValidationPattern.IsMatch(FirstNameBox.Text))
            {
                FirstNameBox.Style = (Style)FindResource("TextBoxError");
                bools[0] = false;
            }
            else if (nameValidationPattern.IsMatch(FirstNameBox.Text))
            {
                FirstNameBox.Style = (Style)FindResource("SignUpPageTextBox");
                bools[0] = true;
            }
        }

        private void NameBox2_TextChanged (object sender, TextChangedEventArgs e)
        {
            Regex nameValidationPattern = new Regex(@"^[a-zA-Z]{3,32}$");

            if (!nameValidationPattern.IsMatch(LastNameBox.Text))
            {
                LastNameBox.Style = (Style)FindResource("TextBoxError");
                bools[1] = false;
            }
            else if (nameValidationPattern.IsMatch(LastNameBox.Text))
            {
                LastNameBox.Style = (Style)FindResource("SignUpPageTextBox");
                bools [1] = true;
            }
        }

        private void Ssn_TextChanged (object sender, TextChangedEventArgs e)
        {
            Regex SsnValidation = new Regex(@"^00\d{8}$");

            if (!SsnValidation.IsMatch(SsnField.Text))
            {
                SsnField.Style = (Style)FindResource("TextBoxError");
                bools[2] = false;
            }
            else if (SsnValidation.IsMatch(SsnField.Text))
            {
                // hmmm
                SsnField.Style = (Style)FindResource("SignUpPageTextBox");
                bools[2] = true;
            }
        }

        private void EmailField_TextChanged (object sender, TextChangedEventArgs e)
        {
            Regex emailValidation = new Regex(@"^[a-zA-Z]{3,32}@[a-zA-Z]{3,32}.[a-zA-Z]{2,3}$");

            if (!emailValidation.IsMatch(EmailField.Text))
            {
                EmailField.Style = (Style)FindResource("TextBoxError");
                bools[3] = false;
            }
            else if (emailValidation.IsMatch(EmailField.Text))
            {
                EmailField.Style = (Style)FindResource("SignUpPageTextBox");
                bools[3] = true;
            }
        }

        private void PhoneNumberField_TextChanged (object sender, TextChangedEventArgs e)
        {
            Regex phonenumberPattern = new Regex(@"^09\d{9}$");

            if (!phonenumberPattern.IsMatch(PhoneNumberField.Text))
            {
                PhoneNumberField.Style = (Style)FindResource("TextBoxError");
                bools[4] = false;
            }
            else if (phonenumberPattern.IsMatch(PhoneNumberField.Text))
            {
                PhoneNumberField.Style = (Style)FindResource("SignUpPageTextBox");
                bools[4] = true;
            }
        }

        private void CustomerSignUpPageSubmitButtonClick (object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < 5; i++)
            {
                if (bools[i] == false)
                {
                    MessageBox.Show("Problems remain.");

                    return;
                }
            }

            // generate a random and unique username and password(both must be unique)
            // E-mail the username and password to the customer
            // add the customer information

            Close();
        }
        private string RandomUsername()
        {
            string connectionString = "Data Source=;Initial Catalog=Post;Integrated Security = true;MultipleActiveResultSets=true";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    string EmployeeselectQuery = "SELECT * FROM Employees";
                    List<Employee> employees = SQL.ReadEmployeesData(connection, EmployeeselectQuery);

                    string CustomerselectQuery = "SELECT * FROM Customers";
                    List<Customer> customers = SQL.ReadCustomersData(connection, CustomerselectQuery);
                    Random random = new Random();
                    while (true)
                    {
                        string RandomUsername = "user";
                        for (int i = 0; i < 4; i++)
                        {
                            RandomUsername += random.Next(10).ToString();
                        }
                        bool Exist = false;
                        foreach (var username in employees.Select(x => x.UserName))
                        {
                            if (RandomUsername == username)
                            {
                                Exist = true;
                            }
                        }
                        foreach (var username in customers.Select(x => x.UserName))
                        {
                            if (RandomUsername == username)
                            {
                                Exist = true;
                            }
                        }
                        if (!Exist)
                        {
                            return RandomUsername;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading data: " + ex.Message);
                    return null;
                }
            }
        } 
    }
}
