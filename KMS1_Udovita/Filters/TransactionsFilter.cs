using KMS1_Udovita.Models;
using System.Collections.Generic;

namespace KMS1_Udovita.Filters
{
    public class TransactionsFilter
    {
        private List<TransactionModel> _filteredListSender = new List<TransactionModel>();
        public List<TransactionModel> FilteredListSender { get => _filteredListSender; set => _filteredListSender = value; }


        private List<TransactionModel> _filteredListReceiver = new List<TransactionModel>();
        public List<TransactionModel> FilteredListReceiver { get => _filteredListReceiver; set => _filteredListReceiver = value; }

        /// <summary>
        /// Filters Data depending if transaction is incoming or outgoing
        /// </summary>
        /// <param name="selectedAccount"></param>
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
