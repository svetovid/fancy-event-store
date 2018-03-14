using System;
using Akka;
using Akka.Persistence;
using Hackaton.Shared.Messages;

namespace Hackaton.Shared.Actors
{
    public class Persistor : PersistentActor
    {
        protected override bool ReceiveRecover(object message)
        {
            message.Match().With<PaymentStatusChangedMessage>(msg => { Console.WriteLine("PaymentStatusChangedMessage Recover Message has been received"); });
            Console.WriteLine("Recover Message has been received");
            //Console.WriteLine(JsonConvert.SerializeObject(message));
            return true;
        }

        protected override bool ReceiveCommand(object message)
        {
            message.Match().With<PaymentStatusChangedMessage>(msg => { Console.WriteLine("PaymentStatusChangedMessage Command Message has been received"); });
            Console.WriteLine("Command Message has been received");
            //Console.WriteLine(JsonConvert.SerializeObject(message));
            return true;
        }

        public override string PersistenceId { get; } = "HackatonPersistor";
    }
}
