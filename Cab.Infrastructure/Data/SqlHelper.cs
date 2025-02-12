using MySql.Data.MySqlClient;
using System.Data;

namespace Cab.Infrastructure.Data
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<DataTable> ExecuteStoredProcedureAsync(string storedProcedureName, Dictionary<string, object> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(storedProcedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                }

                await conn.OpenAsync();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
            }
        }
    }
}
