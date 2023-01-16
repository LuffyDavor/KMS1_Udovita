using KMS1_Udovita.Filters;
using KMS1_Udovita.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMS1_Udovita.Writers
{
    public class CsvWriter : Writer
    {
        public TransactionsFilter Filter => TransactionsWindow.Filter;
        public override void SaveFiles()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    string senderFilePath = Path.Combine(folderPath, "SenderTransactions.csv");
                    string receiverFilePath = Path.Combine(folderPath, "ReceiverTransactions.csv");

                    SaveSender(senderFilePath);

                    SaveReceiver(receiverFilePath);

                    MessageBox.Show($"Data exported successfully to:{senderFilePath} and {receiverFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SaveReceiver(string receiverFilePath)
        {
            using (StreamWriter receiverStreamWriter = new StreamWriter(receiverFilePath))
            {
                receiverStreamWriter.WriteLine("Sender-KntNr,Empfaenger-KntNr,Verwendungszweck,Betrag,Buchungsdatum");
                foreach (TransactionModel transaction in Filter.FilteredListReceiver)
                {
                    receiverStreamWriter.WriteLine(string.Join(",",
                                                               transaction.SenderAccountNr,
                                                               transaction.ReceiverAccountNr,
                                                               transaction.Usage,
                                                               transaction.Amount,
                                                               transaction.BookingDate));
                }
            }
        }

        private void SaveSender( string senderFilePath)
        {
            using (StreamWriter senderStreamWriter = new StreamWriter(senderFilePath))
            {
                senderStreamWriter.WriteLine("Sender-KntNr,Empfaenger-KntNr,Verwendungszweck,Betrag,Buchungsdatum");
                foreach (TransactionModel transaction in Filter.FilteredListSender)
                {
                    senderStreamWriter.WriteLine(string.Join(",",
                                                             transaction.SenderAccountNr,
                                                             transaction.ReceiverAccountNr,
                                                             transaction.Usage,
                                                             transaction.Amount,
                                                             transaction.BookingDate));
                }
            }
        }


    }
}
