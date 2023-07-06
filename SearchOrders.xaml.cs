using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Reflection.Metadata;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for SearchOrders.xaml
    /// </summary>
    public partial class SearchOrders : Window
    {
        static bool[] bools = { false, false, false };
        Employee employee;
        public SearchOrders (Employee employee)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            this.employee = employee;
        }

        private void SearchOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            List<Order> orders = SQL.ReadOrdersData();
            if (bools[0])
            {
                orders = orders.Where(x => x.CustomerSSN == SsnField.Text).ToList();
            }
            if (bools[1])
            {
                orders = orders.OrderBy(x => Math.Abs(x.Calculate() - double.Parse(PaidPriceField.Text))).ToList();
            }
            if (bools[2])
            {
                orders = orders.OrderBy(x => Math.Abs(x.Weight - double.Parse(WeightBox.Text))).ToList();
            }
            if (Object.IsChecked || Document.IsChecked || Fragile.IsChecked)
            {
                if (!Object.IsChecked)
                {
                    orders = orders.Where(x => x.Content != PackageContent.Object).ToList();
                }
                if (!Document.IsChecked)
                {
                    orders = orders.Where(x => x.Content != PackageContent.Document).ToList();
                }
                if (!Fragile.IsChecked)
                {
                    orders = orders.Where(x => x.Content != PackageContent.Fragile).ToList();
                }
            }
            if (Ordinary.IsChecked || Express.IsChecked)
            {
                if (!Ordinary.IsChecked)
                {
                    orders = orders.Where(x => x.postType != PostType.Ordinary).ToList();
                }
                if (!Express.IsChecked)
                {
                    orders = orders.Where(x => x.postType != PostType.Express).ToList();
                }
            }

            try
            {
                using (StreamWriter writer = new StreamWriter($"SearchResult_employee_{employee.EmployeeID}.csv", false))
                {
                    writer.WriteLine("Order ID,Sender Address,Reciever Address,Content,Has Expensive Content,Weight,Post Type,Phone,Status,CustomerSSN,Date,Price,Comment");
                    foreach (var order in orders)
                    {
                        writer.WriteLine($"{order.OrderID},{order.SenderAddress.Replace(',', '/')},{order.RecieverAddress.Replace(',', '/')},{order.Content},{order.HasExpensiveContent},{order.Weight},{order.postType},'{order.Phone,11},{order.Status, 13},'{order.CustomerSSN, 10},{order.Date.ToString().Substring(0, 8)},{order.Calculate()},{order.Comment.Replace(',', ' ')}");
                    }
                    writer.Close();
                }

                MessageBox.Show("Search completed! Now you can see the search result in the .csv file in the app directory.");
            }
            catch
            {
                MessageBox.Show("Please close the file and search again!");
            }
        }

        /////////////////////////////////////////////////////////

        private void ObjectOptionCheck (object sender, EventArgs e)
        {

        }

        private void DocumentOptionCheck (object sender, EventArgs e)
        {

        }

        private void FragileOptionCheck (object sender, EventArgs e)
        {

        }

        /////////////////////////////////////////////////////////

        private void OrdinaryOptionCheck (object sender, RoutedEventArgs e)
        {

        }

        private void ExpressOptionCheck (object sender, RoutedEventArgs e)
        {

        }

        private void SsnField_TextChanged (object sender, TextChangedEventArgs e)
        {
            Regex SsnValidation = new Regex(@"^00\d{8}$");

            if (!SsnValidation.IsMatch(SsnField.Text))
            {
                SsnField.Style = (Style)FindResource("TextBoxError");
                bools[0] = false;
            }
            else if (SsnValidation.IsMatch(SsnField.Text))
            {
                // hmmm
                SsnField.Style = (Style)FindResource("TextBox");
                bools[0] = true;
            }
            else
            {
                SsnField.Style = (Style)FindResource("TextBox");
            }
        }

        private void PaidPriceField_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(PaidPriceField.Text, out double price))
            {
                PaidPriceField.Style = (Style)FindResource("TextBox");
                bools[1] = true;
            }
            else if (!double.TryParse(PaidPriceField.Text, out double p))
            {
                PaidPriceField.Style = (Style)FindResource("TextBoxError"); 
                bools[1] = false;
            }
            else
            {
                PaidPriceField.Style = (Style)FindResource("TextBox");
            }
        }

        private void WeightBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(WeightBox.Text, out double temp))
            {
                WeightBox.Style = (Style)FindResource("TextBox");
                bools[2] = true;
            }
            else if (double.TryParse(WeightBox.Text, out double t)) 
            { 
                WeightBox.Style = (Style)FindResource("TextBoxError"); 
                bools[2] = false;
            }
            else
            {
                PaidPriceField.Style = (Style)FindResource("TextBox");
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
