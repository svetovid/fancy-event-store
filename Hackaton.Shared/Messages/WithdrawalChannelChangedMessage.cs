using System.Collections.Generic;

namespace Hackaton.Shared.Messages
{
    public class WithdrawalChannelChangedMessage
    {
        public WithdrawalChannelChangedMessage(string paymentReference, string customerReference, string siteName,
            Channel currentChannel, List<WithdrawalChannelHistory> channelHistory)
        {
            PaymentReference = paymentReference;
            CustomerReference = customerReference;
            SiteName = siteName;
            CurrentChannel = currentChannel;
            ChannelHistory = channelHistory;
        }

        public string PaymentReference { get; }
        public string CustomerReference { get; }
        public string SiteName { get; }
        public Channel CurrentChannel { get; }
        public List<WithdrawalChannelHistory> ChannelHistory { get; }
    }
}