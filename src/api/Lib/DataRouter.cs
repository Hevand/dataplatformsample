using Microsoft.Data.SqlClient;

namespace api.Lib
{
    static public class DataRouter
    {
        static public string TenantDbConnectionString(string MasterDbConnectionString, string TenantAadId)
        {
            string server = "";
            string database = "";
                


            SqlConnection sqlconn = new SqlConnection(MasterDbConnectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlconn;
            sqlCommand.CommandText = "SELECT * FROM Tenants WHERE tenant_aadtentantid = @id";

            SqlParameter p = new SqlParameter();
            p.ParameterName = "@id";
            p.SqlDbType = System.Data.SqlDbType.NVarChar;
            p.Value = TenantAadId;
            sqlCommand.Parameters.Add(p);

            sqlconn.Open();
            using (SqlDataReader dr = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        server = dr["tenant_dbserver"].ToString();
                        database = dr["tenant_dbname"].ToString();
                    }
                }
                dr.Close();
            }
            

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(MasterDbConnectionString);
            sb["Server"] = server;
            sb.InitialCatalog = database;
            return sb.ToString();

            
        }
    }
}
