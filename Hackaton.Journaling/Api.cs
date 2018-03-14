using System;
using Akka.Actor;

namespace Hackaton.Journaling
{
    public class Api : ReceiveActor
    {
        public Api(IActorRef persistor)
        {
            Receive<string>(m => { Console.WriteLine(m); });

            Receive<Shared.Event>(message =>
            {
                Console.WriteLine($"Event has been received by {Self}");

                persistor.Tell(message);
            });

            Receive<Shared.Messages.PaymentStatusChangedMessage>(message =>
            {
                Console.WriteLine("PaymentStatusChangedMessage has been received");

                persistor.Tell(message);
            });

            Receive<Shared.Messages.PaymentApprovalStatusChangedMessage>(message =>
            {
                Console.WriteLine("PaymentApprovalStatusChangedMessage has been received");

                persistor.Tell(message);
            });

            Receive<Shared.Messages.WithdrawalChannelChangedMessage>(message =>
            {
                Console.WriteLine("WithdrawalChannelChangedMessage has been received");

                persistor.Tell(message);
            });

            Receive<Shared.Messages.PaymentCustomerAccountChangedMessage>(message =>
            {
                Console.WriteLine("PaymentCustomerAccountChangedMessage has been received");

                persistor.Tell(message);
            });
        }
    }
}
