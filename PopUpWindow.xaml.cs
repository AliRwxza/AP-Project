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
    /// Interaction logic for PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        public PopUpWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
                document.Add(new iText.Layout.Element.Paragraph("This is the text content of the PDF file."));
                document.Add(new iText.Layout.Element.Paragraph($"{DateTime.Now}"));

                // Close the document
                document.Close();

                MessageBox.Show("Saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed creating your reciept. Error message : " + ex.Message);
            }
        }

        private void NoButton_Click (object sender, RoutedEventArgs e) => Close();
    }
}
