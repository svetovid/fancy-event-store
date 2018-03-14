using System;

namespace Hackaton.Shared.Messages
{
    public class PaymentStatusHistory
    {
        public PaymentStatusHistory(long paymentStatusHistoryId, string status, int paymentResponseCode,
            string responseText, DateTime created, string providerAccountName, string setBy,
            bool? walletBalanceAffected, int? providerAccountConfigId)
        {
            PaymentStatusHistoryId = paymentStatusHistoryId;
            Status = status;
            PaymentResponseCode = paymentResponseCode;
            ResponseText = responseText;
            Created = created;
            ProviderAccountName = providerAccountName;
            SetBy = setBy;
            WalletBalanceAffected = walletBalanceAffected;
            ProviderAccountConfigId = providerAccountConfigId;
        }

        public long PaymentStatusHistoryId { get; }
        public string Status { get; }
        public int PaymentResponseCode { get; }
        public string ResponseText { get; }
        public DateTime Created { get; }
        public string ProviderAccountName { get; }
        public string SetBy { get; }
        public bool? WalletBalanceAffected { get; }
        public int? ProviderAccountConfigId { get; }
    }
}