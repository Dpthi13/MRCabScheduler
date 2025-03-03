
namespace Cab.Infrastructure.Helpers
{
    public class CabAppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Jwt Jwt { get; set; }
        public IEnumerable<string> PermittedUserAccess {  get; set; }
    }

    public class ConnectionStrings
    {
        public string CabDatabase { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Validity { get; set; }
    }
}
