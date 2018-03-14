namespace Hackaton.Shared.Messages
{
    public enum ApprovalStatus
    {
        None = 0,
        ApprovalRequired = 1,
        Locked = 2,
        Approved = 4,
        Rejected = 8,
        ApprovedManually = 16,
        SecondApprovalRequired = 32,
        AccountInformationRequired = 64
    }
}