using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Helpers;
using Cab.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cab.Service
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<CabAppSettings> _settings;

        public TokenService(IOptions<CabAppSettings> settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public string CreateToken(UserInfo user)
        {
            var claims = new List<Claim>
            {
                new Claim("empId", string.IsNullOrEmpty(user.EmpId) ? "": user.EmpId),
                new Claim("empName", string.IsNullOrEmpty(user.EmpName) ? "": user.EmpName),
                new Claim("email", string.IsNullOrEmpty(user.Email) ? "" : user.Email),
                new Claim("role", string.IsNullOrEmpty(user.RoleName) ? "": user.RoleName)
            };

            var token = new JwtSecurityToken(
                issuer: _settings.Value.Jwt.Issuer,
                audience: _settings.Value.Jwt.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(TimeSpan.Parse(_settings.Value.Jwt.Validity).TotalSeconds),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Jwt.Key)), SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
