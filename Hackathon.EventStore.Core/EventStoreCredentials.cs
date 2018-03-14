using EventStore.ClientAPI.SystemData;

namespace Hackathon.EventStore.Core
{
    public class EventStoreCredentials
    {
        public static UserCredentials Default { get; } = new UserCredentials("admin", "changeit");
    }
}
