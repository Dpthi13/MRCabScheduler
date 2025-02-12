using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.Interfaces
{
    public interface IDatabaseService
    {
        Task<T> ExecuteStoredProcAsync <T>(string procName, Dictionary<string, string> procParams);
        Task<IEnumerable<T>> ExecuteStoredProcWithNoParamsAsync<T>(string procName);
        Task<IEnumerable<T>> ExecuteStoredProcListAsync<T>(string procName, Dictionary<string, string> procParams);
    }
}
