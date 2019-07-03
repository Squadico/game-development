using System;
using System.Net;
using Akka.Actor;
using Akka.IO;

namespace Demo.TestServer
{
    public sealed class TCPBinder : ReceiveActor
    {
        public TCPBinder(EndPoint endpoint)
        {
            Context.System.Tcp().Tell(new Tcp.Bind(Self, endpoint));

            Receive<Tcp.Connected>(connected =>
            {
                Console.WriteLine($"Connected: {connected.RemoteAddress}");

                var connection = Sender;
                var gateway = Context.ActorOf(Props.Create<Gateway>(connection), $"Gateway_{Guid.NewGuid()}");
                connection.Tell(new Tcp.Register(gateway));
            });
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy( 
                maxNrOfRetries: 1, 
                withinTimeRange: TimeSpan.FromSeconds(1), 
                localOnlyDecider: x => Directive.Stop);
        }
    }
}
