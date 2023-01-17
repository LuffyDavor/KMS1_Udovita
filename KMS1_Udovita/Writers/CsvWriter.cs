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

                    SaveTransactions(senderFilePath, Filter.FilteredListSender);

                    SaveTransactions(receiverFilePath, Filter.FilteredListReceiver);

                    MessageBox.Show($"Data exported successfully to:{senderFilePath} and {receiverFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SaveTransactions(string filePath, List<TransactionModel> transactions)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine("Sender-KntNr,Empfaenger-KntNr,Verwendungszweck,Betrag,Buchungsdatum");
                foreach (TransactionModel transaction in transactions)
                {
                    streamWriter.WriteLine(string.Join(",",
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
