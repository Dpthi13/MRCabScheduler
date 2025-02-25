using Cab.Infrastructure.DomainEntities;

namespace Cab.Infrastructure.Interfaces
{
    public interface IUserManagementService
    {
        Task<UserInfo> GetUserInformationAsync(string empId);
        Task<(string EmpId, string AccessToken, string RoleName)?> AuthenticateUserAsync(string empId, string password);
        Task RegisterUserAsync(UserRegistration userRegistration);
    }
}
