using System;
using System.Linq;
using System.Net;
using System.Text;
using Network.Transport;
using System.Windows.Forms;
using Networking.Transport.Common;

namespace UsageExample.Networking.Tcp
{
    public partial class TCPTransportExample : Form
    {
        MainForm Main;
        Logger Logger;
        Protocol Protocol;
        static Random Random = new Random();

        public TCPTransportExample(MainForm main)
        {
            Main = main;
            Main.Hide();

            InitializeComponent();

            Logger = new Logger(LogArea);
            Protocol = new Protocol(IPAddress.Loopback, TestConfig.ServerPort);

            Protocol.Connected += () => Logger.Message("Connected");
            Protocol.Disconnected += () => Logger.Error("Disconnected");
            Protocol.DataReceived += OnDataReceived;
        }
        
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Protocol.ConnectAsync();
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            Protocol.Disconnect();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < RepeatDataCount.Value; i++)
            {
                var length = (int)DataLength.Value;
                var data = Encoding.UTF8.GetBytes(RandomString(length));

                Protocol.SendAsync(data);
            }
        }

        void OnDataReceived(byte[] data)
        {
            var message = Encoding.UTF8.GetString(data);
            Logger.Message(message);
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            Logger.Clear();
        }

        private void TCPTransportExample_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.Show();
        }

        static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return
                new string(
                    Enumerable
                        .Repeat(chars, length)
                        .Select(s => s[Random.Next(s.Length)])
                        .ToArray());
        }
    }
}
