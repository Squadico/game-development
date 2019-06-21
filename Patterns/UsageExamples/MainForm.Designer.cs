namespace UsageExamples
{
    partial class MainForm
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
            this.SimpleStatePatternExampleButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SimpleStatePatternExampleButton
            // 
            this.SimpleStatePatternExampleButton.Location = new System.Drawing.Point(6, 19);
            this.SimpleStatePatternExampleButton.Name = "SimpleStatePatternExampleButton";
            this.SimpleStatePatternExampleButton.Size = new System.Drawing.Size(309, 61);
            this.SimpleStatePatternExampleButton.TabIndex = 0;
            this.SimpleStatePatternExampleButton.Text = "SimpleStatePattern";
            this.SimpleStatePatternExampleButton.UseVisualStyleBackColor = true;
            this.SimpleStatePatternExampleButton.Click += new System.EventHandler(this.SimpleStatePatternExampleButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SimpleStatePatternExampleButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 94);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Examples";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 116);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SimpleStatePatternExampleButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

