using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace svmFace
{
    public partial class askName : Form
    {
        public askName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        public string getName() 
        {
            return textBox1.Text;
        }
    }
}
