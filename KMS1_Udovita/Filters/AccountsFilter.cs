using KMS1_Udovita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_Udovita.Filters
{
    public class AccountsFilter
    {
        private List<AccountModel> _filteredList = new List<AccountModel>();
        public List<AccountModel> FilteredList { get => _filteredList; set => _filteredList = value; }


        public void FilterData(CustomerModel selectedCustomer) 
        {

            foreach (AccountModel acc in MainWindow.csvReader.AllAccountData)
            {
                if (acc.CustomerID == selectedCustomer.CustomerID)
                {
                    GetTransactionsAmount(acc);
                    GetAccountBalance(acc);
                    FilteredList.Add(acc);
                }
            }
        }

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
