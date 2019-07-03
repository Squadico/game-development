using System;
using System.Windows.Forms;

namespace Demo.Network.TcpTransport
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void TcpTransportDemoButton_Click(object sender, EventArgs e)
        {
            var example = new TCPTransportDemoForm(this);
            example.Show();
        }
    }
}
