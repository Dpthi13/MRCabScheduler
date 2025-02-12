using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
