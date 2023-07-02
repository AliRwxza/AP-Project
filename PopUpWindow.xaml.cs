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
            // if (pdf created successfully)
            MessageBox.Show("Saved successfully.");
            // else
            //MessageBox.Show("Failed creating the file.");
        }

        private void NoButton_Click (object sender, RoutedEventArgs e) => Close();
    }
}
