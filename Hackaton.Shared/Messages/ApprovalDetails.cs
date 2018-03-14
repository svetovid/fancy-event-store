using System;

namespace Hackaton.Shared.Messages
{
    public class ApprovalDetails
    {
        public ApprovalDetails(long paymentApprovalStatusHistoryId, ApprovalStatus approvalStatus, int? refuseReasonId,
            DateTime changed, string setBy)
        {
            PaymentApprovalStatusHistoryId = paymentApprovalStatusHistoryId;
            ApprovalStatus = approvalStatus;
            RefuseReasonId = refuseReasonId;
            Changed = changed;
            SetBy = setBy;
        }

        public long PaymentApprovalStatusHistoryId { get; }
        public ApprovalStatus ApprovalStatus { get; }
        public int? RefuseReasonId { get; }
        public DateTime Changed { get; }
        public string SetBy { get; }
    }
}