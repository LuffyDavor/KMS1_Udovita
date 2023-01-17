using KMS1_Udovita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_Udovita.Readers
{
    public abstract class Reader
    {
        // CUSTOMERS DATA LIST
        private readonly List<CustomerModel> _allCustomerData = new List<CustomerModel>();
        public List<CustomerModel> AllCustomerData => _allCustomerData;

        // ACCOUNTS DATA LIST
        private readonly List<AccountModel> _allAccountData = new List<AccountModel>();
        public List<AccountModel> AllAccountData => _allAccountData;

        // TRANSACTIONS DATA LIST
        private readonly List<TransactionModel> _allTransactionData = new List<TransactionModel>();
        public List<TransactionModel> AllTransactionData => _allTransactionData;


        protected CustomerModel _customerData;
        protected AccountModel _accountData;
        protected TransactionModel _transactionData;
        public abstract void OpenFiles();
    }
}
