using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
                using (FileStream stream = new FileStream("Receipt.pdf", FileMode.Create))
                {
                    PdfDocument document = new PdfDocument();

                    PdfWriter writer = PdfWriter.GetInstance(document, stream);
                    document.Open();
                    document.Add(new Header("Receipt", "reciept"));
                    document.Add(new Paragraph($"Charge amount: {ChargeAmount}"));
                    document.Add(new Paragraph($"New balance: {LoggedInCustomer.Wallet}"));
                    document.Add(new Paragraph($"{DateTime.Now}"));
                    document.Close();
                }
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
