using Akka.Actor;
using Akka.Routing;

namespace Hackaton.Journaling
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var system = ActorSystem.Create("ras");

            var persistorPool = system.ActorOf(Props.Create<EventStore.Persistor>().WithRouter(FromConfig.Instance), "persistor");
            system.ActorOf(Props.Create(() => new Api(persistorPool)), "api");

            system.WhenTerminated.Wait();
        }
    }
}