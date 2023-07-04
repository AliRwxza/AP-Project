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
            MainPanel.Visibility = Visibility.Collapsed;
            MainPanelLeftColumn.Visibility = Visibility.Collapsed;

            FourthFeatureLeftColumn.Visibility = Visibility.Visible;
            FourthFeatureWindow.Visibility = Visibility.Visible;
        }

        private void Logout_Click (object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
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
                if (true) // the package was delivered
                {
                    SubmitOpinionHyperlink.Visibility = Visibility.Visible;
                }
                else // the package was not delivered
                {
                    SubmitOpinionHyperlink.Visibility = Visibility.Collapsed;
                }
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

        private void SubmitOpinion_Click (object sender, RoutedEventArgs e)
        {
            OrdersReportSecondPage.Visibility = Visibility.Collapsed;
            GetCustomerOpinionPage.Visibility = Visibility.Visible;
        }

        private void SubmitOpinionButton_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerOpinionField.Text.Length == 0)
                {
                    MessageBox.Show("This field can't be empty.");
                }
                else if (CustomerOpinionField.Text.Length >= 10)
                {
                    // save the opinion
                    MessageBox.Show("Saved successfully.");
                }
                else
                {
                    MessageBox.Show("This field must contain atleast 10 characters.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed saving your opinion. Error message : " + ex.Message);
            }
        }

        // IF CLICKED, GO BACK TO ORDERS REPORT FIRST PAGE
        private void OrdersReportSecondPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            OrdersReportSecondPage.Visibility = Visibility.Collapsed;
            OrdersReportFirstPage.Visibility = Visibility.Visible;
        }

        // IF CLICKED, GO BACK TO ORDERS REPORT SECOND PAGE
        private void OrdersThirdPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            GetCustomerOpinionPage.Visibility = Visibility.Collapsed;
            OrdersReportSecondPage.Visibility = Visibility.Visible;
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

        //////////////////////// SECOND PAGE ( WALLET CHARGE )
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

        //////////////////////////////////////////////////// FOURTH FEATURE

        private void ChangeUsernameButton_Click (object sender, RoutedEventArgs e)
        {
            if (NewUsernameField.Text.Length == 0)
            {
                MessageBox.Show("New username field can't be empty.");
                NewUsernameField.Style = (Style)FindResource("TextBoxError");
            }
            else if (Validation.UserName(NewUsernameField.Text))
            {
                MessageBox.Show("Username must be between 3 and 32 letters and alphanumeric only.");
                NewUsernameField.Style = (Style)FindResource("TextBoxError");
            }
            // check the password
            else if (false)
            {
                MessageBox.Show("Wrong password.");
                UsernameChangePasswordField.Style = (Style)FindResource("TextBoxError");
            }
        }

        private void ChangePasswordButton_Click (object sender, RoutedEventArgs e)
        {
            if (NewPasswordField.Text.Length == 0)
            {
                MessageBox.Show("Empty password field.");
                NewPasswordField.Style = (Style)FindResource("TextBoxError");

            }
            else if (NewPasswordAgainField.Text.Length == 0)
            {
                MessageBox.Show("You need to enter the password again.");
                NewPasswordAgainField.Style = (Style)FindResource("TextBoxError");
            }
            else if (NewPasswordField.Text != NewPasswordAgainField.Text)
            {
                MessageBox.Show("Entered passwords do not match");
                NewPasswordAgainField.Style = (Style)FindResource("TextBoxError");
            }
            else if (!Validation.Password(NewPasswordField.Text))
            {
                MessageBox.Show("Password must have atleat 1 capital letter, 1 small letter, a number, and have atleast 8 letters overall.");
                NewPasswordField.Style = (Style)FindResource("TextBoxError");
                NewPasswordAgainField.Style = (Style)FindResource("TextBoxError");
            }
            // check if the password is wrong
            else if (false)
            {
                MessageBox.Show("Wrong password. Try again.");
                PasswordChangePasswordField.Style = (Style)FindResource("TextBoxError");
            }
            else
            {
                try
                {
                    // change the password
                    MessageBox.Show("Password Changed Successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed changing your password. Error message :" + ex.Message);
                }
            }
        }

        private void FourthFeatureBackButton_Click (object sender, RoutedEventArgs e)
        {
            FourthFeatureLeftColumn.Visibility = Visibility.Collapsed;
            FourthFeatureWindow.Visibility = Visibility.Collapsed;

            MainPanelLeftColumn.Visibility = Visibility.Visible;
            MainPanel.Visibility = Visibility.Visible;
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
