using System.Data;

namespace Cab.Infrastructure.DomainEntities
{
    public class SQLParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public SqlDbType SqlDbType { get; set; }
        public string TypeName { get; set; }
    }
}
