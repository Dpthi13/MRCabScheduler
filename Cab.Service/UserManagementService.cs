using Cab.Infrastructure.Constants;
using Cab.Infrastructure.DomainEntities;
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
        /// <summary>
        /// Get empoyee's information
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>

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
        /// <summary>
        /// Register new empployee in system
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns></returns>
        public async Task RegisterUserAsync(UserRegistration userRegistration)
        {
            if (parameters.Count > 0) parameters.Clear();
            parameters.Add("p_EmpId", userRegistration.EmpId.ToString());
            parameters.Add("P_EmpName", userRegistration.EmpName);
            parameters.Add("P_Address", userRegistration.Address);
            parameters.Add("P_EmpPhone", userRegistration.EmpPhone);
            parameters.Add("P_EmailId", userRegistration.EmailId);
            parameters.Add("P_Password", userRegistration.Password);

            await _databaseService.ExecuteStoredProcAsync<UserRegistration>(StoredProcedures.GetRegisterUser, parameters);
        }
    }
}
