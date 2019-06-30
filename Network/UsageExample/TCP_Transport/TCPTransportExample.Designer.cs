namespace UsageExample.Networking.Tcp
{
    partial class TCPTransportExample
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
            this.LogArea = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.DataLength = new System.Windows.Forms.NumericUpDown();
            this.Length = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RepeatDataCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatDataCount)).BeginInit();
            this.SuspendLayout();
            // 
            // LogArea
            // 
            this.LogArea.BackColor = System.Drawing.SystemColors.Control;
            this.LogArea.Location = new System.Drawing.Point(6, 18);
            this.LogArea.Name = "LogArea";
            this.LogArea.ReadOnly = true;
            this.LogArea.Size = new System.Drawing.Size(261, 364);
            this.LogArea.TabIndex = 0;
            this.LogArea.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LogArea);
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 387);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logger";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(20, 13);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(259, 26);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(20, 94);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(261, 28);
            this.DisconnectButton.TabIndex = 3;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(20, 45);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(99, 40);
            this.SendButton.TabIndex = 4;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // DataLength
            // 
            this.DataLength.Location = new System.Drawing.Point(186, 44);
            this.DataLength.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.DataLength.Name = "DataLength";
            this.DataLength.Size = new System.Drawing.Size(62, 20);
            this.DataLength.TabIndex = 5;
            this.DataLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Length
            // 
            this.Length.AutoSize = true;
            this.Length.Location = new System.Drawing.Point(138, 47);
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(40, 13);
            this.Length.TabIndex = 6;
            this.Length.Text = "Length";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "bytes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Repeat";
            // 
            // RepeatDataCount
            // 
            this.RepeatDataCount.Location = new System.Drawing.Point(187, 67);
            this.RepeatDataCount.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.RepeatDataCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RepeatDataCount.Name = "RepeatDataCount";
            this.RepeatDataCount.Size = new System.Drawing.Size(61, 20);
            this.RepeatDataCount.TabIndex = 9;
            this.RepeatDataCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "times";
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.Location = new System.Drawing.Point(18, 521);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(261, 23);
            this.ClearLogButton.TabIndex = 11;
            this.ClearLogButton.Text = "Clear Log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // TCPTransportExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 556);
            this.Controls.Add(this.ClearLogButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RepeatDataCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Length);
            this.Controls.Add(this.DataLength);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "TCPTransportExample";
            this.Text = "TCPTransportExample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCPTransportExample_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatDataCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogArea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.NumericUpDown DataLength;
        private System.Windows.Forms.Label Length;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown RepeatDataCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ClearLogButton;
    }
}