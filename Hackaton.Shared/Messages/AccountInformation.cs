namespace Hackaton.Shared.Messages
{
    public class AccountInformation
    {
        public AccountInformation(int accountId, bool isDummy, string displayedAccountIdentifier)
        {
            AccountId = accountId;
            IsDummy = isDummy;
            DisplayedAccountIdentifier = displayedAccountIdentifier;
        }

        public int AccountId { get; }
        public bool IsDummy { get; }
        public string DisplayedAccountIdentifier { get; }
    }
}