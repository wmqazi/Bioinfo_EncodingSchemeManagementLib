using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Qazi.BinaryFileIOManager;

namespace Qazi.EncodingSchemeManager
{
    public partial class EncodingSchemeDefinationWnd : Form
    {
        private DataTable dtGrid;
        private Dictionary<string, string> htEncoding;
        private BinaryFileSerializationManager binaryFileManager;

        public EncodingSchemeDefinationWnd()
        {
            InitializeComponent();
            dtGrid = new DataTable();
            dataGridView1.DataSource = dtGrid;
            dtGrid.Columns.Add("Items");
            dtGrid.Columns.Add("Code");
            dataGridView1.Columns[1].Width = 414;
            htEncoding = new Dictionary<string, string>();
            binaryFileManager = new BinaryFileSerializationManager();
            New();
        }

        private void New()
        {
            if (dtGrid.Rows.Count > 0)
                dtGrid.Rows.Clear();
            
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            New();
        }

        private void saveDlg_FileOk(object sender, CancelEventArgs e)
        {
            if (htEncoding.Count > 0)
                htEncoding.Clear();

            foreach (DataRow row in dtGrid.Rows)
            {
                if (htEncoding.ContainsKey(row["Items"].ToString()) == true)
                {
                    MessageBox.Show("Duplicate Key Items Detected. Cannot Proceede...");
                    htEncoding.Clear();
                    return;
                }
                htEncoding.Add(row["Items"].ToString(), row["Code"].ToString());
            }
            htEncoding.Add("<IsBinaryString>", chkIsBinaryString.Checked.ToString());
            binaryFileManager.Save(saveDlg.FileName, htEncoding);
        }

        private void openDlg_FileOk(object sender, CancelEventArgs e)
        {
            htEncoding =((Dictionary<string,string>) binaryFileManager.Open(openDlg.FileName));
            EncodingFormateConvertor.HashToDataTable(htEncoding,dtGrid);
            if(htEncoding.ContainsKey("<IsBinaryString>") )
                chkIsBinaryString.Checked = bool.Parse(htEncoding["<IsBinaryString>"]);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            //open Dialog
            openDlg.ShowDialog(this);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            
            saveDlg.ShowDialog(this);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        
    }
}