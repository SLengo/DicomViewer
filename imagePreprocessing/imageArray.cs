using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imagePreprocessing
{
    public partial class imageArray : Form
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

        public imageArray()
        {
            InitializeComponent();
            labelImageName.Text = "";
        }

        private void imageArray_Load(object sender, EventArgs e)
        {
            
        }

        private void imageArray_VisibleChanged(object sender, EventArgs e)
        {
            if (Program.mainForm.filenameFirstImage != "")
            {
                labelImageName.Text = Program.mainForm.filenameFirstImage;
                richTextBox1.Text = "[";
                for (int i = 4000; i < 5000; i++)
                {
                   richTextBox1.Text += Convert.ToString(Program.mainForm.arrNormalizeImage[i]) + ", ";   
                }
                richTextBox1.Text += "]";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.mainForm.Enabled = true;
            this.Hide();
        }
    }
}
