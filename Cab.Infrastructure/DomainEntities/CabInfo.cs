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
        public string DriverName { get; set; }
        public string CabNumber { get; set; }
        public string DriverPhoneNo { get; set; }
        public TimeSpan PickupTime { get; set; }
        public DateTime PickUpDate { get; set; }
        public string DropAddress { get; set; }
    }
}
