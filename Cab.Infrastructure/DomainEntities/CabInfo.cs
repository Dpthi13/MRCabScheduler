using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Infrastructure.DomainEntities
{
    public class CabInfo
    {
        public int CabId { get; set; }
        public string RequestId { get; set; }
        public string DriverName { get; set; }
        public string CabNumber { get; set; }
        public string DriverPhoneNo { get; set; }
        public string PickUpTime { get; set; }
        public string PickUpDate { get; set; }
        public string DropAddress { get; set; }
    }
}
