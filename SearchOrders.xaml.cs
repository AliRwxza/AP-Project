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

        private void SearchOrdersButton_Click (object sender, RoutedEventArgs e)
        {
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

        private void ValuableCheckBoxCheck (object sender, RoutedEventArgs e)
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
