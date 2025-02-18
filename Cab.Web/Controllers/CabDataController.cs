using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Interfaces;
using Cab.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Cab.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [ApiController]
    public class CabDataController : ControllerBase
    {
        private readonly ICabDataService _cabDataService;

        public CabDataController(IHttpContextAccessor contextAccessor, ICabDataService cabDataService)
        {
            _cabDataService = cabDataService;
        }
        /// <summary>
        /// Updates the cab information
        /// </summary>
        /// <param name="cabInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpsertCabData")]
        public async Task<IActionResult> UpsertCabDataAsync([FromBody] CabInfo cabInfo)
        {
            await _cabDataService.UpsertCabDataAsync(cabInfo);
            return NoContent();
        }
        /// <summary>
        /// returns the cab details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ViewCabDetails")]
        public async Task<IEnumerable<CabInfo>> ViewCabDetailsAsync()
        {
            return await _cabDataService.ViewCabDetailsAsync();
        }

    }
}
