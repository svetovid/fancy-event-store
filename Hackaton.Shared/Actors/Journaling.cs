using System;
using Akka.Actor;
using Hackaton.Shared.Messages;

namespace Hackaton.Shared.Actors
{
    public class Journaling : ReceiveActor
    {
        public Journaling(IActorRef persistor)
        {
            Receive<string>(m => { Console.WriteLine(m); });

            Receive<PaymentStatusChangedMessage>(message =>
            {
                Console.WriteLine("PaymentStatusChangedMessage has been received");

                persistor.Tell(message);
            });

            Receive<PaymentApprovalStatusChangedMessage>(message =>
            {
                Console.WriteLine("PaymentApprovalStatusChangedMessage has been received");

                persistor.Tell(message);
            });

            Receive<WithdrawalChannelChangedMessage>(message =>
            {
                Console.WriteLine("WithdrawalChannelChangedMessage has been received");

                persistor.Tell(message);
            });

            Receive<PaymentCustomerAccountChangedMessage>(message =>
            {
                Console.WriteLine("PaymentCustomerAccountChangedMessage has been received");

                persistor.Tell(message);
            });
        }
    }
}