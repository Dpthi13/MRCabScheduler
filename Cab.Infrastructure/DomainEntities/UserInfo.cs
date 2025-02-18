namespace Cab.Infrastructure.DomainEntities
{
    public class UserInfo
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string RoleName { get; set; }
    }
    public class UserRegistration
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Address { get; set; }
        public string EmpPhone { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
