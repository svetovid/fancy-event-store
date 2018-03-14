namespace Hackaton.Shared.Messages
{
    public class IRDetails
    {
        public IRDetails(string irScoreProvider, int bin)
        {
            IRScoreProvider = irScoreProvider;
            Bin = bin;
        }

        public string IRScoreProvider { get; }
        public int Bin { get; }
    }
}