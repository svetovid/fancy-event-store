namespace Hackaton.Shared
{
    internal partial class Program
    {
        public enum MessageType
        {
            PaymentStatusChanged = 1,
            ApprovalStatusChanged,
            WithdrawalChannelChanged,
            CustomerAccountChanged
        }
    }
}
