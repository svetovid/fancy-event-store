using System;
using System.Text;
using Hackathon.BusinessLogic;
using Hackathon.EventStore.Core;
using Hackaton.Shared.Messages;

namespace Hackathon.EventStore.Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            EventStoreLoader.SetupEventStore();
            var eventReader = new EventReader("Payments");
            var eventWriter = new EventWriter("CounterResults");

            eventReader.ConnectToPersistentSubscription("Payments", "Payments_Counter", (_, e) =>
            {
                var data = Encoding.ASCII.GetString(e.Event.Data);
                switch (e.Event.EventType)
                {
                    case "PaymentStatusChanged":
                        var pscmessage = data.ParseJson<PaymentStatusChangedMessage>();
                        var calculator = new CustomerPaymentCounterCalculator();
                        var result = calculator.GetPaymentChanges(pscmessage);
                        var eReader1 = new EventReader($"evt-{pscmessage.Payment.PaymentReference}");
                        var allMessages = eReader1.ReadAllMessages(pscmessage.Payment.PaymentReference);
                        var counterResult = new CounterResult {CustomerReference = pscmessage.Customer.CustomerReference, PaymentReference = pscmessage.Payment.PaymentReference, Increment = result } ;
                        eventWriter.WriteEvent(counterResult.AsJsonString(), "IncrementCalculated");
                        Console.WriteLine($"**Event received for payment {pscmessage.Payment.PaymentReference}");
                        break;
                    case "ApprovalStatusChanged":
                        var asmessage = data.ParseJson<PaymentApprovalStatusChangedMessage>();
                        break;
                    case "CustomerAccountChanged":
                        var camessage = data.ParseJson<PaymentCustomerAccountChangedMessage>();
                        break;
                    case "WithdrawalChannelChanged":
                        var wcmessage = data.ParseJson<WithdrawalChannelChangedMessage>();
                        break;
                    default:
                        Console.WriteLine("Cannot parse event type");
                        return;
                }

            });

            Console.ReadLine();
        }
    }
}
