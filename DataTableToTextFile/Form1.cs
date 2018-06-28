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

namespace DataTableToTextFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string _sFolderPath = string.Empty;
        private void btnSetPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog oDialog = new FolderBrowserDialog();
                if (oDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = oDialog.SelectedPath + "\\";
                }
                else
                {
                    return;
                }

                if (txtPath.Text.Trim().Length > 0)
                    _sFolderPath = txtPath.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Path cannot be set due to " + ex.Message, "Path Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            DataRow dRow = dt.NewRow();
            dRow["Name"] = "Name 1";
            dRow["Phone"] = "Phone 1";
            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["Name"] = "Name 2";
            dRow["Phone"] = "Phone 2";
            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["Name"] = "Name 3";
            dRow["Phone"] = "Phone 3";
            dt.Rows.Add(dRow);

            string sStringToWrite = string.Empty;
            string sSuccess = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                sStringToWrite += dr["Name"].ToString().Trim() + "\t\t" +
                                    dr["Phone"].ToString().Trim();
                sStringToWrite += Environment.NewLine;
            }

            if (sStringToWrite.Trim().Length > 0)
            {
                sSuccess += WriteToTxtFile(sStringToWrite, _sFolderPath, "Contact List") + Environment.NewLine;
                sStringToWrite = string.Empty;
            }
            if (sSuccess.Trim().Length > 0)
                MessageBox.Show(sSuccess);
        }

        public static string WriteToTxtFile(string sStringToWrite, string sFilePath, string sFileName)
        {
            string sSuccess = string.Empty;

            try
            {
                DirectoryInfo oFileDirectory = new DirectoryInfo(sFilePath);
                if (!oFileDirectory.Exists)
                {
                    oFileDirectory.Create();
                }

                StreamWriter oSWriter = new StreamWriter(sFilePath + "/" + sFileName + ".txt", false);
                oSWriter.WriteLine(sStringToWrite);
                sSuccess = "You can find a file named - " + sFileName + " at " + sFilePath;
                oSWriter.Flush();
                oSWriter.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return sSuccess;
        }
    }
}
