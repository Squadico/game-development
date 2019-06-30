using Akka.Actor;
using Networking.Transport.Common;
using System;
using System.Net;

namespace TestServer
{
    class Program
    {
        static ActorSystem System;

        static void Main(string[] args)
        {
            System = ActorSystem.Create("system");
            
            System.ActorOf(Props.Create<TCPBinder>(new IPEndPoint(IPAddress.Any, TestConfig.ServerPort)), "TCPBinder");

            Console.WriteLine("Server started");
            Console.ReadLine();
            System.Terminate();
        }
    }
}
