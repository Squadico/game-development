using System;
using System.Text;
using Akka.Actor;
using Akka.IO;
using Network.Transport.Tcp;

namespace Demo.TestServer
{
    public sealed class Gateway : ReceiveActor
    {
        IActorRef Connection;
        
        ReceivingStream DataStream;

        public Gateway(IActorRef connection)
        {
            DataStream = new ReceivingStream();

            Connection = connection;
            Context.Watch(connection);

            Receive<Tcp.Received>(received => OnReceivedData(received));
            Receive<Tcp.ConnectionClosed>(closed =>
            {
                Console.WriteLine("Disconnected");
                Context.Stop(Self);
            });
            
            Receive<Terminated>(terminated =>
            {
                Console.WriteLine("Disconnected");
                Context.Stop(Self);
            });
        }

        void OnReceivedData(Tcp.Received received)
        {
            DataStream.ReadMessage(received.Data.ToArray());
            while (DataStream.HasMessage)
            {
                var data = DataStream.GetMessage();
                
                //Deserializing data is shown on server
                Console.WriteLine(Encoding.UTF8.GetString(data));

                //Echo the same data to client
                Reply(data);
            }
        }

        void Reply(byte[] message)
        {
            var packet = Data.AddSizeToMessage(message);
            Connection.Tell(Tcp.Write.Create(ByteString.CopyFrom(packet)));
        }
    }
}
