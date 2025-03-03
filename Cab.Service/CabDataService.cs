using Cab.Infrastructure.Constants;
using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab.Service
{
    public class CabDataService : ICabDataService
    {
        private readonly IDatabaseService _databaseService;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public CabDataService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        /// <summary>
        /// Updates the cab information
        /// </summary>
        /// <param name="cabInfo"></param>
        /// <returns></returns>
        public async Task UpsertCabDataAsync(CabInfo cabInfo)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "p_CabId", cabInfo.CabId.ToString() },
                { "p_RequestId", cabInfo.RequestId.ToString() },
                { "p_DriverName",  cabInfo.DriverName},
                { "p_CabNumber", cabInfo.CabNumber},
                { "p_DriverPhoneNo",  cabInfo.DriverPhoneNo},
                { "p_PickupTime",  cabInfo.PickUpTime},
                {"p_PickupDate", cabInfo.PickUpDate},
                { "p_DropAddress", cabInfo.DropAddress},
            };

            await _databaseService.ExecuteStoredProcAsync<Task>(StoredProcedures.GetUpsertCabData, parameters);
        }
        /// <summary>
        /// returns the cab details
        /// </summary>
        /// <returns></returns>
        public async Task<CabInfo> GetCabDetailsAsync(int empId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "p_EmpId", empId.ToString() }
                };

            return await _databaseService.ExecuteStoredProcAsync<CabInfo>(StoredProcedures.GetCabDetails, parameters);
        }
    }
}
