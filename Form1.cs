using BatExplorerLibrary;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> log = new List<string>();

        private ProgressDialog ProgressDialog { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);
            this.ProgressDialog = new ProgressDialog();
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            string folderPath = SelectFolder();

            if (folderPath != null)
            {
                this.ProgressDialog.Show();
                await DeleteIncompleteRecordsFromFolder(folderPath);
                this.ProgressDialog.Hide();
                ShowLog();
            }
            Close();
        }

        private void ShowLog()
        {
            RecordListDialog dialog = new RecordListDialog();
            dialog.SetLog(log);
            DialogResult result = dialog.ShowDialog(this);
        }

        private async Task DeleteIncompleteRecordsFromFolder(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            this.ProgressDialog.StatusLabel.Text = "Wat zou er niet compleet zijn? Even kijken...";
            List<Record> incompleteRecords = await GetIncompleteRecordsFromFolder(directoryInfo);

            if(incompleteRecords.Count > 0)
            {
                this.ProgressDialog.StatusLabel.Text = incompleteRecords.Count + " incomplete opnames gevonden.";
            } else
            {
                this.ProgressDialog.StatusLabel.Text = "Niks incompleet gevonden.";
            }

            foreach (Record record in incompleteRecords)
            {
                LogDeleteRecord(record);
                await DeleteRecord(record);
            }
            this.ProgressDialog.StatusLabel.Text = "Klaar";
        }

        private void LogDeleteRecord(Record record)
        {
            log.Add(record.Name);
        }

        private Task DeleteRecord(Record record)
        {
            return Task.Run(() =>
            {
                record.AudioFile?.Delete();
                record.MetaDataFile?.Delete();
            });
        }

        private async Task<List<Record>> GetIncompleteRecordsFromFolder(DirectoryInfo folder)
        {
            List<Record> records = await Record.FromDirectory(folder);
            List<Record> incompleteRecords = records.Where((record) =>
            {
                return !record.HasAudioFile() || !record.HasMetaDataFile();
            }).ToList();
            return incompleteRecords;
        }

        private string SelectFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Kies de folder waar je incomplete opnames wilt verwijderen.";
            folderBrowserDialog.ShowNewFolderButton = false;

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }

            return null;
        }

        private void recordList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
