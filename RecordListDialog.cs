using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatExplorerChecker
{
    public partial class RecordListDialog : Form
    {
        public RecordListDialog()
        {
            InitializeComponent();
        }

        private void RecordListDialog_Load(object sender, EventArgs e)
        {

        }

        public void SetLog(List<string> log)
        {
            log.ForEach((record) =>
            {
                recordList.Items.Add(record);
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.FileName = "log";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.AddExtension = true;
            DialogResult result = saveFileDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                Stream stream = saveFileDialog.OpenFile();
                StreamWriter writer = new StreamWriter(stream);

                foreach(string record in recordList.Items)
                {
                    writer.WriteLine(record);
                }
                writer.Flush();
                writer.Close();
            }
        }
    }
}
