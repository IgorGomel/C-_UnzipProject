using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Diagnostics;

namespace Unzipping
{
    
    public partial class Form1 : Form
    {
        string tempFileName;
        ZipArchive archive;
        public string ourFile;
        string extractPath = @".\extract";

        public void A()
        { 
            Directory.CreateDirectory(extractPath);
            
            archive = ZipFile.Open(ourFile, ZipArchiveMode.Update);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".csv"))
                {
                    entry.ExtractToFile(extractPath + "\\" + entry.Name, true);
                    tempFileName = extractPath + "\\" + entry.Name;
                    this.B(tempFileName);
                }
            }

            MessageBox.Show("Файли розархівіровано!");

            var folder = Directory.GetFiles(extractPath);
      
            foreach (var entry in folder)
            {
                File.Delete(entry);
            }

            Directory.Delete(extractPath);
            btUnzipCSV.Enabled = false;
            
        }

        public void B(string tmp)
        {
            MessageBox.Show("Медод Б викликано для " + tmp +" !");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripOpenFile_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.EndsWith(".zip"))
            {
                ourFile = openFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("Виберіть архівний файл з розширенням .zip");
                btUnzipCSV.Enabled = false;
                return;
            }
            btUnzipCSV.Enabled = true;
                 
        }

        private void btUnzipCSV_Click(object sender, EventArgs e)
        {
            this.A();
        }
    }
}
