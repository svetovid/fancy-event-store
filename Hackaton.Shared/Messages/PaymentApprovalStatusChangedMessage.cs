using System.Collections.Generic;

namespace Hackaton.Shared.Messages
{
    public class PaymentApprovalStatusChangedMessage
    {
        public PaymentApprovalStatusChangedMessage(long paymentId, string paymentReference, string siteName,
            string paymentMethodName, PaymentStatusHistory statusHistory, int methodActionId,
            List<ApprovalDetails> approvalStatusHistory, string requestedByFromAdminPanel, bool isReconciled)
        {
            PaymentId = paymentId;
            PaymentReference = paymentReference;
            SiteName = siteName;
            PaymentMethodName = paymentMethodName;
            StatusHistory = statusHistory;
            MethodActionId = methodActionId;
            ApprovalStatusHistory = approvalStatusHistory;
            RequestedByFromAdminPanel = requestedByFromAdminPanel;
            IsReconciled = isReconciled;
        }

        public long PaymentId { get; }
        public string PaymentReference { get; }
        public string SiteName { get; }
        public string PaymentMethodName { get; }
        public PaymentStatusHistory StatusHistory { get; }
        public int MethodActionId { get; }
        public List<ApprovalDetails> ApprovalStatusHistory { get; }
        public string RequestedByFromAdminPanel { get; }
        public bool IsReconciled { get; }
    }
}