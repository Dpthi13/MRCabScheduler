using Cab.Infrastructure.Constants;
using Cab.Infrastructure.DomainEntities;
using Cab.Infrastructure.Helpers;
using Cab.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using UserInfo = Cab.Infrastructure.DomainEntities.UserInfo;

namespace Cab.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ITokenService _tokenService;
        private readonly IDatabaseService _databaseService;
        private readonly IOptions<CabAppSettings> _settings;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public UserManagementService(ITokenService tokenService, IDatabaseService databaseService, IOptions<CabAppSettings> settings)
        {
            _tokenService = tokenService;
            _databaseService = databaseService;
            _settings = settings;
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
           // userInfo.AccessToken = _tokenService.CreateToken(userInfo);
            return userInfo;
        }
        /// <summary>
        /// Authenticates a user based on the provided employee ID and password.
        /// Returns an access token if authentication is successful; otherwise, returns an unauthorized response.
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(string EmpId, string AccessToken, string Role)?> AuthenticateUserAsync(string empId, string password)
            {
                var parameters = new Dictionary<string, string>
                    {
                        { "P_EmpId", empId },
                        { "P_Password", password }
                    };
            var permittedUsers = _settings.Value.PermittedUserAccess;
            
            var userInfo = await _databaseService.ExecuteStoredProcAsync<UserInfo>(StoredProcedures.GetUserInformation, parameters);

            if (userInfo == null)
                return null;

            string role = permittedUsers.Contains(empId) ? "admin" : "user";

            string token = _tokenService.CreateToken(userInfo, role);

            return (userInfo.EmpId, token, role);
        }
        /// <summary>
        /// Register new empployee in system
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns></returns>
        public async Task RegisterUserAsync(UserRegistration userRegistration)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "p_EmpId", userRegistration.EmpId.ToString()} ,
                { "P_EmpName", userRegistration.EmpName},
                { "P_Address", userRegistration.Address},
                { "P_EmpPhone", userRegistration.EmpPhone },
                { "P_EmailId", userRegistration.EmailId},
                { "P_Password", userRegistration.Password},
            };
            await _databaseService.ExecuteStoredProcAsync<UserRegistration>(StoredProcedures.GetRegisterUser, parameters);
        }
    }
}

