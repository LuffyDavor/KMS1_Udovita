using KMS1_Udovita.Filters;
using KMS1_Udovita.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace KMS1_Udovita.Writers
{
    public class CsvWriter : Writer
    {
        public TransactionsFilter Filter => TransactionsWindow.Filter;

        /// <summary>
        /// Save Files Using FolderBrowserDialog 
        /// </summary>
        public override void SaveFiles()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // SET INITIAL DIRECTORY
                folderDialog.SelectedPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));

                // OPEN FOLDER DIALOG
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // GET USER PATH
                    string folderPath = folderDialog.SelectedPath;

                    // DETERMINE PATH OF FILES TO BE CREATED
                    string senderFilePath = Path.Combine(folderPath, "SenderTransactions.csv");
                    string receiverFilePath = Path.Combine(folderPath, "ReceiverTransactions.csv");

                    // USE SAVE TRANSACTIONS METHOD TO WRITE FILES
                    SaveTransactions(senderFilePath, Filter.FilteredListSender);
                    SaveTransactions(receiverFilePath, Filter.FilteredListReceiver);

                    // DISPLAY MESSAGEBOX WHEN WRITING HAS FINISHED
                    MessageBox.Show($"Data exported successfully to:{senderFilePath} and {receiverFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// StreamWriter Method to Write selected data to te given filepath
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="transactions"></param>
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
                streamWriter.Close();
            }
        }

    }
}
