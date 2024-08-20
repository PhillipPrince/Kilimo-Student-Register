using System.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Kilimo.DBOperations
{
    
        public class SqlConnectionManager
        {
            private readonly string _connectionString;

            public SqlConnectionManager(string connectionString)
            {
                _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            }

            public IDbConnection GetConnection()
            {
                IDbConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }

            public void CloseConnection(IDbConnection connection)
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            // You can add methods for executing queries, handling transactions, etc.
            // For example:
            public IDbCommand CreateCommand(string query, IDbConnection connection)
            {
                var command = connection.CreateCommand();
                command.CommandText = query;
                return command;
            }

            // Execute a non-query command (e.g., INSERT, UPDATE, DELETE)
            public int ExecuteNonQuery(string query, IDbConnection connection)
            {
                using (var command = CreateCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }

            // Execute a query and return a data reader
            public IDataReader ExecuteReader(string query, IDbConnection connection)
            {
                using (var command = CreateCommand(query, connection))
                {
                    return command.ExecuteReader();
                }
            }


        }
    }
