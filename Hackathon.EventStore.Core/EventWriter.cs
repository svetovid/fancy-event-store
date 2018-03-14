using System;
using System.Text;
using EventStore.ClientAPI;

namespace Hackathon.EventStore.Core
{
    public class EventWriter
    {
        private readonly string _streamName;

        public long? Checkpoint { get; private set; }

        public EventWriter(string streamName)
        {
            _streamName = streamName;
        }

        public void WriteEvent(string message, string messageType )
        {
            var r = EventStoreLoader.Connection.AppendToStreamAsync(
                _streamName,
                Checkpoint ?? ExpectedVersion.Any,
                new EventData(
                    Guid.NewGuid(),
                    messageType,
                    true,
                    Encoding.UTF8.GetBytes(message),
                    new byte[] { }
                )
            );

            Checkpoint = r.Result.NextExpectedVersion;
        }
    }
}
