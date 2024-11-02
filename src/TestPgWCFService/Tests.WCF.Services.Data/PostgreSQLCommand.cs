using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Tests.WCF.Services.Data
{
    internal sealed class PostgreSQLCommand
    {
        internal static NpgsqlDataReader GetReader(string commandText,
            NpgsqlParameter[] parameters,
            System.Data.CommandType cmdtype)
        {
                NpgsqlDataReader resp = null;
                NpgsqlConnection conn = PostgreSQLDataBase.GetInstance().GetConnection();
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    resp = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                return resp;
        }
    }
}
