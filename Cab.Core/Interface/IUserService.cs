using Cab.Core.Domain_Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Core.Interface
{
    public interface IUserService
    {
        Task<User?> LoginAsync(int empId, string password);
    }
}
