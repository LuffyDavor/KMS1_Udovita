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
            CustomerModel selectedCustomer = (CustomerModel)customersDataGrid.SelectedItem;

            AccountsFilter filter = new AccountsFilter();
            filter.FilterData(selectedCustomer);

            AccountsWindow accountsWindow = new AccountsWindow(this, filter);
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
                }
                else
                {
                    btnDetails.IsEnabled = true;
                }
            };
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {

            await csvReader.OpenFiles();
            csvReader.StoreCustomerData();
            csvReader.StoreAccountData();
            csvReader.StoreTransactionData();

            if (csvReader.Customers == null || csvReader.Accounts == null || csvReader.Transactions == null)
            {
                return;
            }

            btnImport.IsEnabled= false;
            customersDataGrid.ItemsSource = csvReader.AllCustomerData;

        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow();

        }

        private void customersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(customersDataGrid.Items.Count != 0) 
            { 
                ChangeWindow(); 
            }
        }

    }
}
