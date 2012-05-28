namespace svmFace
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureTransport = new System.Windows.Forms.OpenFileDialog();
            this.train = new System.Windows.Forms.Button();
            this.faceDir = new System.Windows.Forms.TextBox();
            this.selectPath = new System.Windows.Forms.Button();
            this.dirWithFaces = new System.Windows.Forms.FolderBrowserDialog();
            this.start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pictureTransport
            // 
            this.pictureTransport.FileName = "openFileDialog1";
            // 
            // train
            // 
            this.train.Location = new System.Drawing.Point(218, 85);
            this.train.Name = "train";
            this.train.Size = new System.Drawing.Size(75, 23);
            this.train.TabIndex = 2;
            this.train.Text = "ClickMe";
            this.train.UseVisualStyleBackColor = true;
            this.train.Click += new System.EventHandler(this.train_Click);
            // 
            // faceDir
            // 
            this.faceDir.Enabled = false;
            this.faceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.faceDir.Location = new System.Drawing.Point(12, 12);
            this.faceDir.Name = "faceDir";
            this.faceDir.Size = new System.Drawing.Size(185, 22);
            this.faceDir.TabIndex = 3;
            // 
            // selectPath
            // 
            this.selectPath.Location = new System.Drawing.Point(218, 11);
            this.selectPath.Name = "selectPath";
            this.selectPath.Size = new System.Drawing.Size(75, 23);
            this.selectPath.TabIndex = 4;
            this.selectPath.Text = "Browse";
            this.selectPath.UseVisualStyleBackColor = true;
            this.selectPath.Click += new System.EventHandler(this.selectPath_Click);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(12, 40);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 5;
            this.start.Text = "Train";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Test all";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 120);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.start);
            this.Controls.Add(this.selectPath);
            this.Controls.Add(this.faceDir);
            this.Controls.Add(this.train);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog pictureTransport;
        private System.Windows.Forms.Button train;
        private System.Windows.Forms.TextBox faceDir;
        private System.Windows.Forms.Button selectPath;
        private System.Windows.Forms.FolderBrowserDialog dirWithFaces;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button button1;
    }
}

