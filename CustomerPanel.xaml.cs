using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using System.Windows.Xps.Packaging;
using System.Xml.Linq;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for CustomerPanel.xaml
    /// </summary>
    public partial class CustomerPanel : Window
    {
        bool weightValidation = false;
        bool priceValidation = false;
        bool YYValidation = false;
        bool MMValidation = false;
        bool[] Card = {false, false, false, false};
        Order order = null;
        Customer LoggedInCustomer;
        bool Charge = false;
        public CustomerPanel (Customer customer, bool charge = false)
        {
            InitializeComponent();

            LoggedInCustomer = customer;
            Charge = charge;
            if (charge)
            {
                ThirdFeatureFirstPageLeftColumn.Visibility = Visibility.Collapsed;
                ThirdFeatureFirstPage.Visibility = Visibility.Collapsed;
                ThirdFeatureSecondPageLeftColumn.Visibility = Visibility.Visible;
                ChargeWalletWindow.Visibility = Visibility.Visible;
                ChargeAmountField.Focus();
                Logout.IsEnabled = false;
                Logout.FontSize = 1;
                WalletChargeBackButton.Visibility = Visibility.Collapsed;
            }
        }

        private void OrderDetailsButton_Click (object sender, RoutedEventArgs e)
        {
            CustomerSSN.Content = $"Customer SSN: {LoggedInCustomer.SSN}";

            MainPanel.Visibility = Visibility.Collapsed;
            MainPanelLeftColumn.Visibility = Visibility.Collapsed;

            SecondFeatureWindow.Visibility = Visibility.Visible;
            SecondFeatureLeftColumn.Visibility = Visibility.Visible;
        }

        private void SecondFeatureBackButton_Click (object sender, RoutedEventArgs e)
        {
            PaidPriceField.Text = string.Empty;
            WeightBox.Text = string.Empty;
            Fragile.IsChecked = false;
            Document.IsChecked = false;
            Object.IsChecked = false;
            Ordinary.IsChecked = false;
            Express.IsChecked = false;

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
            Balance.Text = LoggedInCustomer.Wallet.ToString();
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
            OrderIdField.Focus();
        }

        // VALIDATE THE ID FIELD
        private void OrderIdField_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(OrderIdField.Text, out int Id) || OrderIdField.Text == string.Empty)
            {
                OrderIdField.Style = (System.Windows.Style)FindResource("TextBox");
            }
            else
            {
                OrderIdField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
        }

        // IF VALID, GO TO THE SECOND PAGE
        private void SearchButton_Click (object sender, RoutedEventArgs e)
        {
            List<Order> orders = SQL.ReadOrdersData().Where(x => x.CustomerSSN == LoggedInCustomer.SSN).ToList();
            if (int.TryParse(OrderIdField.Text, out int Id))
            {
                foreach (var order in orders)
                {
                    if (order.OrderID == Id)
                    {
                        this.order = order;
                        OrdersReportFirstPage.Visibility = Visibility.Collapsed;
                        OrdersReportSecondPage.Visibility = Visibility.Visible;
                        SenderAddressField.Text = order.SenderAddress;
                        ReceiverAddressField.Text = order.RecieverAddress;
                        PackageTypeField.Text = order.Content.ToString();
                        ContainsValuableField.Text = (bool)order.HasExpensiveContent ? "Contains valueable content" : "Does not have valueable content";
                        WeightField.Text = $"Weight: {order.Weight}";
                        PostTypeField.Text = $"Post type: {order.postType}";
                        PhoneNumberField.Text = order.Phone != string.Empty ? order.Phone : "No phone number has been submitted";
                        if (order.Status == PackageStatus.Delivered) 
                        {
                            SubmitOpinionHyperlink.Visibility = Visibility.Visible;
                        }
                        else 
                        {
                            SubmitOpinionHyperlink.Visibility = Visibility.Collapsed;
                        }
                        return;
                    }
                }
                MessageBox.Show("Order ID not found!");
            }
            else
                MessageBox.Show("Not a valid ID.");
        }

        // IF CLICKED, GO BACK TO THE MAIN PANEL
        private void OrdersReportFirstPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            OrderIdField.Text = string.Empty;
            order = null;
            OrderIdField.Style = (System.Windows.Style)FindResource("TextBox");
            OrdersReportFirstPage.Visibility = Visibility.Collapsed;
            OrdersReportLeftColumn.Visibility = Visibility.Collapsed;

            MainPanel.Visibility = Visibility.Visible;
            MainPanelLeftColumn.Visibility = Visibility.Visible;
        }

        private void PaidPriceField_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(PaidPriceField.Text, out double price))
            {
                PaidPriceField.Style = (System.Windows.Style)FindResource("TextBox");
                priceValidation = true;
            }
            else
            {
                PaidPriceField.Style = (System.Windows.Style)FindResource("TextBoxError");
                priceValidation = false;
            }
        }
        private void WeightBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(WeightBox.Text, out double weight))
            {
                WeightBox.Style = (System.Windows.Style)FindResource("TextBox");
                weightValidation = true;
            }
            else
            {
                WeightBox.Style = (System.Windows.Style)FindResource("TextBoxError");
                weightValidation = false;
            }
        }
        private void SearchOrdersButton_Click (object sender, RoutedEventArgs e)
        {
            List<Order> orders = SQL.ReadOrdersData().Where(x => x.CustomerSSN == LoggedInCustomer.SSN).ToList();

            if (priceValidation)
            {
                orders = orders.OrderBy(x => Math.Abs(x.Calculate() - double.Parse(PaidPriceField.Text))).ToList();
            }
            if (weightValidation)
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
                using (StreamWriter writer = new StreamWriter($"SearchResult_customer_{LoggedInCustomer.SSN}.csv", false))
                {
                    writer.WriteLine("Order ID,Sender Address,Reciever Address,Content,Has Expensive Content,Weight,Post Type,Phone,Status,CustomerSSN,Date,Price,Comment");
                    foreach (var order in orders)
                    {
                        writer.WriteLine($"{order.OrderID},{order.SenderAddress.Replace(',', '/')},{order.RecieverAddress.Replace(',', '/')},{order.Content},{order.HasExpensiveContent},{order.Weight},{order.postType},'{order.Phone,11},{order.Status,13},'{order.CustomerSSN,10},{order.Date.ToString().Substring(0, 8)},{order.Calculate()},{order.Comment.Replace(',', ' ')}");
                    }
                    writer.Close();
                }
                MessageBox.Show("Search completed! Now you can see the search result in the .csv file in the app directory.");
            }
            catch { MessageBox.Show("Please close the file and search again!"); }
        }

        private void SubmitOpinion_Click (object sender, RoutedEventArgs e)
        {
            if (order != null)
            {
                CustomerOpinionField.Text = order.Comment;
            }
            OrdersReportSecondPage.Visibility = Visibility.Collapsed;
            GetCustomerOpinionPage.Visibility = Visibility.Visible;
        }

        private void SubmitOpinionButton_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerOpinionField.Text.Length == 0)
                {
                    MessageBox.Show("This field can't be empty!");
                }
                else if (CustomerOpinionField.Text.Length < 10)
                {
                    MessageBox.Show("This field must contain atleast 10 characters!");
                }
                else if (CustomerOpinionField.Text.Length >= 500)
                {
                    MessageBox.Show("Maximum limit of comment is 500 characters!");
                }
                else
                {
                    if (order != null)
                    {
                        order.Comment = CustomerOpinionField.Text;
                        SQL.UpdateTable<Order>(order);
                    }
                    MessageBox.Show("Saved successfully.");
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
            ChargeAmountField.Focus();
            First.Text = string.Empty;
            Second.Text = string.Empty;
            Third.Text = string.Empty;
            Forth.Text = string.Empty;
            Cvv2Field.Text = string.Empty;
            MmField.Text = string.Empty;
            YyField.Text = string.Empty;
            ChargeAmountField.Text = string.Empty;
        }

        private void ThirdFeatureFirstPageBackButton_Click (object sender, RoutedEventArgs e)
        {
            ThirdFeatureFirstPage.Visibility = Visibility.Collapsed;
            ThirdFeatureFirstPageLeftColumn.Visibility = Visibility.Collapsed;

            MainPanel.Visibility = Visibility.Visible;
            MainPanelLeftColumn.Visibility = Visibility.Visible;
        }

        //////////////////////// SECOND PAGE ( WALLET CHARGE )
        
        private void Changed_First(object sender, EventArgs e)
        {
            if (First.Text.Length == 4)
            {
                Second.Focus();
            }
        }
        private void Changed_Second(object sender, EventArgs e)
        {
            if (Second.Text.Length == 4)
            {
                Third.Focus();
            }
        }
        private void Changed_Third(object sender, EventArgs e)
        {
            if (Third.Text.Length == 4)
            {
                Forth.Focus();
            }
        }
        private void Changed_Forth(object sender, EventArgs e)
        {
            if (Forth.Text.Length == 4)
            {
                Cvv2Field.Focus();
            }
        }
        private void Changed_CVV2 (object sender, EventArgs e)
        {
            Regex Cvv2Pattern = new Regex(@"^\d{3,4}$");

            if (Cvv2Field.Text == string.Empty)
            {
                PlaceHolderCVV2.Visibility = Visibility.Visible;
                Cvv2Field.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            else
            {
                PlaceHolderCVV2.Visibility = Visibility.Hidden;
                if (Cvv2Field.Text.Length == 4)
                {
                    if (MmField.Text == string.Empty)
                    {
                        MmField.Focus();
                    }
                    else if (YyField.Text == string.Empty)
                    {
                        YyField.Focus();
                    }
                }
                if (Cvv2Pattern.IsMatch(Cvv2Field.Text))
                {
                    Cvv2Field.Style = (System.Windows.Style)FindResource("TextBox");
                }
                else
                    Cvv2Field.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            Cvv2Field.Style = (System.Windows.Style)FindResource("TextBox");
        }
        private void Changed_MM (object sender, EventArgs e)
        {
            Regex patternMM = new Regex(@"^\d{2}$");

            if (MmField.Text == string.Empty)
            {
                MMValidation = false;
                PlaceHolderMM.Visibility = Visibility.Visible;
                MmField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            else
            {
                PlaceHolderMM.Visibility = Visibility.Hidden;
                if (patternMM.IsMatch(MmField.Text) && int.TryParse(MmField.Text, out int MM))
                {
                    if (MM > 12 || MM < 0)
                    {
                        MmField.Style = (System.Windows.Style)FindResource("TextBoxError");
                        MMValidation = false;
                        return;
                    }
                    MmField.Style = (System.Windows.Style)FindResource("TextBox");
                    MMValidation = true;
                    if (YyField.Text == string.Empty)
                    {
                        YyField.Focus();
                    }
                }
                else
                {
                    MmField.Style = (System.Windows.Style)FindResource("TextBoxError");
                    MMValidation = false;
                }
            }
        }

        private void Changed_YY (object sender, EventArgs e)
        {
            Regex PatternYY = new Regex(@"^\d{2}$");

            if (YyField.Text == string.Empty)
            {
                PlaceHolderYY.Visibility = Visibility.Visible;
                YyField.Style = (System.Windows.Style)FindResource("TextBoxError");
                YYValidation = false;
            }
            else
            {
                PlaceHolderYY.Visibility = Visibility.Hidden;

                if (PatternYY.IsMatch(YyField.Text) && int.TryParse(YyField.Text, out int YY))
                {
                    if (YY > 99 || YY < 0)
                    {
                        YYValidation = false;
                        YyField.Style = (System.Windows.Style)FindResource("TextBoxError");
                        return;
                    }
                    YyField.Style = (System.Windows.Style)FindResource("TextBox");
                    YYValidation = true;
                }
                else
                {
                    YYValidation = false;
                    YyField.Style = (System.Windows.Style)FindResource("TextBoxError");
                }
            }
        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            string CardNumber = First.Text + Second.Text + Third.Text + Forth.Text;
            if (CardNumber.Length == 16)
            {
                if (Validation.LUHN(CardNumber))
                {
                    if (Cvv2Field.Text != string.Empty)
                    {
                        if (Validation.CVV2(Cvv2Field.Text))
                        {
                            if (YyField.Text != string.Empty && MmField.Text != string.Empty)
                            {
                                if (MMValidation && YYValidation)
                                {
                                    if (double.TryParse(ChargeAmountField.Text, out double chargeAmount))
                                    {
                                        if (chargeAmount >= 10000)
                                        {
                                            LoggedInCustomer.Wallet += chargeAmount;
                                            SQL.UpdateTable<Customer>(LoggedInCustomer);
                                            PopUpWindow popUpWindow = new PopUpWindow(LoggedInCustomer, chargeAmount, this, Charge);
                                            popUpWindow.Show();
                                        }
                                        else { MessageBox.Show("Minimum value of charge amount is 10,000!"); }
                                    }
                                    else { MessageBox.Show("Invalid charge amount!"); }
                                }
                                else { MessageBox.Show("Invalid date!"); }
                            }
                            else { MessageBox.Show("Fill the date fields!"); }
                        }
                        else { MessageBox.Show("Invalid CVV2!"); }
                    }
                    else { MessageBox.Show("Fill the CVV2 field!"); }
                }
                else { MessageBox.Show("Invalid card number!"); }
            }
            else { MessageBox.Show("Fill the card number field."); }
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
            try
            {
                if (NewUsernameField.Text.Length == 0)
                {
                    MessageBox.Show("New username field can't be empty.");
                    NewUsernameField.Style = (System.Windows.Style)FindResource("TextBoxError");
                }
                else if (!Validation.UserName(NewUsernameField.Text))
                {
                    if (SQL.UserExist(NewUsernameField.Text))
                    {
                        MessageBox.Show("This username already exists!");
                    }
                    else
                    {
                        MessageBox.Show("Username must be between 3 and 32 letters and alphanumeric only.");
                    }
                    NewUsernameField.Style = (System.Windows.Style)FindResource("TextBoxError");
                }
                else if (NewUsernameField.Text == LoggedInCustomer.UserName)
                {
                    MessageBox.Show("Username hasn't changed!");
                }
                else if (UsernameChangePasswordField.Text != LoggedInCustomer.Password)
                {
                    MessageBox.Show("Wrong password.");
                    UsernameChangePasswordField.Style = (System.Windows.Style)FindResource("TextBoxError");
                }
                else
                {
                    if (!Validation.UniqueUsername(NewUsernameField.Text))
                    {
                        MessageBox.Show("Username taken!");
                        return;
                    }
                    
                    LoggedInCustomer.UserName = NewUsernameField.Text;
                    SQL.UpdateTable<Customer>(LoggedInCustomer);
                    MessageBox.Show("Username Changed Successfully.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }   
        }

        private void ChangePasswordButton_Click (object sender, RoutedEventArgs e)
        {
            if (NewPasswordField.Text.Length == 0)
            {
                MessageBox.Show("Empty password field.");
                NewPasswordField.Style = (System.Windows.Style)FindResource("TextBoxError");

            }
            else if (NewPasswordAgainField.Text.Length == 0)
            {
                MessageBox.Show("You need to enter the password again.");
                NewPasswordAgainField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            else if (NewPasswordField.Text != NewPasswordAgainField.Text)
            {
                MessageBox.Show("Entered passwords do not match");
                NewPasswordAgainField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            else if (!Validation.Password(NewPasswordField.Text))
            {
                MessageBox.Show("Password must have atleat 1 capital letter, 1 small letter, a number, and have atleast 8 letters overall.");
                NewPasswordField.Style = (System.Windows.Style)FindResource("TextBoxError");
                NewPasswordAgainField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            // check if the password is wrong
            else if (LoggedInCustomer.Password != PasswordChangePasswordField.Text)
            {
                MessageBox.Show("Wrong password. Try again.");
                PasswordChangePasswordField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            else
            {
                try
                {
                    NewPasswordField.Style = (Style)FindResource("TextBox");
                    NewPasswordAgainField.Style = (Style)FindResource("TextBox");

                    LoggedInCustomer.Password = NewPasswordField.Text;
                    SQL.UpdateTable<Customer>(LoggedInCustomer);
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

        }

        private bool ContainsAlphabeticCharacters (string input)
        {
            foreach (char c in input)
                if (char.IsLetter(c))
                    return true;

            return false;
        }

        //private void PayButton_Click (object sender, RoutedEventArgs e)
        //{
        //    // check card number
        //    // add the value 
        //    PopUpWindow popUpWindow = new PopUpWindow(LoggedInCustomer, chargeAmount, this);
        //    popUpWindow.Show();
        //}

        private void MmField_PreviewTextInput (object sender, TextCompositionEventArgs e)
        {

        }
        private void NewPasswordField_Changed(object sender, EventArgs e)
        {
            if (!Validation.Password(NewPasswordField.Text))
            {
                NewPasswordField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            else
            {
                NewPasswordField.Style = (System.Windows.Style)FindResource("TextBox");
            }
        }
        private void NewPasswordAgainField_Changed(object sender, EventArgs e)
        {
            if (!Validation.Password(NewPasswordField.Text))
            {
                NewPasswordAgainField.Style = (System.Windows.Style)FindResource("TextBoxError");
            }
            else
            {
                NewPasswordAgainField.Style = (System.Windows.Style)FindResource("TextBox");
            }
        }
    }
}
