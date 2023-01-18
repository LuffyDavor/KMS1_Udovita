using KMS1_Udovita.Models;
using System.Collections.Generic;
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

        // MODELS FOR EACH OF REQUIRED DATA TYPE
        protected CustomerModel _customerData;
        protected AccountModel _accountData;
        protected TransactionModel _transactionData;

        /// <summary>
        /// Handles Opening Files Asynchrously
        /// </summary>
        /// <returns>Returns a task (basically void)</returns>
        public abstract Task OpenFiles();
    }
}
