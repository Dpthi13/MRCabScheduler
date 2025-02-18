using Cab.Infrastructure.DomainEntities;
using Org.BouncyCastle.Asn1.Pkcs;
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
        Task<IEnumerable<CabInfo>> ViewCabDetailsAsync();

    }
}
