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
        public async Task PostCabRequestAsync(RequestInfo requestInfo)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "p_EmpId", requestInfo.EmpId.ToString()} ,
                { "p_EmpName", requestInfo.EmpName} ,
                { "p_RequestDate",  requestInfo.RequestDate},
                { "p_PickupTime", requestInfo.PickUpTime},
                { "p_PickupPoint",  requestInfo.PickUpPoint},
                { "p_Area", requestInfo.Area},
                { "p_DropAddress", requestInfo.DropAddress},
            };
           await _databaseService.ExecuteStoredProcAsync<Task>(StoredProcedures.GetRequestCab, parameters);
        }
        /// <summary>
        /// Returns Cab request details
        /// </summary>
        /// <param name="pickUpPoint"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RequestInfo>> GetCabRequestsAsync()
        {
            /*Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "P_PickUpPoint", pickUpPoint }
                };
            */
            return await _databaseService.ExecuteStoredProcWithNoParamsAsync<RequestInfo>(StoredProcedures.GetViewCabRequests );
        }

    }
}
