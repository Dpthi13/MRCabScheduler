using Cab.Infrastructure.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.Interfaces
{
    public interface IUserManagementService
    {
        Task<UserInfo> GetUserInformationAsync(string empId);
    }
}
