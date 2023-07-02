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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for SearchOrders.xaml
    /// </summary>
    public partial class SearchOrders : Window
    {
        static bool[] bools = { false, false, false };

        public SearchOrders ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
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
            if (MainMenu.Header.ToString() != "Package Type")
            {
                orders = orders.Where(x => x.Content == Enum.Parse<PackageContent>(MainMenu.Header.ToString())).ToList();
            }
            if (MainMenu2.Header.ToString() != "Delivery Type")
            {
                orders = orders.Where(x => x.postType == Enum.Parse<PostType>(MainMenu2.Header.ToString())).ToList();
            }
            using (StreamWriter writer = new StreamWriter("SearchResult.csv", false))
            {
                writer.WriteLine("Order ID,Sender Address,Reciever Address,Content,Has Expensive Content,Weight,Post Type,Phone,Status,CustomerSSN,Date,Price,Comment");
                foreach (var order in orders)
                {
                    writer.WriteLine($"{order.OrderID},{order.SenderAddress},{order.RecieverAddress},{order.Content},{order.HasExpensiveContent},{order.Weight},{order.postType},{order.Phone},{order.Status},{order.CustomerSSN},{order.Date},{order.Calculate()},{order.Comment}");
                }
                writer.Close();
            }
            MessageBox.Show("Search completed! Now you can see the search result in the .csv file in the app directory.");

            // search the data base
            // make a csv file for the results (sorted by order date)
            // the search range must contain every single order done by any of the employees
        }

        /////////////////////////////////////////////////////////

        private void ObjectOptionCheck (object sender, EventArgs e)
        {
            
        }

        private void DocumentOptiUnCheck (object sender, EventArgs e)
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
        }

        private void PaidPriceField_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(PaidPriceField.Text, out double price))
            {
                PaidPriceField.Style = (Style)FindResource("TextBox");
                bools[1] = true;
            }
            else { PaidPriceField.Style = (Style)FindResource("TextBoxError"); bools[1] = false; }
        }

        private void WeightBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(WeightBox.Text, out double temp))
            {
                WeightBox.Style = (Style)FindResource("TextBox");
                bools[2] = true;
            }
            else { WeightBox.Style = (Style)FindResource("TextBoxError"); bools[2] = false; }
        }
    }
}
