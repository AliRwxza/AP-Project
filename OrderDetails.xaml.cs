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
        //Order order;
        static bool IdValidation = false;
        public OrderDetails ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            //order = SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)).ToList()[0];
            //foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
            //{
            //    MainMenu.Header = order.Status.ToString();
            //}
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
                    switch (order.Status.ToString())
                    {
                        case "Submitted":
                            MainMenu.Header = "Submitted";
                            Submitted.IsChecked = true;
                            break;
                        case "ReadyToSend":
                            MainMenu.Header = "Ready to Send";
                            ReadyToSend.IsChecked = true;
                            break;
                        case "OnTheWay":
                            MainMenu.Header = "On the Way";
                            OnTheWay.IsChecked = true;
                            break;
                        case "Delivered":
                            MainMenu.Header = "Delivered";
                            Delivered.IsChecked = true;
                            break;
                    }
                    
                    MainMenu.Name = order.Status.ToString();

                }
                foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
                {
                    FirstPage.Visibility = Visibility.Collapsed;
                    SecondPage.Visibility = Visibility.Visible;
                    BackButton.Visibility = Visibility.Visible;
                    SenderAddressField.Text = order.SenderAddress;
                    ReceiverAddressField.Text = order.RecieverAddress;
                    PackageTypeField.Text = order.Content.ToString();
                    ContainsValuableField.Text = (bool)order.HasExpensiveContent ? "Contains valueable content" : "Doesn't contain valueable content";
                    WeightField.Text = $"Weight: {order.Weight}";
                    PostTypeField.Text = $"Post Type: {order.postType}";
                    PhoneNumberField.Text = order.Phone != "" ? $"Phone: {order.Phone}" : "No phone number has been set";
                    return;
                }   
                MessageBox.Show("This Order ID does not exist.");
            }
            else
            {
                MessageBox.Show("Invalid Order ID!");
            }
        }

        private void SubmittedOption_Checked (object sender, RoutedEventArgs e)
        {
            if (Submitted.IsChecked == true)
            {
                MainMenu.Header = "Submitted";
                MainMenu.Name = "Submitted";
                ReadyToSend.IsChecked = false;
                OnTheWay.IsChecked = false;
                foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
                {
                    order.Status = PackageStatus.Submitted;
                    SQL.UpdateTable(order);
                }
            }
        }

        private void ReadyToSendOption_Checked (object sender, RoutedEventArgs e)
        {
            if (ReadyToSend.IsChecked == true)
            {
                MainMenu.Header = "Ready to Send";
                MainMenu.Name = "ReadyToSend";
                Submitted.IsChecked = false;
                OnTheWay.IsChecked = false;
                foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
                {
                    order.Status = PackageStatus.ReadyToSend;
                    SQL.UpdateTable(order);
                }
            }
        }

        private void OnTheWayOption_Checked (object sender, RoutedEventArgs e)
        {
            if (OnTheWay.IsChecked == true)
            {
                MainMenu.Header = "On the Way";
                MainMenu.Name = "OnTheWay";
                Submitted.IsChecked = false;
                ReadyToSend.IsChecked = false;
                foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
                {
                    order.Status = PackageStatus.OnTheWay;
                    SQL.UpdateTable(order);
                }
            }
        }

        private void DeliveredOption_Checked (object sender, RoutedEventArgs e)
        {
            if (Delivered.IsChecked == true)
            {
                MainMenu.Header = "Delivered";
                MainMenu.Name = "Delivered";

                Submitted.IsChecked = false;
                ReadyToSend.IsChecked = false;
                OnTheWay.IsChecked = false;

                MainMenu.IsCheckable = false;
                Submitted.IsCheckable = false;
                ReadyToSend.IsCheckable = false;
                OnTheWay.IsCheckable = false;
                Delivered.IsCheckable = false;

                Submitted.IsEnabled = false;
                ReadyToSend.IsEnabled = false;
                OnTheWay.IsEnabled = false;
                Delivered.IsEnabled = false;

                foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
                {
                    order.Status = PackageStatus.Delivered;
                    SQL.UpdateTable(order);
                }
            }
        }

        private void BackButton_Click (object sender, RoutedEventArgs e)
        {
            FirstPage.Visibility = Visibility.Visible;
            SecondPage.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            //foreach (var order in SQL.ReadOrdersData().Where(x => x.OrderID == int.Parse(OrderIdField.Text)))
            //{
            //    order.Status = Enum.Parse<PackageStatus>(MainMenu.Name);
            //    SQL.UpdateTable(order);
            //}
                
        }
    }
}
