using System;
using Akka.Actor;
//using Hackathon.EventStore.Core;

namespace Hackaton.EventStore
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            //Console.WriteLine("Setup EventStore");
            //EventStoreLoader.SetupEventStore();

            Console.WriteLine("Create actors model");
            ActorSystem.Create("ras").WhenTerminated.Wait();
        }
    }
}
