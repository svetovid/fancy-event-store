using System;

namespace Hackaton.Shared.Messages
{
    public class WithdrawalChannelHistory
    {
        public WithdrawalChannelHistory(long withdrawalChannelHistoryId, Channel channel, DateTime created,
            string setBy)
        {
            WithdrawalChannelHistoryId = withdrawalChannelHistoryId;
            Channel = channel;
            Created = created;
            SetBy = setBy;
        }

        public long WithdrawalChannelHistoryId { get; }
        public Channel Channel { get; }
        public DateTime Created { get; }
        public string SetBy { get; }
    }
}