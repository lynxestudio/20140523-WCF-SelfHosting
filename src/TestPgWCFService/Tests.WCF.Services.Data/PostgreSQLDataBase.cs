using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;

namespace Tests.WCF.Services.Data
{
    internal sealed class PostgreSQLDataBase
    {
        static NpgsqlConnection _conn = null;
        static PostgreSQLDataBase Instance = null;
        string ConnectionString = null;
        private PostgreSQLDataBase()
        {
                if (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["ProductsDB"].ConnectionString))
                    ConnectionString = ConfigurationManager.ConnectionStrings["ProductsDB"].ConnectionString;
                else
                    throw new ApplicationException("ConnectionString not found.");
        }
        private static void CreateInstance()
        {
            if (Instance == null)
            { Instance = new PostgreSQLDataBase(); }
        }
        public static PostgreSQLDataBase GetInstance()
        {
            if (Instance == null)
                CreateInstance();
            return Instance;
        }
        public NpgsqlConnection GetConnection()
        {
                _conn = new NpgsqlConnection(ConnectionString);
                _conn.Open();
                return _conn;
        }
    }
}
