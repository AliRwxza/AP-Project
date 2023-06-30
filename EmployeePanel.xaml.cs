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
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Window
    {
        public EmployeePanel ()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void CustomerSignUpButtonClick (object sender, RoutedEventArgs e)
        {
            CustomerSignUpWindow customerSignUpWindow = new CustomerSignUpWindow ();
            customerSignUpWindow.Show ();
        }

        private void EmployeePanelOrderButton (object sender, RoutedEventArgs e)
        {
            TakeCustomerIdNumber takeCustomerIdNumber = new TakeCustomerIdNumber ();
            takeCustomerIdNumber.Show();
        }

        private void SearchOrdersButtonClick (object sender, RoutedEventArgs e)
        {
            SearchOrders searchOrders = new SearchOrders ();
            searchOrders.Show ();
        }
    }
}
