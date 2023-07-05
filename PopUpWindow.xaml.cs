using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        Customer LoggedInCustomer;
        double ChargeAmount;
        Window window;
        public PopUpWindow(Customer LoggedInCustomer, double ChargeAmount, Window window)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.LoggedInCustomer = LoggedInCustomer;
            this.ChargeAmount = ChargeAmount;
            this.window = window;
        }

        private void YesButton_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                string path = $"PaymentReciept_{LoggedInCustomer.UserName}_{DateTime.Now.ToString().Replace('/', '-').Replace(':', '-').Replace(' ', '-')}.pdf"; // must be in this format : "Payment Reciept " + ID + ".pdf"

                PdfWriter writer = new PdfWriter(path);

                // Create a new PDF document
                PdfDocument pdf = new PdfDocument(writer);

                // Create a new iText document
                iText.Layout.Document document = new(pdf);
                iText.Layout.Element.Paragraph paragraph = new iText.Layout.Element.Paragraph();
                // Add a new paragraph with the text content
                paragraph.Add($"Receipt for customer {LoggedInCustomer.UserName}\n");
                paragraph.Add($"Charge amount: {ChargeAmount}\n");
                paragraph.Add($"New balance: {LoggedInCustomer.Wallet}\n");
                paragraph.Add($"Date: {DateTime.Now}\n");
                try
                {
                    document.Add(paragraph);
                } 
                catch { }
                // Close the document
                document.Close();

            }
            catch (Exception ex) { MessageBox.Show($"Failed creating the file. {ex.Message}"); }
            MessageBox.Show("Balance updated successfully!");
            CustomerPanel panel = new CustomerPanel(LoggedInCustomer);
            panel.Show();
            Close();
            window.Close();
        }
        
        private void NoButton_Click (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Balance updated successfully!");
            CustomerPanel panel = new CustomerPanel(LoggedInCustomer);
            panel.Show();
            Close();
            window.Close();
        }

    }
}
