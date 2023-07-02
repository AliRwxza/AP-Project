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
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for GetEmailWindow.xaml
    /// </summary>
    public partial class GetEmailWindow : Window
    {
        public GetEmailWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void EmailField_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (Validation.Email(EmailField.Text))
                EmailField.Style = (Style)FindResource("TextBox");
            else
                EmailField.Style = (Style)FindResource("TextBoxError");
        }

        private void Button_Click (object sender, RoutedEventArgs e)
        {
            string fromAddress = GlobalVariables.SourceEmail;
            string toAddress = EmailField.Text; // Replace with the recipient's email address
            string subject = "Package Delivery Report";
            string message = "Your Package with the [Id number] was delivered\n" + // replace the Id number
                "Now you can let us know your opinion. ";

            // send an email with the message above
            Email.SendEmail(fromAddress, toAddress, subject, message);
        }
    }
}
