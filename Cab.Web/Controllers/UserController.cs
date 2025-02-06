using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cab.Core.Interface;
using Cab.Core.Domain_Enities;
namespace Cab.Web.Controllers
{
    [ApiController]
    [Route("api/user")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginRequest)
        {
            var user = await _userService.LoginAsync(loginRequest.EmpId, loginRequest.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(user);
        }
        /*[HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working!");
        }*/

    }
}
