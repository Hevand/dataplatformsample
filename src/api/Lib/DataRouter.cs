using api.Models;
using Microsoft.Data.SqlClient;

namespace api.Lib
{
    static public class DataRouter
    {
        static public string TenantDbConnectionString(dbTenantAdminContext context, string masterDbConnectionString, string tenantAadId)
        {
            string server = "";
            string database = "";

            var tenant = context.Tenants.Where(t => t.TenantAadtentantid == tenantAadId).ToList();
            if(tenant.Any())
            {
                server = tenant.First().TenantDbserver;
                database = tenant.First().TenantDbname;
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(masterDbConnectionString);
                sb["Server"] = server;
                sb.InitialCatalog = database;
                return sb.ToString();
            }
            else
            {
                throw new Exception("Tenant not found in TenantAdmin Database.");
            }
        }
    }
}
