using Cab.Infrastructure.Helpers;
using Cab.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;


namespace Cab.Infrastructure.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        private readonly ILogger<DatabaseService> _logger;
        private readonly int _timeout;

        public DatabaseService(IOptions<CabAppSettings> settings, ILogger<DatabaseService> logger)
        {
            _logger = logger;
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
                //_timeout = settings.Value.ConnectionStrings.ConnectionTimeout;
            }
            _connectionString = settings.Value.ConnectionStrings.CabDatabase;

        }
        public async Task<T> ExecuteStoredProcAsync<T>(string procName, Dictionary<string, string> procParams)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                if (procParams != null)
                {
                    DapperHelper.MapProperties(procParams, parameters);
                }
                var result = await connection.QueryAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 180);
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcListAsync<T>(string procName, Dictionary<string, string> procParams)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                if (procParams != null)
                {
                    DapperHelper.MapProperties(procParams, parameters);
                }
                var result = await connection.QueryAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 180);
                return result;
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcWithNoParamsAsync<T>(string procName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(procName, null, commandType: CommandType.StoredProcedure, commandTimeout: 180);
                return result;
            }
        }

        //Executes a SELECT query that retrieves multiple rows. 
        public async Task<IEnumerable<T>> GetAllAsync<T>(string query, object parameters)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        //Executes a SELECT query that retrieves all records without using parameters
        public async Task<IEnumerable<T>> GetAllAsync<T>(string query)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(query);
            }
        }

        //Executes a SELECT query that is expected to return only one row.
        public async Task<T> GetSingleAsync<T>(string query, object parameters)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
            }
        }

        //Executes INSERT, UPDATE, DELETE queries.It Does not return any data, returns only number of rows affected.(For eg: 1 or 2)
        public async Task<int> ExecuteNonQueryAsync(string query, object parameters)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(query, parameters, commandTimeout: 180);
            }
        }

    }
}
