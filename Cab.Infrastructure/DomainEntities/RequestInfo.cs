using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.DomainEntities
{
    public class RequestInfo
    {
        public int EmpId { get; set; }
        public DateTime RequestDate { get; set; }
        public TimeSpan PickUpTime { get; set; }
        public string PickUpPoint { get; set; }
        public string Area { get; set; }
        public string DropAddress { get; set; }
    }
}
