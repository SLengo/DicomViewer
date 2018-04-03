using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dicomViewer
{
    public partial class SetTempDir : Form
    {

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public SetTempDir()
        {
            InitializeComponent();
        }

        private void SetTempDir_VisibleChanged(object sender, EventArgs e)
        {
            CurrDirTextBox.Text = Program.TempDir;
        }

        private void OpenDirButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(CurrDirTextBox.Text);
        }

        private void SetDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog BrowseCurrDir = new FolderBrowserDialog();
            if(DialogResult.OK == BrowseCurrDir.ShowDialog())
            {
                Program.IniObj.IniWriteValue("Info", "temp_folder", BrowseCurrDir.SelectedPath);
                Program.TempDir = BrowseCurrDir.SelectedPath;
                CurrDirTextBox.Text = BrowseCurrDir.SelectedPath;
            }
        }

        private void CnclButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.mainForm.Enabled = true;
        }
    }
}
