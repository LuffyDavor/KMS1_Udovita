using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_Udovita.Models
{
    public class AccountModel : BaseModel
    {
        public string AccountNumber {get; set;}
        public int TransactionsAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal SentAmount { get; set; }

    }
}
