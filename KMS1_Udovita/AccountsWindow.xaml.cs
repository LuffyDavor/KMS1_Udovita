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
    public partial class AccountsWindow : Window
    {
        public AccountsWindow(AccountsFilter filter, CustomerModel selectedCustomer)
        {
            InitializeComponent();



            accountsWindow.DataContext = filter;
            accountsDataGrid.ItemsSource = filter.FilteredList;
            accountsDataGrid.SelectionChanged += (s, e) => {
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

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            AccountModel selectedAccount = (AccountModel)accountsDataGrid.SelectedItem;
            TransactionsFilter filter = new TransactionsFilter();
            filter.FilterData(selectedAccount);
            var transactionsWindow = new TransactionsWindow(filter, selectedAccount);
            transactionsWindow.Show();
            this.Close();
        }
    }
}
