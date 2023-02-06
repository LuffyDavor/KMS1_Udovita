using KMS1_Udovita.Filters;
using KMS1_Udovita.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Window = System.Windows.Window;

namespace KMS1_Udovita
{
    /// <summary>
    /// Interaction logic for AccountsWindow.xaml
    /// </summary>
    public partial class AccountsWindow : Window, IWindowMethods
    {
        private static MainWindow _mainWindow;
        public string AccOwner { get; private set; }
        public AccountsWindow(MainWindow mainWindow, AccountsFilter filter,CustomerModel selectedCustomer)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            accountsDataGrid.ItemsSource = filter.FilteredList;
            AccOwner = selectedCustomer.Name;
            txtCustomerName.DataContext = selectedCustomer;
            txtCustomerID.DataContext = selectedCustomer;

            // HANDLE DETAIL BTN AND WINDOW BEING CLOSED
            HandleDetailsBtn();
            Closing += AccountsWindow_Closing;
        }

        private void AccountsWindow_Closing(object sender, CancelEventArgs e)
        {
            _mainWindow.Show();
        }

        public void HandleDetailsBtn()
        {
            // HANDLING BTN "DETAILS" ENABLE/DISABLE VISIBLE/INVISIBLE
            accountsDataGrid.SelectionChanged += (s, e) =>
            {
                if (accountsDataGrid.SelectedItem == null)
                {
                    btnDetails.IsEnabled = false;
                    btnDetails.Visibility = Visibility.Hidden;

                }
                else
                {
                    btnDetails.Visibility = Visibility.Visible;
                    btnDetails.IsEnabled = true;
                }
            };
        }

        public void ChangeWindow()
        {
            // GET SELECTED ACCOUNT
            AccountModel selectedAccount = (AccountModel)accountsDataGrid.SelectedItem;

            // FILTER DATA FOR TRANSACTION WINDOW BASED ON SELECTION
            TransactionsFilter filter = new TransactionsFilter();
            filter.FilterData(selectedAccount);

            // CREATE NEW WINDOW PASSING FILTERED DATA
            TransactionsWindow transactionsWindow = new TransactionsWindow(this, filter, selectedAccount);
            transactionsWindow.Show();

            this.Hide();
        }


        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow();
        }

        private void accountsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (accountsDataGrid.Items.Count != 0 && accountsDataGrid.SelectedItem != null)
            {
                ChangeWindow();
            }
        }
    }
}
