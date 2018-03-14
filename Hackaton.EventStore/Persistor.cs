using System;
using System.Collections.Generic;
using Akka;
using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Persistence;
//using Hackathon.EventStore.Core;
using Hackaton.EventReader;
using Hackaton.Shared;
using Hackaton.Shared.Messages;
using Hackaton.EventStore.Utils;

namespace Hackaton.EventStore
{
    public class Persistor : UntypedPersistentActor
    {
        private List<PaymentStatusChangedMessage> _store = new List<PaymentStatusChangedMessage>();
        //private readonly EventWriter _eventWriter;
        private IActorRef _shardRegion;
        private AutomaticCluster _automicCluster;

        public Persistor()
        {
            //_eventWriter = new EventWriter("Payments");

            //_reader = Context.ActorOf(Props.Create<Reader>(), "eventreader");

            //_automicCluster = new AutomaticCluster(Context.System);
            //_automicCluster.Join();

            var sharding = ClusterSharding.Get(Context.System);
            _shardRegion = sharding.Start(
                typeName: "reader",
                entityProps: Props.Create<Reader>(),
                settings: ClusterShardingSettings.Create(Context.System),
                messageExtractor: new MessageExtractor(10));
        }

        //~Persistor()
        //{
        //    _automicCluster.Leave();
        //}

        protected override void OnCommand(object message)
        {
            message.Match().With<Shared.Event>(msg =>
            {
                Console.WriteLine($"PaymentStatusChangedMessage Recover Message has been received by Persistor {Self}");
                _eventWriter.WriteEvent(msg.Message, ((MessageType)msg.MessageType).ToString());
                _reader.Tell(msg);
            });
            Console.WriteLine("Command Message has been received");
        }

        protected override void OnRecover(object message)
        {
            message.Match()
                .With<PaymentStatusChangedMessage>(msg => _store.Add(msg))
                .With<SnapshotOffer>(offer =>
                {
                    Console.WriteLine("PaymentStatusChangedMessage Recover Message has been received within snapshots");

                    _store = (List<PaymentStatusChangedMessage>) offer.Snapshot;
                    Console.WriteLine($"Recovered state with {_store.Count} messages");
                });
            Console.WriteLine("Recover Message has been received");
        }

        public override string PersistenceId { get; } = "HackatonPersistor";
    }
}
