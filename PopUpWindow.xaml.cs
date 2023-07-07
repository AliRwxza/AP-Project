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
        bool Charge;
        public PopUpWindow(Customer LoggedInCustomer, double ChargeAmount, Window window, bool Charge)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.LoggedInCustomer = LoggedInCustomer;
            this.ChargeAmount = ChargeAmount;
            this.window = window;
            this.Charge = Charge;
        }

        private void YesButton_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                string time = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-').Replace(' ', '-');
                string path = $"PaymentReciept_{LoggedInCustomer.SSN}_{time}.pdf";

                PdfWriter writer = new PdfWriter(path);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new(pdf);
                iText.Layout.Element.Paragraph paragraph = new iText.Layout.Element.Paragraph();
                paragraph.Add($"Receipt for customer {LoggedInCustomer.UserName}\n");
                paragraph.Add($"Charge amount: {ChargeAmount}\n");
                paragraph.Add($"New balance: {LoggedInCustomer.Wallet}\n");
                paragraph.Add($"Date: {DateTime.Now}\n");
                document.Add(paragraph);
                document.Close();
            }
            catch (Exception ex) { MessageBox.Show($"Failed creating the file. {ex.Message}"); }
            MessageBox.Show("Balance updated successfully!");
            if (!Charge)
            {
                CustomerPanel panel = new CustomerPanel(LoggedInCustomer);
                panel.Show();
            }
            Close();
            window.Close();
        }
        
        private void NoButton_Click (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Balance updated successfully!");
            if (!Charge)
            {
                CustomerPanel panel = new CustomerPanel(LoggedInCustomer);
                panel.Show();
            }
            Close();
            window.Close();
        }

    }
}
