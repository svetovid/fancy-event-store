namespace Hackaton.Shared.Messages
{
    public class CustomerAccountInformation
    {
        public CustomerAccountInformation(int customerAccountId, string paymentMethod, string currencyCode)
        {
            CustomerAccountId = customerAccountId;
            PaymentMethod = paymentMethod;
            CurrencyCode = currencyCode;
        }

        public int CustomerAccountId { get; }
        public string PaymentMethod { get; }
        public string CurrencyCode { get; }
    }
}