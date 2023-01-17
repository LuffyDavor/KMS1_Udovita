using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMS1_Udovita.Readers
{
    public class StoreData<T>
    {
        public Action<string[], List<T>> ProcessDelegate { get; set; }

        public void ProcessData(string[] data, List<T> list)
        {
            if (data == null) { return; }
            foreach (string line in data)
            {
                try
                {
                    ProcessDelegate(line.Split(','), list);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Format in given file !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }

}
