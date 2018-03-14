namespace Hackaton.Shared
{
    public class Event
    {
        public Event(string message, int messageType)
        {
            ReferenceKey = referenceKey;
            Message = message;
            MessageType = messageType;
        }

        public string ReferenceKey { get;}

        public string Message { get; }

        public int MessageType { get; }
    }
}
