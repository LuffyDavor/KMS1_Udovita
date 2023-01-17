using KMS1_Udovita.Models;
using KMS1_Udovita.Readers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace KMS1_Udovita
{
    public class CsvReader : Reader
    {
        public string[] Customers { get; private set; }
        public string[] Accounts { get; private set; }
        public string[] Transactions { get; private set; }
      

        public override void OpenFiles()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;

                    if (Directory.Exists(folderPath) &&
                        File.Exists(Path.Combine(folderPath, "Kunden.csv")) &&
                        File.Exists(Path.Combine(folderPath, "Konten.csv")) &&
                        File.Exists(Path.Combine(folderPath, "Buchungen.csv")))
                    {

                        Customers = File.ReadAllLines(Path.Combine(folderPath, "Kunden.csv")).Skip(1).ToArray();
                        Accounts = File.ReadAllLines(Path.Combine(folderPath, "Konten.csv")).Skip(1).ToArray();
                        Transactions = File.ReadAllLines(Path.Combine(folderPath, "Buchungen.csv")).Skip(1).ToArray();
                    }
                    else
                    {
                        MessageBox.Show("The selected folder does not contain the required CSV files (Kunden.csv, Konten.csv, Buchungen.csv)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void StoreCustomerData()
        {
            if(Customers == null ) { return; }
            foreach (string line in Customers)
            {
                string[] singleCustomerData = line.Split(',');

                try
                {
                    _customerData = new CustomerModel()
                    {
                        CustomerID = Convert.ToInt32(singleCustomerData[0]),
                        Name = singleCustomerData[1],

                    };
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Format in given file !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AllCustomerData.Add(_customerData);
            }
        }

        public void StoreAccountData()
        {
            if (Accounts == null) { return; }
            foreach (string line in Accounts)
            {
                string[] singleAccountData = line.Split(',');

                try
                {
                    _accountData = new AccountModel()
                    {
                        CustomerID = Convert.ToInt32(singleAccountData[0]),
                        AccountNumber = singleAccountData[1],

                    };
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Format in given file !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AllAccountData.Add(_accountData);
            }
        }

        public void StoreTransactionData()
        {
            if (Transactions == null) { return; }
            foreach (string line in Transactions)
            {
                string[] singleTransactionData = line.Split(',');

                try
                {
                    _transactionData = new TransactionModel()
                    {
                        SenderAccountNr = singleTransactionData[0],
                        ReceiverAccountNr = singleTransactionData[1],
                        Usage = singleTransactionData[2],
                        Amount = Convert.ToDecimal(singleTransactionData[3].Replace('.',',')),
                        BookingDate = singleTransactionData[4],
                    };
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Format in given file !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AllTransactionData.Add(_transactionData);
            }
        }
    }
}
