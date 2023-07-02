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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void TextBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (LoginPagePasswordBox.Password.ToString() == LoginPageUsernameBox.Text.ToString())
            {
                //MessageBox.Show("Login Successful.");
            }
        }

        private void LoginButtonClick (object sender, RoutedEventArgs e)
        {
            object user = SQL.FindUSer(LoginPageUsernameBox.Text);
            // if the person is a employee:
            if (user is Employee)
            {
                if (((Employee)user).Password == LoginPagePasswordBox.Password)
                {
                    EmployeePanel employeePanel = new EmployeePanel();
                    employeePanel.Show();
                }
                else
                {
                    MessageBox.Show("Error: Wrong password!");
                }
            }

            // else if the person is a customer :
            else if (user is Customer) 
            {
                if (((Customer)user).Password == LoginPagePasswordBoxTxt.Text)
                {
                    CustomerPanel customerPanel = new CustomerPanel();
                    customerPanel.Show();
                }
                else
                {
                    MessageBox.Show("Error: Wrong password!");
                }
                
            }
            // username not found
            else if (user == null)
            {
                MessageBox.Show("Error: Username not found!");
            }
            //Close();
        }

        private void SignUpButtonClick (object sender, RoutedEventArgs e)
        {
            SignUpWindow temp = new SignUpWindow();
            temp.Show();
        }

        private void LoginWindowPasswordVisibilityChechbox (object sender, RoutedEventArgs e)
        {
            if (CheckBox1.IsChecked == true)
            {
                LoginPagePasswordBox.Visibility = Visibility.Collapsed;

                LoginPagePasswordBoxTxt.Text = LoginPagePasswordBox.Password;

                LoginPagePasswordBoxTxt.Visibility = Visibility.Visible;
            }
            else if (CheckBox1.IsChecked == false)
            {
                LoginPagePasswordBox.Visibility = Visibility.Visible;

                LoginPagePasswordBoxTxt.Visibility = Visibility.Collapsed;
            }
        }

        private void LoginPagePasswordBoxTxt_TextChanged (object sender, TextChangedEventArgs e) => LoginPagePasswordBox.Password = LoginPagePasswordBoxTxt.Text;
    }
}
