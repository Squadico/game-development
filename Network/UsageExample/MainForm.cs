using System;
using System.Windows.Forms;

namespace UsageExample.Networking.Tcp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TcpTransportExampleButton_Click(object sender, EventArgs e)
        {
            var example = new TCPTransportExample(this);
            example.Show();
        }
    }
}
