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

        private void NameBox2_TextChanged (object sender, TextChangedEventArgs e)
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

        private void Id_TextChanged (object sender, TextChangedEventArgs e)
        {
            IdField.Style = Validation.EmployeeID(IdField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[2] = Validation.EmployeeID(IdField.Text);

            //Regex IdValidation = new Regex(@"^\d{2}9\d{2}$");
            //if (!IdValidation.IsMatch(IdField.Text))
            //{
            //    IdField.Style = (Style)FindResource("TextBoxError");
            //    bools[2] = false;
            //}
            //else if (IdValidation.IsMatch(IdField.Text))
            //{
            //    IdField.Style = (Style)FindResource("SignUpPageTextBox");
            //    bools[2] = true;
            //}
        }

        private void EmailField_TextChanged (object sender, TextChangedEventArgs e)
        {
            EmailField.Style = Validation.Email(EmailField.Text) ? (Style)FindResource("SignUpPageTextBox") : (Style)FindResource("TextBoxError");
            bools[4] = Validation.Email(EmailField.Text);

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

        private void SignUpPageSubmitButtonClick (object sender, RoutedEventArgs e)
        {
            Regex passwordVerification = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$");

            //if (passwordVerification.IsMatch(PasswordEntry1.Password) && PasswordEntry1.Password == PasswordEntry2.Password)
            if (Validation.Password(PasswordEntry1.Password) && PasswordEntry1.Password == PasswordEntry2.Password)
            {
                //MessageBox.Show("here");
                for (int i = 0; i < 5; i++)
                {
                    if (!bools[i])
                    {
                        MessageBox.Show("Problems Remain." + i);

                        return;
                    }
                }

                new Employee(FirstNameBox.Text, LastNameBox.Text, IdField.Text, EmailField.Text, UsernameField.Text, PasswordEntry1.Password);
                MessageBox.Show("Employee added!");
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



            Regex nameValidationPattern = new Regex(@"^[a-zA-Z0-9]{3,32}$");
            if (!nameValidationPattern.IsMatch(UsernameField.Text))
            {
                UsernameField.Style = (Style)FindResource("TextBoxError");
                bools[3] = false;
            }
            else if (nameValidationPattern.IsMatch(UsernameField.Text))
            {
                UsernameField.Style = (Style)FindResource("SignUpPageTextBox");
                bools[3] = true;
            }
        }

        private void PasswordEntry1Txt_TextChanged (object sender, TextChangedEventArgs e) => PasswordEntry1.Password = PasswordEntry1Txt.Text;

        private void PasswordEntry2Txt_TextChanged (object sender, TextChangedEventArgs e) => PasswordEntry2.Password = PasswordEntry2Txt.Text;
    }
}
