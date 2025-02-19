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

        public AuthController(IHttpContextAccessor contextAccessor, IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        /// <summary>
        /// Get empoyee's information
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public async Task<UserInfo> GetUserInfoAsync(string empId)
        {
            return await _userManagementService.GetUserInformationAsync(empId);
        }
        /// <summary>
        /// Register new employee in system
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistration userRegistration)
        {
            await _userManagementService.RegisterUserAsync(userRegistration);
            return NoContent();
        
        }
        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new Exception("This is a test exception.");
        }
        [HttpGet("test-app-exception")]
        public IActionResult TestApplicationException()
        {
            throw new ApplicationException("Invalid Token");
        }

    }
}
