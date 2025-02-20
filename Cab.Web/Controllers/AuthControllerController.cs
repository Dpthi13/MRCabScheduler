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
        /// Authenticates a user based on the provided employee ID and password.
        /// Returns an access token if authentication is successful; otherwise, returns an unauthorized response.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest loginRequest)
        {
            var userTokenData = await _userManagementService.AuthenticateUserAsync(loginRequest.EmpId, loginRequest.Password);

            if (userTokenData == null)
                return Unauthorized(new { message = "Invalid employee ID or password" });

            return Ok(new
            {
                empId = userTokenData.Value.EmpId,
                accessToken = userTokenData.Value.AccessToken
            });
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
    }
}