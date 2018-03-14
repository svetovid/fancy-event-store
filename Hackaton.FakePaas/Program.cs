using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Akka.Actor;
using Hackathon.EventStore.Core;
using Hackaton.Shared;

namespace Hackaton.FakePaas
{
    internal class Program
    {
        private const string ConnectionString =
            "Server=SRVUAPAYDB02.betsson.local;Database=wp_eventstore;Integrated Security=SSPI";

        private static ActorSelection _actorSelector;

        private static void Main(string[] args)
        {
            var pageSize = args != null && args.Length > 0 ? int.Parse(args[0] ?? "200") : 500;
            var pageCount = args != null && args.Length > 0 ? int.Parse(args[1] ?? "1000") : 3;

            var system = ActorSystem.Create("ras");
            _actorSelector = system.ActorSelection("akka.tcp://ras@127.0.0.1:5888/user/api");

            Console.WriteLine("Execution started at {0}", DateTime.Now.ToLongTimeString());
            //Console.WriteLine("Setup EventStore");
            //EventStoreLoader.SetupEventStore();
            //var eventWriter = new EventWriter("Payments");

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    for (var i = 0; i < pageCount; i++)
                    {
                        var res = ReadExistingEventsFromDbWithParams(pageSize, i, conn);
                        //res.ForEach(r => eventWriter.WriteEvent(r.Message, ((MessageType)r.MessageType).ToString()));
                        SendEvents(res);                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Execution completed at {0}", DateTime.Now.ToLongTimeString());
            Console.ReadLine();
        }

        private static List<Shared.Event> ReadExistingEventsFromDb(int pageSize, int batchId)
        {
            var list = new List<Shared.Event>();

            using (var cmd = new SqlCommand())
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.CommandText = $@"
                        SELECT [message], [messageTypeId] FROM teventMessage (NOLOCK) WHERE id > 222869
                        ORDER BY id
                        OFFSET     {pageSize * batchId} ROWS
                        FETCH NEXT {pageSize} ROWS ONLY;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                           list.Add(new Shared.Event(reader.GetSqlString(0).Value, reader.GetSqlString(1).Value, reader.GetSqlInt16(2).Value));
                    }
                }
            }

            return list;
        }

        private static List<Shared.Event> ReadExistingEventsFromDbWithParams(int pageSize, int batchId, SqlConnection conn)
        {
            var list = new List<Shared.Event>();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = $@"
                        SELECT [message], [messageTypeId] FROM teventMessage (NOLOCK)  WHERE id > 222869
                        ORDER BY id
                        OFFSET     {pageSize * batchId} ROWS
                        FETCH NEXT {pageSize} ROWS ONLY;";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(new Shared.Event(reader.GetSqlString(0).Value, reader.GetSqlString(1).Value, reader.GetSqlInt16(2).Value));
                }
            }

            return list;
        }

        private static void SendEvents(List<Shared.Event> events)
        {
            foreach (var @event in events)
            {
                try
                {
                    _actorSelector.Tell(@event);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
