using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Window
    {
        public EmployeePanel ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void CustomerSignUpButtonClick (object sender, RoutedEventArgs e)
        {
            CustomerSignUpWindow customerSignUpWindow = new CustomerSignUpWindow ();
            customerSignUpWindow.Show ();
        }

        private void EmployeePanelOrderButton (object sender, RoutedEventArgs e)
        {
            TakeCustomerIdNumber takeCustomerIdNumber = new TakeCustomerIdNumber ();
            takeCustomerIdNumber.Show();
        }

        private void SearchOrdersButtonClick (object sender, RoutedEventArgs e)
        {
            SearchOrders searchOrders = new SearchOrders ();
            searchOrders.Show ();
        }

        private void OrderDetailsButtonClick (object sender, RoutedEventArgs e)
        {
            OrderDetails orderDetails = new OrderDetails();
            orderDetails.Show();
        }

        private void ReceiptEmailButtonClick (object sender, RoutedEventArgs e)
        {
            string fromAddress = "alireza.rahimi1328@gmail.com";
            string toAddress = "mr.alza43@gmail.com"; // Replace with the recipient's email address
            string subject = "Package Delivery Report";
            string message = "Your Package with the [Id number] was delivered\n" + // replace the Id number
                "Now you let us know your opinion. ";

            // send an email with the message above
            SendEmail(fromAddress, toAddress, subject, message);
        }

        static void SendEmail (string fromAddress, string toAddress, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(fromAddress, toAddress, subject, body);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.Credentials = new NetworkCredential("alireza.rahimi1328@gmail.com", "keesvlwywkydxquz"); // Replace with your email and password
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);

                MessageBox.Show("Email sent successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send E-mail. Error message: " + ex.Message);
            }
        }

    }
}
