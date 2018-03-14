namespace Hackathon.BusinessLogic
{
    public class CounterResult
    {
        public string CustomerReference { get; set; }

        public string PaymentReference { get; set; }

        public PaymentStatisticIncrement Increment { get; set; }
    }
}
