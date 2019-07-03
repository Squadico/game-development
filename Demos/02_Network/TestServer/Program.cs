using Akka.Actor;
using System;
using System.Net;

namespace Demo.TestServer
{
    class Program
    {
        static ActorSystem System;

        static void Main(string[] args)
        {
            System = ActorSystem.Create("system");
            
            System.ActorOf(Props.Create<TCPBinder>(new IPEndPoint(IPAddress.Any, 17779)), "TCPBinder");

            Console.WriteLine("Server started");
            Console.ReadLine();
            System.Terminate();
        }
    }
}
