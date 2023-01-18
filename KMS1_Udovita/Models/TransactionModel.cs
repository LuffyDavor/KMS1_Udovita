using System;

namespace KMS1_Udovita.Models
{
    public class TransactionModel
    {
        public string SenderAccountNr { get; set; }
        public string ReceiverAccountNr { get; set; }
        public string Usage{ get; set; }
        public decimal Amount { get; set; }
        public DateTime BookingDate{ get; set; }

    }
}
