using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
//using Hackathon.EventStore.Core;

namespace Hackaton.EventReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Setup EventStore");
            EventStoreLoader.SetupEventStore();

            ActorSystem.Create("ras").WhenTerminated.Wait();
        }
    }
}
