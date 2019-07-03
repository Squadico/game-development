using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Network.Transport.Tcp
{
    public class Protocol
    {
        public event Action Connected;
        public event Action Disconnected;
        public event Action<byte[]> DataReceived;

        int Port;
        IPAddress Ip;
        TcpClient Client;

        AsyncCollection<byte[]> SendQueue;
        AsyncCollection<byte[]> ReceiveQueue;

        NetworkStream Stream;
        ReceivingStream ReceivingStream;
        CancellationTokenSource CancelSource;

        public Protocol(IPAddress ip, int port)
        {
            Ip = ip;
            Port = port;

            ReceivingStream = new ReceivingStream();
            SendQueue = new AsyncCollection<byte[]>(new ConcurrentQueue<byte[]>());
            ReceiveQueue = new AsyncCollection<byte[]>(new ConcurrentQueue<byte[]>());

            ReceiveAsync();
        }

        public async void ConnectAsync()
        {
            CancelSource = new CancellationTokenSource();

            try
            {
                Client = new TcpClient();
                await Client.ConnectAsync(Ip, Port);

                Stream = Client.GetStream();

                var writing = EnableWriting();
                var reading = EnableReading();

                Connected?.Invoke();
            }
            catch
            {
                RemoteDisconnect();
            }
        }

        public async void SendAsync(byte[] message)
        {
            var packet = Data.AddSizeToMessage(message);
            await SendQueue.AddAsync(packet);
        }

        async Task EnableWriting()
        {
            try
            {
                while (await SendQueue.OutputAvailableAsync())
                {
                    var packet = await SendQueue.TakeAsync();
                    await Stream.WriteAsync(packet, 0, packet.Length);
                }
            }
            catch
            {
                RemoteDisconnect();
            }
        }

        async Task EnableReading()
        {
            try
            {
                while (true)
                {
                    var packet = await Stream.ReadMessageAsync(CancelSource.Token);
                    if (packet == null)
                    {
                        RemoteDisconnect();
                        return;
                    }

                    ReceivingStream.ReadMessage(packet);

                    while (ReceivingStream.HasMessage)
                        await ReceiveQueue.AddAsync(ReceivingStream.GetMessage());
                }
            }
            catch
            {
                RemoteDisconnect();
            }
        }
        async void ReceiveAsync()
        {
            while (true)
            {
                while (await ReceiveQueue.OutputAvailableAsync().ConfigureAwait(false))
                {
                    var packet = await ReceiveQueue.TakeAsync().ConfigureAwait(false);
                    DataReceived?.Invoke(packet);
                }
            }
        }

        public void Disconnect()
        {
            try { CancelSource?.Cancel(); } catch { }
            try { CancelSource?.Dispose(); } catch { }
            try { Client?.Close(); } catch { }
            try { Stream?.Close(); } catch { }
        }

        internal void RemoteDisconnect()
        {
            Disconnect();
            Disconnected?.Invoke();
        }
    }
}
