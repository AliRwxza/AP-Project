﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
        Employee employee;
        public EmployeePanel (Employee employee)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            this.employee = employee;
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
            SearchOrders searchOrders = new SearchOrders (employee);
            searchOrders.Show ();
        }

        private void OrderDetailsButtonClick (object sender, RoutedEventArgs e)
        {
            OrderDetails orderDetails = new OrderDetails();
            orderDetails.Show();
        }

        private void ReceiptEmailButtonClick (object sender, RoutedEventArgs e)
        {
            GetEmailWindow getEmailWindow = new GetEmailWindow ();
            getEmailWindow.Show ();
        }

        private void Logout_Click (object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow ();
            mainWindow.Show ();

            Close ();
        }
    }
}
