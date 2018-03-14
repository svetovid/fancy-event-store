using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Hackathon.EventStore.Core
{
    public class EventReader
    {
        private readonly string _streamName;

        public long? Checkpoint { get; private set; }

        //private readonly IEventStoreConnection _connection;
        //private readonly IKnownEventsProvider _knownEventsProvider;

        public EventReader(string streamName /*IKnownEventsProvider knownEventsProvider*/)
        {
            _streamName = streamName;
            Checkpoint = null;
        }

        public List<Hackaton.Shared.Event> ReadAllMessages(string paymentReference)
        {
            var length = EventStoreLoader.Connection.GetStreamMetadataAsync($"evt-{paymentReference}", EventStoreCredentials.Default)
                .Result.Stream.Length;
            var result = EventStoreLoader.Connection
                .ReadStreamEventsForwardAsync($"evt-{paymentReference}", 0, length, true, EventStoreCredentials.Default)
                .Result.Events;
            return result.Select(r => new Hackaton.Shared.Event(Encoding.UTF8.GetString(r.Event.Data),
                (int) Enum.Parse(typeof(Hackaton.Shared.MessageType), r.Event.EventType))).ToList();
        }

        public void StartReading()
        {
            var settings = new CatchUpSubscriptionSettings(500, 500, true, true);
            EventStoreLoader.Connection.SubscribeToStreamFrom(_streamName, Checkpoint, settings, Appeared, subscriptionDropped: Dropped);
        }

        public void ConnectToPersistentSubscription(string stream, string group, Action<EventStorePersistentSubscriptionBase, ResolvedEvent> action)
        {
            EventStoreLoader.Connection.ConnectToPersistentSubscription(stream, group, action);
        }

        private Task Appeared(EventStoreCatchUpSubscription data, ResolvedEvent evt)
        {
            var recordedEvent = evt.Event;
            if (IsSystemStream(recordedEvent.EventStreamId)) return Task.CompletedTask;

            var linkedStream = evt.Link?.EventStreamId;
            if (IsSystemStream(linkedStream)) return Task.CompletedTask;

            var checkpoint = evt.Event.EventNumber;
            Checkpoint = checkpoint;

            return null;
        }

        private static bool IsSystemStream(string linkedStream)
        {
            return linkedStream != null && linkedStream.StartsWith("$");
        }

        private void Dropped(EventStoreCatchUpSubscription sub, SubscriptionDropReason reason, Exception ex)
        {
            
        }
    }
}
