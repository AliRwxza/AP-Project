using System;
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
using iText.Kernel.Pdf;


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
                string path = "PaymentReciept.pdf"; // must be in this format : "Payment Reciept " + ID + ".pdf"

                PdfWriter writer = new PdfWriter(path);

                // Create a new PDF document
                PdfDocument pdf = new PdfDocument(writer);

                // Create a new iText document
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                // Add a new paragraph with the text content
                document.Add(new iText.Layout.Element.Paragraph("Receipt"));
                document.Add(new iText.Layout.Element.Paragraph($"Charge amount: ChargeAmount"));
                document.Add(new iText.Layout.Element.Paragraph($"New balance: LoggedInCustomer.Wallet"));
                document.Add(new iText.Layout.Element.Paragraph($"DateTime.Now"));

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
