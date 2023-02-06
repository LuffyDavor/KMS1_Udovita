using KMS1_Udovita.Models;
using System.Collections.Generic;

namespace KMS1_Udovita.Filters
{
    public class AccountsFilter
    {
        private List<AccountModel> _filteredList = new List<AccountModel>();
        public List<AccountModel> FilteredList { get => _filteredList; set => _filteredList = value; }

        /// <summary>
        /// Get data of selected customer
        /// </summary>
        /// <param name="selectedCustomer"></param>
        public void FilterData(CustomerModel selectedCustomer) 
        {

            foreach (AccountModel acc in MainWindow.csvReader.AllAccountData)
            {
                // RESET IN CASE SAME CUSTOMER IS CLICKED MULTIPLE TIMES
                acc.Balance = 0;
                acc.TransactionsAmount = 0;
                acc.SentAmount= 0;
                acc.ReceivedAmount= 0;

                // GET ACCOUNTS OF SELECTED CUSTOMER
                if (acc.CustomerID == selectedCustomer.CustomerID)
                {
                    GetTransactionsAmount(acc);
                    GetAccountBalance(acc);
                    FilteredList.Add(acc);
                }
            }
        }

        /// <summary>
        /// Gets total number of transactions per account
        /// </summary>
        /// <param name="selectedAccount"></param>
        public void GetTransactionsAmount(AccountModel selectedAccount) 
        {
            foreach (TransactionModel trans in MainWindow.csvReader.AllTransactionData)
            {
                if (trans.SenderAccountNr == selectedAccount.AccountNumber || trans.ReceiverAccountNr == selectedAccount.AccountNumber)
                {
                    selectedAccount.TransactionsAmount++;
                }
            }
        }
        /// <summary>
        /// Calculates balance, sent amount and received amount
        /// </summary>
        /// <param name="selectedAccount"></param>
        public void GetAccountBalance(AccountModel selectedAccount)
        {
            foreach (TransactionModel trans in MainWindow.csvReader.AllTransactionData)
            {

                if (trans.SenderAccountNr == selectedAccount.AccountNumber)
                {
                    selectedAccount.SentAmount -= trans.Amount;
                }
                else if (trans.ReceiverAccountNr == selectedAccount.AccountNumber)
                {
                    selectedAccount.ReceivedAmount += trans.Amount;
                }
            }
            selectedAccount.Balance = selectedAccount.SentAmount + selectedAccount.ReceivedAmount;
        }

    }
}
