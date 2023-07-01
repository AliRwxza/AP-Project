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
    /// Interaction logic for CustomerPanel.xaml
    /// </summary>
    public partial class CustomerPanel : Window
    {
        static bool IdValidation = false;
        public CustomerPanel ()
        {
            InitializeComponent();
        }

        private void OrderDetailsButton_Click (object sender, RoutedEventArgs e)
        {
            
        }

        ///////////////////////////////////////////////           SECOND FEATURE

        private void SearchAnOrder_Click (object sender, RoutedEventArgs e)
        {
            MainPanel.Visibility = Visibility.Collapsed;
            MainPanelLeftColumn.Visibility = Visibility.Collapsed;

            OrdersReportLeftColumn.Visibility = Visibility.Visible;
            OrdersReportFirstPage.Visibility = Visibility.Visible;
        }

        // VALIDATE THE ID FIELD
        private void OrderIdField_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(OrderIdField.Text, out int Id))
            {
                IdValidation = true;
                OrderIdField.Style = (Style)FindResource("TextBox");
            }
            else
            {
                IdValidation = false;
                OrderIdField.Style = (Style)FindResource("TextBoxError");
            }
        }

        // IF VALID, GO TO THE SECOND PAGE
        private void SearchButton_Click (object sender, RoutedEventArgs e)
        {
            if (IdValidation)
            {
                // EXTRACT THE INFORMATION AND PUT IT IN THE SECOND PAGE
                OrdersReportFirstPage.Visibility = Visibility.Collapsed;
                OrdersReportSecondPage.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Not a valid ID.");
        }

        // IF CLICKED, GO BACK TO THE MAIN PANEL
        private void OrdersReportFirstPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            OrdersReportFirstPage.Visibility = Visibility.Collapsed;
            OrdersReportLeftColumn.Visibility = Visibility.Collapsed;

            MainPanel.Visibility = Visibility.Visible;
            MainPanelLeftColumn.Visibility = Visibility.Visible;
        }

        private void PaidPriceField_TextChanged (object sender, TextChangedEventArgs e)
        {

        }

        private void WeightBox_TextChanged (object sender, TextChangedEventArgs e)
        {

        }

        private void SearchOrdersButton_Click (object sender, RoutedEventArgs e)
        {

        }

        private void OrdersReportSecondPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            OrdersReportSecondPage.Visibility = Visibility.Collapsed;
            OrdersReportFirstPage.Visibility = Visibility.Visible;
        }
    }
}
