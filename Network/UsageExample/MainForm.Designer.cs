namespace UsageExample.Networking.Tcp
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
            this.TcpTransportExampleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TcpTransportExampleButton
            // 
            this.TcpTransportExampleButton.Location = new System.Drawing.Point(13, 13);
            this.TcpTransportExampleButton.Name = "TcpTransportExampleButton";
            this.TcpTransportExampleButton.Size = new System.Drawing.Size(309, 58);
            this.TcpTransportExampleButton.TabIndex = 0;
            this.TcpTransportExampleButton.Text = "TCP Transport Example";
            this.TcpTransportExampleButton.UseVisualStyleBackColor = true;
            this.TcpTransportExampleButton.Click += new System.EventHandler(this.TcpTransportExampleButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 82);
            this.Controls.Add(this.TcpTransportExampleButton);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TcpTransportExampleButton;
    }
}

