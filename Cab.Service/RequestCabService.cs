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

    public class RequestCabService : IRequestCabService
    {
        private readonly IDatabaseService _databaseService;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public RequestCabService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        /// <summary>
        /// Processes  the cab request
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        public async Task RequestCabAsync(RequestInfo requestInfo)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "p_EmpId", requestInfo.EmpId.ToString()} ,
                { "P_RequestDate",  requestInfo.RequestDate.ToString("yyyy-MM-dd HH:mm:ss")},
                { "P_PickUpTime", requestInfo.PickUpTime.ToString()},
                { "P_PickUpPoint",  requestInfo.PickUpPoint},
                { "P_Area", requestInfo.Area},
                { "P_DropAddress", requestInfo.DropAddress},
            };
           await _databaseService.ExecuteStoredProcAsync<RequestInfo>(StoredProcedures.GetRequestCab, parameters);
        }
        /// <summary>
        /// Returns Cab request details
        /// </summary>
        /// <param name="pickUpPoint"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RequestInfo>> ViewCabRequestsAsync(string pickUpPoint)
        {
            var parameters = new Dictionary<string, string>
                {
                    { "P_PickUpPoint", pickUpPoint }
                };

            return await _databaseService.ExecuteStoredProcListAsync<RequestInfo>(StoredProcedures.GetViewCabRequests, parameters);
        }

    }
}
