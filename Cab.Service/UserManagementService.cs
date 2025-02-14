using Cab.Infrastructure.Constants;
using Cab.Infrastructure.Interfaces;
using UserInfo = Cab.Infrastructure.DomainEntities.UserInfo;

namespace Cab.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ITokenService _tokenService;
        private readonly IDatabaseService _databaseService;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public UserManagementService(ITokenService tokenService, IDatabaseService databaseService)
        {
            _tokenService = tokenService;
            _databaseService = databaseService;
        }

        public async Task<UserInfo> GetUserInformationAsync(string empId)
        {
            if (parameters.Count > 0) parameters.Clear();
            parameters.Add("P_EmpId", empId);
            var userInfo = await _databaseService.ExecuteStoredProcAsync<UserInfo>(StoredProcedures.GetUserInformation, parameters);

            if (userInfo == null)
                return null;

            userInfo.EmpId = empId;
            userInfo.AccessToken = _tokenService.CreateToken(userInfo);
            return userInfo;
        }
    }
}
