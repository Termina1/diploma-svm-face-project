using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SVM;

namespace svmFace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        protected Trainer trainer;

        private void selectPath_Click(object sender, EventArgs e)
        {
            dirWithFaces.ShowDialog();
            if (Directory.Exists(dirWithFaces.SelectedPath))
            {
                faceDir.Text = dirWithFaces.SelectedPath;
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (faceDir.Text != "")
            {
                //trainer = Trainer.build(faceDir.Text);
            }
        }

        private void train_Click(object sender, EventArgs e)
        {
            pictureTransport.ShowDialog();
            if (File.Exists(pictureTransport.FileName)) {
                //MessageBox.Show(trainer.predict(pictureTransport.FileName));
            }
        }
    }
}
