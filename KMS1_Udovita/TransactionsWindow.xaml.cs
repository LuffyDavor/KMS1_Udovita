using KMS1_Udovita.Filters;
using KMS1_Udovita.Models;
using KMS1_Udovita.Writers;
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

namespace KMS1_Udovita
{
    /// <summary>
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        public static TransactionsFilter Filter { get; private set; }
        public TransactionsWindow(TransactionsFilter filter, AccountModel selectedAccount)
        {
            InitializeComponent();
            Filter= filter;

            transWindow.DataContext = Filter;
            senderDataGrid.ItemsSource = Filter.FilteredListSender;
            receiverDataGrid.ItemsSource = Filter.FilteredListReceiver;
            txtSent.DataContext = selectedAccount;
            txtReceived.DataContext = selectedAccount;
            txtTotal.DataContext = selectedAccount;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CsvWriter csvWriter= new CsvWriter();
            csvWriter.SaveFiles();
        }
    }
}
