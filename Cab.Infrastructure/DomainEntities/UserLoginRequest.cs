using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.DomainEntities
{
    public class UserLoginRequest
    {
        public string EmpId { get; set; }
        public string Password { get; set; }
    }
}
