namespace Cab.Infrastructure.Interfaces
{
    public interface IDatabaseService
    {
        Task<T> ExecuteStoredProcAsync<T>(string procName, Dictionary<string, string> procParams);
        Task<IEnumerable<T>> ExecuteStoredProcWithNoParamsAsync<T>(string procName);
        Task<IEnumerable<T>> ExecuteStoredProcListAsync<T>(string procName, Dictionary<string, string> procParams);
    }
}
