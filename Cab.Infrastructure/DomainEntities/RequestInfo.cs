using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.DomainEntities
{
    public class RequestInfo
    {
        public string RequestId { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string RequestDate { get; set; }
        public string PickUpTime { get; set; }
        public string PickUpPoint { get; set; }
        public string Area { get; set; }
        public string DropAddress { get; set; }
    }
}
