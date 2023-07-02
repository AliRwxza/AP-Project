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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        static bool IdValidation = false;
        public OrderDetails ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void OrderIdField_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(OrderIdField.Text, out int Id))
            {
                OrderIdField.Style = (Style)FindResource("TextBox");
                IdValidation = true;
            }
            else
            {
                OrderIdField.Style = (Style)FindResource("TextBoxError");
                IdValidation = false;
            }
        }

        private void SearchButton_Click (object sender, RoutedEventArgs e)
        {
            if (IdValidation)
            {
                foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
                {

                }
                FirstPage.Visibility = Visibility.Collapsed;
                SecondPage.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;

                //else :
                // MessageBox.Show("This Order ID does not exist.");
            }
        }

        private void SubmittedOption_Checked (object sender, RoutedEventArgs e)
        {
            if (SubmittedOption.IsChecked == true)
            {
                MainMenu.Header = "Submitted";
                ReadyToSendOption.IsChecked = false;
                OnTheWayOption.IsChecked = false;
            }
        }

        private void ReadyToSendOption_Checked (object sender, RoutedEventArgs e)
        {
            if (ReadyToSendOption.IsChecked == true)
            {
                MainMenu.Header = "Ready to Send";
                SubmittedOption.IsChecked = false;
                OnTheWayOption.IsChecked = false;
            }
        }

        private void OnTheWayOption_Checked (object sender, RoutedEventArgs e)
        {
            if (OnTheWayOption.IsChecked == true)
            {
                MainMenu.Header = "On the Way";
                SubmittedOption.IsChecked = false;
                ReadyToSendOption.IsChecked = false;
            }
        }

        private void DeliveredOption_Checked (object sender, RoutedEventArgs e)
        {
            if (DeliveredOption.IsChecked == true)
            {
                MainMenu.Header = "Delivered";

                SubmittedOption.IsChecked = false;
                ReadyToSendOption.IsChecked = false;
                OnTheWayOption.IsChecked = false;

                MainMenu.IsCheckable = false;
                SubmittedOption.IsCheckable = false;
                ReadyToSendOption.IsCheckable = false;
                OnTheWayOption.IsCheckable = false;
                DeliveredOption.IsCheckable = false;

                SubmittedOption.IsEnabled = false;
                ReadyToSendOption.IsEnabled = false;
                OnTheWayOption.IsEnabled = false;
                DeliveredOption.IsEnabled = false;
            }
        }

        private void BackButton_Click (object sender, RoutedEventArgs e)
        {
            FirstPage.Visibility = Visibility.Visible;
            SecondPage.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
        }
    }
}
