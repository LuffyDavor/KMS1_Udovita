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
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace KMS1_Udovita
{
    /// <summary>
    /// Interaction logic for AccountsWindow.xaml
    /// </summary>
    public partial class AccountsWindow : Window, IWindowMethods
    {
        public AccountsWindow(AccountsFilter filter)
        {
            InitializeComponent();

            //accountsWindow.DataContext = filter;
            accountsDataGrid.ItemsSource = filter.FilteredList;
            HandleDetailsBtn();
            //this.Closing += AccountsWindow_Closed;
        }

        private void HandleDetailsBtn()
        {
            // HANDLING BTN "DETAILS" ENABLE/DISABLE
            accountsDataGrid.SelectionChanged += (s, e) =>
            {
                if (accountsDataGrid.SelectedItem == null)
                {
                    btnDetails.IsEnabled = false;
                }
                else
                {
                    btnDetails.IsEnabled = true;
                }
            };
        }

        public void ChangeWindow()
        {
            AccountModel selectedAccount = (AccountModel)accountsDataGrid.SelectedItem;

            TransactionsFilter filter = new TransactionsFilter();
            filter.FilterData(selectedAccount);

            TransactionsWindow transactionsWindow = new TransactionsWindow(filter, selectedAccount);
            transactionsWindow.Show();

            this.Close();
        }

        private void AccountsWindow_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.customersDataGrid.ItemsSource = MainWindow.csvReader.AllCustomerData;
        }
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow();
        }

        private void accountsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (accountsDataGrid.Items.Count != 0)
            {
                ChangeWindow();
            }
        }
    }
}
