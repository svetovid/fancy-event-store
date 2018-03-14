using System.Collections.Generic;

namespace Hackaton.Shared.Messages
{
    public class PaymentStatusChangedMessage
    {
        public PaymentStatusChangedMessage(string siteName, PaymentInformation payment,
            CustomerAccountInformation customerAccount, AccountInformation account, CustomerInformation customer,
            List<PaymentStatusHistory> statusHistory, ApprovalDetails approvalDetails, ProviderAccount providerAccount,
            IRDetails irDetails)
        {
            SiteName = siteName;
            Payment = payment;
            CustomerAccount = customerAccount;
            Account = account;
            Customer = customer;
            StatusHistory = statusHistory;
            ApprovalDetails = approvalDetails;
            ProviderAccount = providerAccount;
            IrDetails = irDetails;
        }

        public string SiteName { get; }
        public PaymentInformation Payment { get; }
        public CustomerAccountInformation CustomerAccount { get; }
        public AccountInformation Account { get; }
        public CustomerInformation Customer { get; }
        public List<PaymentStatusHistory> StatusHistory { get; }
        public ApprovalDetails ApprovalDetails { get; }
        public ProviderAccount ProviderAccount { get; }
        public IRDetails IrDetails { get; }
    }
}