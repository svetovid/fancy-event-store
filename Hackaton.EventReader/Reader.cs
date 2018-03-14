using System;
using System.Text;
using Akka.Actor;
using Hackathon.BusinessLogic;
using Hackathon.EventStore.Core;
using Hackaton.Shared;
using Hackaton.Shared.Messages;
using Newtonsoft.Json;

namespace Hackaton.EventReader
{
    public class Reader : ReceiveActor
    {
        public Reader()
        {
            var eventWriter = new EventWriter("CounterResults");

            Receive<Shared.Event>(message =>
            {
                Console.WriteLine($"Message received by Reader {Self}");
                string paymentReference;
                switch ((MessageType)message.MessageType)
                {
                    case MessageType.ApprovalStatusChanged:
                        paymentReference = JsonConvert.DeserializeObject<PaymentApprovalStatusChangedMessage>(message.Message).PaymentReference;
                        Console.WriteLine($"**ApprovalStatusChanged Event received for payment {paymentReference}");
                        break;
                    case MessageType.PaymentStatusChanged:
                        var pscmessage = JsonConvert.DeserializeObject<PaymentStatusChangedMessage>(message.Message);
                        var calculator = new CustomerPaymentCounterCalculator();
                        var result = calculator.GetPaymentChanges(pscmessage);

                        var eReader1 = new Hackathon.EventStore.Core.EventReader($"evt-{pscmessage.Payment.PaymentReference}");
                        var allMessages = eReader1.ReadAllMessages(pscmessage.Payment.PaymentReference);

                        var counterResult = new CounterResult
                        {
                            CustomerReference = pscmessage.Customer.CustomerReference,
                            PaymentReference = pscmessage.Payment.PaymentReference,
                            Increment = result
                        };

                        eventWriter.WriteEvent(counterResult.AsJsonString(), "IncrementCalculated");
                        Console.WriteLine($"**Event received for payment {pscmessage.Payment.PaymentReference}");
                        break;
                    case MessageType.CustomerAccountChanged:
                        paymentReference = JsonConvert.DeserializeObject<PaymentCustomerAccountChangedMessage>(message.Message).PaymentReference;
                        Console.WriteLine($"**CustomerAccountChanged Event received for payment {paymentReference}");
                        break;
                    case MessageType.WithdrawalChannelChanged:
                        paymentReference = JsonConvert.DeserializeObject<WithdrawalChannelChangedMessage>(message.Message).PaymentReference;
                        Console.WriteLine($"**WithdrawalChannelChanged Event received for payment {paymentReference}");
                        break;
                    default:
                        Console.WriteLine("arrrrr");
                        return;
                }
            });
        }
    }
}
