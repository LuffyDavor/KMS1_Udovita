using KMS1_Udovita.Filters;
using KMS1_Udovita.Models;
using KMS1_Udovita.Writers;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KMS1_Udovita
{
    /// <summary>
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        public static TransactionsFilter Filter { get; private set; }
        public static AccountModel SelectedAccount { get; private set; }
        private readonly AccountsWindow _accountsWindow;
        public TransactionsWindow(AccountsWindow accountsWindow, TransactionsFilter filter, AccountModel selectedAccount)
        {
            InitializeComponent();

            Filter = filter;
            SelectedAccount = selectedAccount;
            _accountsWindow = accountsWindow;

            // HANDLE ITEM SOURCES FOR GRIDS
            transWindow.DataContext = Filter;

            // SORT LISTS BEFORE SETTING ITEM SOURCES
            Filter.FilteredListSender = Filter.FilteredListSender.OrderByDescending(x => x.BookingDate.Date).ToList();
            Filter.FilteredListReceiver = Filter.FilteredListReceiver.OrderByDescending(x => x.BookingDate.Date).ToList();

            senderDataGrid.ItemsSource = Filter.FilteredListSender;
            receiverDataGrid.ItemsSource = Filter.FilteredListReceiver;


            // SET DATACONTEXT FOR TEXT BLOCKS
            txtAccName.DataContext = _accountsWindow;
            txtAccNr.DataContext = SelectedAccount;
            txtSent.DataContext = SelectedAccount;
            txtReceived.DataContext = SelectedAccount;
            txtTotal.DataContext = SelectedAccount;

            // HANDLE ACTION WHEN WINDOW IS CLOSED
            Closing += TransactionsWindow_Closing;


        }

        private void TransactionsWindow_Closing(object sender, CancelEventArgs e)
        {
            _accountsWindow.accountsDataGrid.SelectedItem = SelectedAccount;
            _accountsWindow.accountsDataGrid.Focus();
            _accountsWindow.Show();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CsvWriter csvWriter= new CsvWriter();
            csvWriter.SaveFiles();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
