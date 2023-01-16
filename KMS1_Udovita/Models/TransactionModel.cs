using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_Udovita.Models
{
    public class TransactionModel
    {
        public string SenderAccountNr { get; set; }
        public string ReceiverAccountNr { get; set; }
        public string Usage{ get; set; }
        public decimal Amount { get; set; }
        public string BookingDate{ get; set; }

    }
}
