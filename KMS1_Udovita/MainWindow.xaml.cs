using KMS1_Udovita.Filters;
using KMS1_Udovita.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMS1_Udovita
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static CsvReader csvReader = new CsvReader();
        public MainWindow()
        {

            InitializeComponent();

            customersDataGrid.SelectionChanged += (s, e) => {
                if (customersDataGrid.SelectedItem == null)
                {
                    btnDetails.IsEnabled = false;
                }
                else
                {
                    btnDetails.IsEnabled = true;
                }
            };
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

            csvReader.OpenFiles();
            csvReader.StoreCustomerData();
            csvReader.StoreAccountData();
            csvReader.StoreTransactionData();

            myWindow.DataContext = csvReader;
            customersDataGrid.ItemsSource = csvReader.AllCustomerData;

            if (csvReader.Customers == null || csvReader.Accounts == null || csvReader.Transactions == null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();

            }

        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            CustomerModel selectedCustomer = (CustomerModel)customersDataGrid.SelectedItem;

            AccountsFilter filter = new AccountsFilter();
            filter.FilterData(selectedCustomer);

            var accountsWindow = new AccountsWindow(filter, selectedCustomer);
            accountsWindow.Show();

            this.Close();

        }
    }
}
