using Cab.Infrastructure.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.Interfaces
{
    public interface ICabDataService
    {
        Task UpsertCabDataAsync(CabInfo cabInfo);
        Task <CabInfo> GetCabDetailsAsync(int empId);

    }
}
