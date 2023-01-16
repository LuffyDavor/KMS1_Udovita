using KMS1_Udovita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_Udovita.Filters
{
    public class TransactionsFilter
    {
        private List<TransactionModel> _filteredListSender = new List<TransactionModel>();
        public List<TransactionModel> FilteredListSender { get => _filteredListSender; set => _filteredListSender = value; }


        private List<TransactionModel> _filteredListReceiver = new List<TransactionModel>();
        public List<TransactionModel> FilteredListReceiver { get => _filteredListReceiver; set => _filteredListReceiver = value; }
        public void FilterData(AccountModel selectedAccount)
        {

            foreach (TransactionModel trans in MainWindow.csvReader.AllTransactionData)
            {
                if (trans.SenderAccountNr == selectedAccount.AccountNumber)
                {
                    FilteredListSender.Add(trans);
                }
                else if (trans.ReceiverAccountNr == selectedAccount.AccountNumber)
                {
                    FilteredListReceiver.Add(trans);
                }
            }
        }
    }
}
