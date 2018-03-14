namespace Hackaton.Shared.Messages
{
    public class ProviderAccount
    {
        public ProviderAccount(int providerAccountId, string providerAccountName, string providerAccountAlias)
        {
            ProviderAccountId = providerAccountId;
            ProviderAccountName = providerAccountName;
            ProviderAccountAlias = providerAccountAlias;
        }

        public int ProviderAccountId { get; }
        public string ProviderAccountName { get; }
        public string ProviderAccountAlias { get; }
    }
}