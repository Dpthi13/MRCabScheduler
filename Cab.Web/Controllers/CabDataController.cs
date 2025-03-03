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
            return Ok(new { message = "Cab assigned successfully!" }); 
        }

        /// <summary>
        /// returns the cab details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ViewCabDetails")]
        public async Task<CabInfo> GetCabDetailsAsync(int empId)
        {
            return await _cabDataService.GetCabDetailsAsync(empId);
        }

    }
}
