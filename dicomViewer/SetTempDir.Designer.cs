namespace dicomViewer
{
    partial class SetTempDir
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
            this.CurrDirTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenDirButton = new System.Windows.Forms.Button();
            this.SetDirButton = new System.Windows.Forms.Button();
            this.CnclButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CurrDirTextBox
            // 
            this.CurrDirTextBox.Location = new System.Drawing.Point(12, 25);
            this.CurrDirTextBox.Name = "CurrDirTextBox";
            this.CurrDirTextBox.ReadOnly = true;
            this.CurrDirTextBox.Size = new System.Drawing.Size(277, 20);
            this.CurrDirTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current directory";
            // 
            // OpenDirButton
            // 
            this.OpenDirButton.Location = new System.Drawing.Point(295, 21);
            this.OpenDirButton.Name = "OpenDirButton";
            this.OpenDirButton.Size = new System.Drawing.Size(75, 27);
            this.OpenDirButton.TabIndex = 2;
            this.OpenDirButton.Text = "Open";
            this.OpenDirButton.UseVisualStyleBackColor = true;
            this.OpenDirButton.Click += new System.EventHandler(this.OpenDirButton_Click);
            // 
            // SetDirButton
            // 
            this.SetDirButton.Location = new System.Drawing.Point(12, 48);
            this.SetDirButton.Name = "SetDirButton";
            this.SetDirButton.Size = new System.Drawing.Size(161, 23);
            this.SetDirButton.TabIndex = 3;
            this.SetDirButton.Text = "Set directory";
            this.SetDirButton.UseVisualStyleBackColor = true;
            this.SetDirButton.Click += new System.EventHandler(this.SetDirButton_Click);
            // 
            // CnclButton
            // 
            this.CnclButton.Location = new System.Drawing.Point(179, 48);
            this.CnclButton.Name = "CnclButton";
            this.CnclButton.Size = new System.Drawing.Size(110, 23);
            this.CnclButton.TabIndex = 4;
            this.CnclButton.Text = "Cancel";
            this.CnclButton.UseVisualStyleBackColor = true;
            this.CnclButton.Click += new System.EventHandler(this.CnclButton_Click);
            // 
            // SetTempDir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 83);
            this.Controls.Add(this.CnclButton);
            this.Controls.Add(this.SetDirButton);
            this.Controls.Add(this.OpenDirButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CurrDirTextBox);
            this.Name = "SetTempDir";
            this.Text = "SetTempDir";
            this.VisibleChanged += new System.EventHandler(this.SetTempDir_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CurrDirTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenDirButton;
        private System.Windows.Forms.Button SetDirButton;
        private System.Windows.Forms.Button CnclButton;
    }
}