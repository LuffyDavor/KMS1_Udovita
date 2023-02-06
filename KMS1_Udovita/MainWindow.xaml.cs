using KMS1_Udovita.Filters;
using KMS1_Udovita.Models;
using System.Windows;
using System.Windows.Input;

namespace KMS1_Udovita
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowMethods
    {
        public static CsvReader csvReader = new CsvReader();

        public MainWindow()
        {

            InitializeComponent();
            HandleDetailsBtn();

        }
        public void ChangeWindow()
        {
            // GET SELECTED CUSTOMER
            CustomerModel selectedCustomer = (CustomerModel)customersDataGrid.SelectedItem;

            // FILTER DATA FOR ACCOUNTS WINDOW BASED ON SELECTED CUSTOMER
            AccountsFilter filter = new AccountsFilter();
            filter.FilterData(selectedCustomer);

            // CREATE NEW ACCOUNTS WINDOW PASSING THE FILTERED DATA
            AccountsWindow accountsWindow = new AccountsWindow(this, filter, selectedCustomer);
            accountsWindow.Show();

            this.Hide();
        }

        public void HandleDetailsBtn()
        {
            customersDataGrid.SelectionChanged += (s, e) =>
            {
                if (customersDataGrid.SelectedItem == null)
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

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            // GET DATA
            await csvReader.OpenFiles();
            csvReader.StoreCustomerData();
            csvReader.StoreAccountData();
            csvReader.StoreTransactionData();

            // CHECK IF SUCCESSFUL ELSE CANCEL PROCESS
            if (csvReader.Customers == null || csvReader.Accounts == null || csvReader.Transactions == null)
            {
                return;
            }

            // IF SUCCESSFUL DISABLE BUTTON AND ENABLE DETAILS BUTTON
            btnImport.IsEnabled= false;
            btnImport.Visibility= Visibility.Hidden;
            HandleDetailsBtn();

            // SET CUSTOMER GRID DATA SOURCE
            customersDataGrid.ItemsSource = csvReader.AllCustomerData;

        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow();

        }

        private void customersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(customersDataGrid.Items.Count != 0 && customersDataGrid.SelectedItem != null) 
            { 
                ChangeWindow(); 
            }
        }

    }
}
