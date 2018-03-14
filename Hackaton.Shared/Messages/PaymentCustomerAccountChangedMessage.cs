namespace Hackaton.Shared.Messages
{
    public class PaymentCustomerAccountChangedMessage
    {
        public PaymentCustomerAccountChangedMessage(string publicPaymentId, string paymentReference, string siteName,
            CustomerInformation customer, AccountInformation account, CustomerAccountInformation customerAccount)
        {
            PublicPaymentId = publicPaymentId;
            PaymentReference = paymentReference;
            SiteName = siteName;
            Customer = customer;
            Account = account;
            CustomerAccount = customerAccount;
        }

        public string PublicPaymentId { get; }
        public string PaymentReference { get; }
        public string SiteName { get; }
        public CustomerInformation Customer { get; }
        public AccountInformation Account { get; }
        public CustomerAccountInformation CustomerAccount { get; }
    }
}