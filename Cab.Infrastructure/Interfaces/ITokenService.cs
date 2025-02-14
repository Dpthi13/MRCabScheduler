using Cab.Infrastructure.DomainEntities;

namespace Cab.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserInfo user);
    }
}
