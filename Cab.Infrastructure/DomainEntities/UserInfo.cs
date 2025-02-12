using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.DomainEntities
{
    public class UserInfo
    {
        public string EmpId {  get; set; }
        public string EmpName {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken {  get; set; }
        public string RoleName {  get; set; }
    }
}
