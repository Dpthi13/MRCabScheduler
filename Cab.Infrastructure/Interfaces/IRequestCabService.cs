using Cab.Infrastructure.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.Interfaces
{
    public interface IRequestCabService
    {
        Task PostCabRequestAsync(RequestInfo requestInfo);
        Task<IEnumerable<RequestInfo>> GetCabRequestsAsync();
    }
}
