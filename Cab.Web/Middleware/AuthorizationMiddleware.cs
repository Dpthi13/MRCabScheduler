using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cab.Web.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public AuthorizationMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString();
            _logger.LogInformation("Token: " + token);

            if (httpContext.User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("Authenticated User: " + httpContext.User.Identity.Name);
            }
            else
            {
                _logger.LogWarning("User is not authenticated.");
            }

            await _next(httpContext);
        }


    }
}
