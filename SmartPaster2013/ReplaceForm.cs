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

namespace SmartPaster2013
{
    public partial class ReplaceForm : Form
    {
        public ReplaceForm()
        {
            InitializeComponent();
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public string TextToReplace
            => this._tbFind.Text;

        public string ReplaceText
            => this._tbReplace.Text;

        public bool Regex
            => this._cbRegex.Checked;

        private void _tbFind_Click(object sender, EventArgs e)
        {
            _tbFind.Select(0, _tbFind.Text.Length);
        }

        private void _tbReplace_Click(object sender, EventArgs e)
        {
            _tbReplace.Select(0, _tbReplace.Text.Length);
        }

        private void ReplaceForm_Load(object sender, EventArgs e)
        {
            //DataTable table = new DataTable();
            //table.Columns.Add("Find");
            //table.Columns.Add("Replace");

            //using (StreamReader sr = new StreamReader(@"D:\Temp\fileread\readtext.txt"))
            //{
            //    while (!sr.EndOfStream)
            //    {
            //        string[] parts = sr.ReadLine().Split(',');
            //        table.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
            //    }
            //}
            //_dgvGrid.DataSource = table;
        }

        private void _bntSave_Click(object sender, EventArgs e)
        {
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var saves = Path.Combine(systemPath, "saves");

            File.AppendAllText(saves, string.Format("{0}{1}", "OK", Environment.NewLine));
        }
    }
}
