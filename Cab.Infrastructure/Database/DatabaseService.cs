using Dapper;
using Cab.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Cab.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Helper_Oracle;
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
            if(settings == null )
            {
                throw new ArgumentNullException(nameof(settings));
                _connectionString = settings.Value.ConnectionStrings.CabDatabase;
                //_timeout = settings.Value.ConnectionStrings.ConnectionTimeout;
            }
        }
        public async Task<T> ExecuteStoredProcAsync<T>(string procName, Dictionary<string, string> procParams)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                if (procParams != null)
                {
                    DapperHelper.MapProperties(procParams, parameters);
                }
                var result = await sqlConnection.QueryAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcListAsync<T>(string procName, Dictionary<string, string> procParams)
        {
            using (var sqlConnection = new SqlConnection(_connectionString)) 
            { 
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                if(procParams != null)
                {
                    DapperHelper.MapProperties(procParams , parameters);
                }
                var result = await sqlConnection.QueryAsync<T>(procName, parameters, commandType : CommandType.StoredProcedure, commandTimeout : _timeout).ConfigureAwait(false);
                return result;
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcWithNoParamsAsync<T>(string procName)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                var result = await sqlConnection.QueryAsync<T>(procName, null, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).ConfigureAwait(false);
                return result;
            }
        }
    }
}
