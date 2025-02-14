using Cab.Infrastructure.DomainEntities;
using Dapper;
using System.Data;

namespace Cab.Infrastructure.Helpers
{
    public static class DapperHelper
    {
        public static void MapProperties(Dictionary<string, string> value, DynamicParameters dynamicParameters)
        {
            IDictionary<string, string> dapperRowProperties = value as IDictionary<string, string>;
            foreach (KeyValuePair<string, string> property in dapperRowProperties)
                dynamicParameters.Add(property.Key, property.Value);
        }

        public static object GetValObjDy(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        public static void MapProperties(Dictionary<string, object> value, DynamicParameters dynamicParameters)
        {
            IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> property in dapperRowProperties)
                dynamicParameters.Add(property.Key, property.Value);
        }

        public static void MapProperties(List<SQLParameter> values, DynamicParameters dynamicParameters)
        {
            foreach (SQLParameter param in values)
                dynamicParameters.Add(param.Name, param.Value, param.SqlDbType == SqlDbType.Structured ? DbType.Object : null);
        }

    }
}
