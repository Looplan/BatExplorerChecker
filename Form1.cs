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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string folderPath = SelectFolder();

            if (folderPath != null)
            {
                DeleteIncompleteRecordsFromFolder(folderPath);
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

        private void DeleteIncompleteRecordsFromFolder(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            List<Record> incompleteRecords = GetIncompleteRecordsFromFolder(directoryInfo);

            foreach (Record record in incompleteRecords)
            {
                LogDeleteRecord(record);
                DeleteRecord(record);
            }
        }

        private void LogDeleteRecord(Record record)
        {
            log.Add(record.Name);
        }

        private void DeleteRecord(Record record)
        {
            record.AudioFile?.Delete();
            record.MetaDataFile?.Delete();
        }

        private List<Record> GetIncompleteRecordsFromFolder(DirectoryInfo folder)
        {
            List<Record> records = Record.FromDirectory(folder);
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
