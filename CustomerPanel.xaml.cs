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
    /// Interaction logic for CustomerPanel.xaml
    /// </summary>
    public partial class CustomerPanel : Window
    {
        static bool IdValidation = false;
        static bool weightValidation = false;
        static bool priceValidation = false;
        static bool chargeAmountValidation = false;

        public CustomerPanel ()
        {
            InitializeComponent();
        }

        private void OrderDetailsButton_Click (object sender, RoutedEventArgs e)
        {
            MainPanel.Visibility = Visibility.Collapsed;
            MainPanelLeftColumn.Visibility = Visibility.Collapsed;

            SecondFeatureWindow.Visibility = Visibility.Visible;
            SecondFeatureLeftColumn.Visibility = Visibility.Visible;
        }

        private void SecondFeatureBackButton_Click (object sender, RoutedEventArgs e)
        {
            SecondFeatureWindow.Visibility = Visibility.Collapsed;
            SecondFeatureLeftColumn.Visibility = Visibility.Collapsed;

            MainPanel.Visibility = Visibility.Visible;
            MainPanelLeftColumn.Visibility = Visibility.Visible;
        }

        private void WalletButton_Click (object sender, RoutedEventArgs e)
        {
            MainPanelLeftColumn.Visibility = Visibility.Collapsed;
            MainPanelLeftColumn.Visibility = Visibility.Collapsed;

            ThirdFeatureFirstPageLeftColumn.Visibility = Visibility.Visible;
            ThirdFeatureFirstPage.Visibility = Visibility.Visible;
        }

        private void ChangeUsernamePasswordButton_Click (object sender, RoutedEventArgs e)
        {

        }

        //////////////////////////

        private void ObjectOptionCheck (object sender, EventArgs e)
        {

        }

        private void DocumentOptionUnCheck (object sender, EventArgs e)
        {

        }

        private void FragileOptionCheck (object sender, EventArgs e)
        {

        }

        //////////////////////////

        private void OrdinaryOptionCheck (object sender, RoutedEventArgs e)
        {

        }

        private void ExpressOptionCheck (object sender, RoutedEventArgs e)
        {

        }

        ////////////////////////////////////////////////////           SECOND FEATURE

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
            if (double.TryParse(PaidPriceField.Text, out double price))
            {
                PaidPriceField.Style = (Style)FindResource("TextBox");
                weightValidation = true;
            }
            else
            {
                PaidPriceField.Style = (Style)FindResource("TextBoxError");
                weightValidation = false;
            }
        }

        private void WeightBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(WeightBox.Text, out double weight))
            {
                WeightBox.Style = (Style)FindResource("TextBox");
                priceValidation = true;
            }
            else
            {
                WeightBox.Style = (Style)FindResource("TextBoxError");
                priceValidation = false;
            }
        }

        private void SearchOrdersButton_Click (object sender, RoutedEventArgs e)
        {
            // search the data base
            // make a csv file for the results (sorted by order date)
            // the search range must contain every single order done by any of the employees
        }

        private void OrdersReportSecondPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            OrdersReportSecondPage.Visibility = Visibility.Collapsed;
            OrdersReportFirstPage.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////// THIRD FEATURE

        private void IncreaseBalanceButton_Click (object sender, RoutedEventArgs e)
        {
            ThirdFeatureFirstPageLeftColumn.Visibility = Visibility.Collapsed;
            ThirdFeatureFirstPage.Visibility = Visibility.Collapsed;

            ThirdFeatureSecondPageLeftColumn.Visibility = Visibility.Visible;
            ChargeWalletWindow.Visibility = Visibility.Visible;
        }

        private void ThirdFeatureFirstPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            ThirdFeatureFirstPage.Visibility = Visibility.Collapsed;
            ThirdFeatureFirstPageLeftColumn.Visibility = Visibility.Collapsed;

            MainPanel.Visibility = Visibility.Visible;
            MainPanelLeftColumn.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////// SECOND PAGE ( WALLET CHARGE )
        private void Changed_CVV2 (object sender, EventArgs e)
        {
            Regex Cvv2Pattern = new Regex(@"^\d{3,4}$");

            if (Cvv2Field.Text == string.Empty)
            {
                PlaceHolderCVV2.Visibility = Visibility.Visible;
                Cvv2Field.Style = (Style)FindResource("TextBoxError");
            }
            else
            {
                PlaceHolderCVV2.Visibility = Visibility.Hidden;

                if (Cvv2Pattern.IsMatch(Cvv2Field.Text))
                    Cvv2Field.Style = (Style)FindResource("TextBox");
                else
                    Cvv2Field.Style = (Style)FindResource("TextBoxError");
            }
        }

        // must be less than or equal to 12
        private void Changed_MM (object sender, EventArgs e)
        {
            Regex patternMM = new Regex(@"^\d{2}$");

            if (MmField.Text == string.Empty)
            {
                PlaceHolderMM.Visibility = Visibility.Visible;
                MmField.Style = (Style)FindResource("TextBoxError");
            }
            else
            {
                PlaceHolderMM.Visibility = Visibility.Hidden;

                if (patternMM.IsMatch(MmField.Text))
                    MmField.Style = (Style)FindResource("TextBox");
                else
                    MmField.Style = (Style)FindResource("TextBoxError");
            }
        }

        private void Changed_YY (object sender, EventArgs e)
        {
            Regex PatternYY = new Regex(@"^\d{2}$");

            if (YyField.Text == string.Empty)
            {
                PlaceHolderYY.Visibility = Visibility.Visible;
                YyField.Style = (Style)FindResource("TextBoxError");
            }
            else
            {
                PlaceHolderYY.Visibility = Visibility.Hidden;

                if (PatternYY.IsMatch(YyField.Text))
                    YyField.Style = (Style)FindResource("TextBox");
                else
                    YyField.Style = (Style)FindResource("TextBoxError");
            }
        }

        private void PayButton_Click (object sender, EventArgs e)
        {
            // check card number and cvv2 and mm and yy too
            if (chargeAmountValidation)
            {
                int chargeAmount = int.Parse(ChargeAmountField.Text);

                if (chargeAmount > 10000)
                {
                    // add to the wallet

                    // and ask if they want this action to get saved
                    PopUpWindow popUpWindow = new PopUpWindow();
                    popUpWindow.Show();

                    // will need date and time
                    // will be making PDF
                }
            }
            else
                MessageBox.Show("Minimum value of charge amount is 10,000");
        }

        private void WalletChargeBackButton_Click (object sender, RoutedEventArgs e)
        {
            ThirdFeatureSecondPageLeftColumn.Visibility = Visibility.Collapsed;
            ChargeWalletWindow.Visibility = Visibility.Collapsed;

            ThirdFeatureFirstPageLeftColumn.Visibility = Visibility.Visible;
            ThirdFeatureFirstPage.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////// GLOBAL

        private void OnlyNumbers_PreviewTextInput (object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int number))
                e.Handled = true;
            chargeAmountValidation = true;
        }

        private bool ContainsAlphabeticCharacters (string input)
        {
            foreach (char c in input)
                if (char.IsLetter(c))
                    return true;

            return false;
        }
    }
}
