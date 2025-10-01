using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Telerik.Reporting.Processing;

namespace AppBankData.Models
{
    public class Parameter
    {
        public string ParameterID { set; get; }
        public string ParameterName { set; get; }
        public string ParameterValue { set; get; }
        public string Description { set; get; }
        public string CreateDateTime { set; get; }
        public string LastUpdateDateTime { set; get; }
        public string CreateByUserID { set; get; }
        public string LastUpdateByUserID { set; get; }
    }
    public class ParameterContext
    {
        private readonly DBContext dbCont = new DBContext();

        private string GenerateNewIdParameter()
        {
            string lastId = null;
            int newIdNumber = 1;

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                string query = "SELECT TOP 1 ParameterID FROM AppParameter ORDER BY ParameterID DESC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lastId = reader["ParameterID"].ToString();
                }

                reader.Close();
            }

            // If there is a previous ID, extract the numeric part and increment it
            if (!string.IsNullOrEmpty(lastId))
            {
                // Assuming the ID is in format "App-0001", extract the numeric part
                lastId = lastId.Replace("" +
                    "Param-", "");
                if (int.TryParse(lastId, out int lastNumber))
                {
                    newIdNumber = lastNumber + 1;
                }
            }

            // Create the new ID with the prefix "App-" and a formatted number (e.g., App-0002)
            return $"Param-{newIdNumber:D5}";
        }

        public IEnumerable<Parameter> GetAllParameter()
        {
            List<Parameter> list = new List<Parameter>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * From AppParameter", con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Parameter
                    {
                        ParameterID = reader["ParameterID"].ToString(),
                        ParameterName = reader["ParameterName"].ToString(),
                        ParameterValue = reader["ParameterValue"].ToString(),
                        Description = reader["Description"].ToString(),
                        CreateDateTime = reader["CreateDateTIme"].ToString(),
                        LastUpdateDateTime = reader["LastUpdateDateTIme"].ToString(),
                        CreateByUserID = reader["CreateByUserID"].ToString(),
                        LastUpdateByUserID = reader["lastUpdateByUserID"].ToString()
                    });
                }

                con.Close();
            }
            return list;
        }

        public Parameter DetailParameter(string id)
        {
            Parameter parameter = new Parameter();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string SqlQuery = "SELECT * WHERE ParameterID = @id";

                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.Parameters.AddWithValue("@ParameterID", id);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    parameter.ParameterID = reader["ParameterID"].ToString();
                    parameter.ParameterName = reader["ParameterName"].ToString();
                    parameter.ParameterValue = reader["ParameterValue"].ToString();
                    parameter.Description = reader["Deskripsi"].ToString();
                    parameter.CreateDateTime = reader["CreateDateTIme"].ToString();
                    parameter.LastUpdateDateTime = reader["LastUpdateDateTIme"].ToString();
                    parameter.CreateByUserID = reader["CreateByUserID"].ToString();
                    parameter.LastUpdateByUserID = reader["lastUpdateByUserID"].ToString();
                }
                con.Close();
            }
            return parameter;
        }

        public void AddParameter(Parameter parameter)
        {
            parameter.ParameterID = GenerateNewIdParameter();
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                
                string sqlQuery = @"INSERT INTO AppParameter(ParameterID,ParameterName,ParameterValue,Description,CreateDateTime,CreateByUserID) VALUES(@parameterID,@ParameterName,@ParameterValue,@Description,@CreateDateTime,@CreateByUserID)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ParameterID", parameter.ParameterID);
                    cmd.Parameters.AddWithValue("@ParameterName", parameter.ParameterName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ParameterValue", parameter.ParameterValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", parameter.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreateDateTime", DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss"));
                    cmd.Parameters.AddWithValue("@CreateByUserID", parameter.CreateByUserID ?? (object)DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void UpdateParameter(Parameter parameter)
        {
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {

                string sqlQuery = @"Update AppParameter
                                SET ParameterName=@ParameterName,
                                    ParameterValue=@ParameterValue,
                                    Description=@Description,
                                    LastUpdateDateTime=@LastUpdateDateTime,
                                    LastUpdateByUserID=@LastUpdateByUserID)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ParameterID", parameter.ParameterID);
                    cmd.Parameters.AddWithValue("@ParameterName", parameter.ParameterName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ParameterValue", parameter.ParameterValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", parameter.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastUpdateDateTime", DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss"));
                    cmd.Parameters.AddWithValue("@LastUpdateByUserID", parameter.LastUpdateByUserID ?? (object)DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}

