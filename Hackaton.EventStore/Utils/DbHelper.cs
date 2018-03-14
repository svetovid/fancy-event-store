using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Akka.Actor;

namespace Hackaton.EventStore.Utils
{
    public class DbHelper
    {
        private readonly Func<SqlConnection> _connectionFactory;

        public DbHelper(Func<SqlConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void InitializeNodesTable()
        {
            using (var cmd = new SqlCommand("", _connectionFactory()))
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cluster_nodes (
                    member_address VARCHAR(255) NOT NULL PRIMARY KEY
                );";

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Address> GetClusterMembers()
        {
            using (var cmd = new SqlCommand(@"SELECT member_address from cluster_nodes", _connectionFactory()))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var result = new List<Address>();
                    while (reader.Read())
                    {
                        var addr = reader.GetString(0);
                        result.Add(Address.Parse(addr));
                    }
                    return result;
                }
            }
        }

        public void AddClusterMember(Address address)
        {
            using (var cmd = new SqlCommand(@"INSERT INTO cluster_nodes(member_address) VALUES (@addr)", _connectionFactory()))
            using (var tx = cmd.Connection.BeginTransaction())
            {
                cmd.Transaction = tx;
                var addr = address.ToString();
                cmd.Parameters.Add("@addr", SqlDbType.Text).Value = addr;

                cmd.ExecuteNonQuery();
                tx.Commit();
            }
        }

        public void RemoveClusterMember(Address address)
        {
            using (var cmd = new SqlCommand(@"DELETE FROM cluster_nodes WHERE member_address = @addr", _connectionFactory()))
            using (var tx = cmd.Connection.BeginTransaction())
            {
                cmd.Transaction = tx;
                var addr = address.ToString();
                cmd.Parameters.Add("@addr", SqlDbType.Text).Value = addr;

                cmd.ExecuteNonQuery();
                tx.Commit();
            }
        }
    }
}
