using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cab.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [ApiController]
    public class RequestCabController : ControllerBase
    {
        private readonly IRequestCabService _requestCabService;

        public RequestCabController(IHttpContextAccessor contextAccessor, IRequestCabService requestCabService)
        {
            _requestCabService = requestCabService;
        }
        /// <summary>
        /// Processes  the cab request
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Request")]
        public async Task<IActionResult> RequestCabAsync([FromBody] RequestInfo requestInfo)
        {
            await _requestCabService.RequestCabAsync(requestInfo);
            return NoContent();
        }
        /// <summary>
        /// returns cab request details
        /// </summary>
        /// <param name="pickUpPoint"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ViewCabRequest")]
        public async Task<IEnumerable<RequestInfo>> ViewCabRequestAsync(string pickUpPoint)
        {
            return await _requestCabService.ViewCabRequestsAsync(pickUpPoint);
        }
    }
}
