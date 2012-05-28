using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SVM;
using svmFace.Providers;

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
                var provider = new ParallelLocalProvider(faceDir.Text, "train");
                Redis.getInstance().wipe();
                Trainer.build(provider);
            }
        }

        private void train_Click(object sender, EventArgs e)
        {
            pictureTransport.ShowDialog();
            if (File.Exists(pictureTransport.FileName)) {
                var provider = new LocalProvider(pictureTransport.FileName, "predict");
                provider.GotName += (ob, name) => MessageBox.Show(name);
                Trainer.build(provider);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            dirWithFaces.ShowDialog();
            if (Directory.Exists(dirWithFaces.SelectedPath)) {
                var prov = new LocalProvider(dirWithFaces.SelectedPath, "multipredict");
                Trainer.build(prov);
            }
        }
    }
}
