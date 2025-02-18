using Cab.Infrastructure.Constants;
using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Interfaces;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cab.Service
{
    public class CabDataService: ICabDataService
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
            if (parameters.Count > 0) parameters.Clear();
            parameters.Add("p_CabId", cabInfo.CabId.ToString());
            parameters.Add("p_DriverName", cabInfo.DriverName);
            parameters.Add("p_CabNumber", cabInfo.CabNumber);
            parameters.Add("p_DriverPhoneNo", cabInfo.DriverPhoneNo);
            parameters.Add("p_PickupTime", cabInfo.PickupTime.ToString(@"hh\:mm\:ss")); 
            parameters.Add("p_PickupDate", cabInfo.PickUpDate.ToString("yyyy-MM-dd")); 
            parameters.Add("p_DropAddress", cabInfo.DropAddress);
            
            //  for debugging
            //foreach (var param in parameters)
            //{
            //    Console.WriteLine($"{param.Key}: {param.Value}");
            //}

            await _databaseService.ExecuteStoredProcAsync<CabInfo>(StoredProcedures.GetUpsertCabData, parameters);
        }
        /// <summary>
        /// returns the cab details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CabInfo>> ViewCabDetailsAsync()
        {
            return await _databaseService.ExecuteStoredProcWithNoParamsAsync<CabInfo>(StoredProcedures.GetCabDetails);
        }

    }

}

