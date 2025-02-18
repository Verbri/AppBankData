using System.Data.SqlClient;

namespace AppBankData.Models
{
    public class ConfigurationHelper
    {
        public static string GetConfigurationValue(string ParameterID)
        {
            DBContext dbCont = new DBContext(); // Ganti dengan connection string Anda.
            string ParameterValue = null;

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                string SqlQuery = "SELECT ParameterValue FROM AppParameter WHERE ParameterID = @ParameterID";
                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.Parameters.AddWithValue("@ParameterID", ParameterID);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    ParameterValue = result.ToString();
                }
            }

            return ParameterValue;
        }
    }
}
