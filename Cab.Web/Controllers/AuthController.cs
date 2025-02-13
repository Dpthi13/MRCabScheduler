using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Cab.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public AuthController(IHttpContextAccessor contextAccessor,  IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        [HttpGet]
        [Route("Login")]
        public async Task<UserInfo> GetUserInfoAsync(string empId)
        {
            return await _userManagementService.GetUserInformationAsync(empId);
        }
    }
}
