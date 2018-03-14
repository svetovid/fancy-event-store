using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Linq;
using Akka.Actor;
using Akka.Cluster;

namespace Hackaton.EventStore.Utils
{
    public class AutomaticCluster
    {
        private readonly ActorSystem _system;
        private readonly Cluster _cluster;
        //private readonly Sql _persistence;
        private readonly DbHelper _dbHelper;

        public AutomaticCluster(ActorSystem system)
        {
            _system = system;
            _cluster = Cluster.Get(system);
            //_persistence = SqlitePersistence.Get(system);
            _dbHelper = new DbHelper(() =>
            {
                var str = _system.Settings.Config.GetString("akka.persistence.journal.sql-server.connection-string");
                var conn = new SqlConnection(str);
                conn.Open();
                return conn;
            });
        }

        public void Join()
        {
            _dbHelper.InitializeNodesTable();

            var members = _dbHelper.GetClusterMembers().ToImmutableList();
            if (members.Any())
            {
                _cluster.JoinSeedNodes(members);
                _dbHelper.AddClusterMember(_cluster.SelfAddress);
            }
            else
            {
                var self = _cluster.SelfAddress;
                _dbHelper.AddClusterMember(self);
                _cluster.JoinSeedNodes(ImmutableList.Create(self));
            }
        }

        public void Leave()
        {
            _dbHelper.RemoveClusterMember(_cluster.SelfAddress);
        }
    }
}
