using Cab.Infrastructure.DomainEntities;

namespace Cab.Infrastructure.Interfaces
{
    public interface IUserManagementService
    {
        Task<UserInfo> GetUserInformationAsync(string empId);
        Task RegisterUserAsync(UserRegistration userRegistration);
    }
}
