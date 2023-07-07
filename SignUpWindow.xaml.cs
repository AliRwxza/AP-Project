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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        static bool[] bools = {false, false, false, false, false};
        public SignUpWindow ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void SignUpPagePasswordCheckBox (object sender, RoutedEventArgs e)
        {
            if (SignUpPageCheckBox.IsChecked == true)
            {
                PasswordEntry1.Visibility = Visibility.Collapsed;
                PasswordEntry2.Visibility = Visibility.Collapsed;

                PasswordEntry1Txt.Text = PasswordEntry1.Password;
                PasswordEntry2Txt.Text = PasswordEntry2.Password;
                
                PasswordEntry1Txt.Visibility = Visibility.Visible;
                PasswordEntry2Txt.Visibility = Visibility.Visible;
            }
            else if (SignUpPageCheckBox.IsChecked == false || SignUpPageCheckBox.IsChecked == null)
            {
                PasswordEntry1.Visibility = Visibility.Visible;
                PasswordEntry2.Visibility = Visibility.Visible;

                PasswordEntry1Txt.Text = PasswordEntry1.Password;
                PasswordEntry2Txt.Text = PasswordEntry2.Password;

                PasswordEntry1Txt.Visibility = Visibility.Collapsed;
                PasswordEntry2Txt.Visibility = Visibility.Collapsed;
            }
        }

        private void NameBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            FirstNameBox.Style = Validation.Name(FirstNameBox.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[0] = Validation.Name(FirstNameBox.Text);
        }

        private void NameBox2_TextChanged (object sender, TextChangedEventArgs e)
        {
            LastNameBox.Style = Validation.Name(LastNameBox.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[1] = Validation.Name(LastNameBox.Text);
        }

        private void Id_TextChanged (object sender, TextChangedEventArgs e)
        {
            IdField.Style = Validation.EmployeeID(IdField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[2] = Validation.EmployeeID(IdField.Text);
        }

        private void EmailField_TextChanged (object sender, TextChangedEventArgs e)
        {
            EmailField.Style = Validation.Email(EmailField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[4] = Validation.Email(EmailField.Text);
        }

        private void SignUpPageSubmitButtonClick (object sender, RoutedEventArgs e)
        {
            Regex passwordVerification = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$");

            if (Validation.Password(PasswordEntry1.Password) && PasswordEntry1.Password == PasswordEntry2.Password)
            {
                if (SQL.ReadEmployeesData().Where(x => x.EmployeeID == IdField.Text).ToList().Count != 0)
                {
                    MessageBox.Show("Someone else has taken this employee id!");
                    return;
                }
                
                for (int i = 0; i < 5; i++)
                {
                    if (!bools[i])
                    {
                        MessageBox.Show("Problems Remain." + i);

                        return;
                    }
                }
                if (!Validation.UniqueUsername(UsernameField.Text))
                {
                    MessageBox.Show("Username taken!");
                    return;
                }
                if (!Validation.UniqueEmail(EmailField.Text))
                {
                    MessageBox.Show("This email is already in use! \nTry logging in.");
                    return;
                }
                Employee employee = new Employee(IdField.Text, FirstNameBox.Text, LastNameBox.Text, EmailField.Text, UsernameField.Text, PasswordEntry1.Password);
                SQL.InsertIntoTable(employee);
                Close();
            }
            else
            {
                PasswordEntry1.Style = (Style)FindResource("PasswordBoxError");
                PasswordEntry1Txt.Style = (Style)FindResource("TextBoxError");

                PasswordEntry2.Style = (Style)FindResource("PasswordBoxError");
                PasswordEntry2Txt.Style = (Style)FindResource("TextBoxError");

                MessageBox.Show("Password is not up to the standards.");
            }
        }

        private void UsernameField_TextChanged (object sender, TextChangedEventArgs e)
        {
            UsernameField.Style = Validation.UserName(UsernameField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[3] = Validation.UserName(UsernameField.Text);
        }

        private void PasswordEntry1Txt_TextChanged (object sender, TextChangedEventArgs e) => PasswordEntry1.Password = PasswordEntry1Txt.Text;

        private void PasswordEntry2Txt_TextChanged (object sender, TextChangedEventArgs e) => PasswordEntry2.Password = PasswordEntry2Txt.Text;
    }
}
