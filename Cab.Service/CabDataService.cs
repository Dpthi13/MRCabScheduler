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
                { "p_CabId", cabInfo.CabId.ToString()} ,
                { "p_DriverName",  cabInfo.DriverName},
                { "p_CabNumber", cabInfo.CabNumber},
                { "p_DriverPhoneNo",  cabInfo.DriverPhoneNo},
                { "p_PickupTime",  cabInfo.PickupTime.ToString(@"hh\:mm\:ss")},
                {"p_PickupDate", cabInfo.PickUpDate.ToString("yyyy-MM-dd")},
                { "P_DropAddress", cabInfo.DropAddress},
            };

            await _databaseService.ExecuteStoredProcAsync<CabInfo>(StoredProcedures.GetUpsertCabData, parameters);
        }
        /// <summary>
        /// returns the cab details
        /// </summary>
        /// <returns></returns>
        public async Task<CabInfo> GetCabDetailsAsync(int EmpId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "p_EmpId", EmpId.ToString() }
                };

            return await _databaseService.ExecuteStoredProcAsync<CabInfo>(StoredProcedures.GetCabDetails, parameters);
        }
    }
}
