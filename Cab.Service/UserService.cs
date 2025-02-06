using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cab.Infrastructure.Data;
using Cab.Core.Domain_Enities;
using Cab.Core.Interface;
namespace Cab.Service
{
    public class UserService : IUserService
    {
        private readonly SqlHelper _sqlHelper;
        public UserService(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }
        public async Task<User?> LoginAsync(int empId, string password)
        {
            Dictionary<string, object> parameters = new()
                 {
                    { "p_EmpId", empId },
                    { "p_Password", password }
                 };

            DataTable resultTable = await _sqlHelper.ExecuteStoredProcedureAsync("sp_UserLogin", parameters);

            if (resultTable.Rows.Count == 0)
            {
                Console.WriteLine("No user found for given credentials.");
                return null;
            }

            DataRow row = resultTable.Rows[0];
            return new User
            {
                UserId = Convert.ToInt32(row["UserId"]),
                EmpName = row["EmpName"].ToString(),
                Role = row["Role"].ToString(),
                EmpId = Convert.ToInt32(row["EmpId"])
            };
        }
    }
}
