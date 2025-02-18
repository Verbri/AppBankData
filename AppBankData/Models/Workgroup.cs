using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AppBankData.Models
{
    public class Workgroup
    {
        [Display(Name = "Id Workgroup")]
        public string Id_Workgroup{ set; get; }

        [Display(Name = "Nama Workgroup")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Nama_Workgroup { set; get; }

        [Display(Name = "Create Date")]
        public string CreateDate { set; get; }

        [Display(Name = "Create Date")]
        public string Id_User { set; get; }

    }

    public class WorkgroupContext
    {
        private readonly DBContext dbCont = new DBContext();

        private string GenerateNewIdWorkgroup()
        {
            string lastId = null;
            int newIdNumber = 1;

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                string query = "SELECT TOP 1 Id_Workgroup FROM DataWorkgroup ORDER BY Id_Workgroup DESC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lastId = reader["Id_Workgroup"].ToString();
                }

                reader.Close();
            }

            // If there is a previous ID, extract the numeric part and increment it
            if (!string.IsNullOrEmpty(lastId))
            {
                // Assuming the ID is in format "App-0001", extract the numeric part
                lastId = lastId.Replace("" +
                    "Work-", "");
                if (int.TryParse(lastId, out int lastNumber))
                {
                    newIdNumber = lastNumber + 1;
                }
            }

            // Create the new ID with the prefix "App-" and a formatted number (e.g., App-0002)
            return $"Work-{newIdNumber:D5}";
        }

        public IEnumerable<Workgroup> GetAllWorkgroup()
        {
            List<Workgroup> list = new List<Workgroup>();

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * From DataWorkgroup", con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Workgroup
                    {
                        Id_Workgroup = reader["Id_Workgroup"].ToString(),
                        Nama_Workgroup = reader["Nama_Workgroup"].ToString(),
                        CreateDate = reader["Nama_Workgroup"].ToString(),
                        Id_User = reader["Id_User"].ToString()
                    });
                }
                con.Close();
            }
            return list;
        }

        public void AddWorkgroup(Workgroup workgroup)
        {
            workgroup.Id_Workgroup = GenerateNewIdWorkgroup();
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
               
                string sqlQuery = "INSERT INTO DataWorkgroup(Id_Workgroup,Nama_Workgroup,CreateDate,Id_User) VALUES(@Id_Workgroup,@Nama_Workgroup,@CreateDate,@Id_User)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Id_Workgroup", workgroup.Id_Workgroup);
                    cmd.Parameters.AddWithValue("@Computername", workgroup.Nama_Workgroup);
                    cmd.Parameters.AddWithValue("@CreateDate", workgroup.CreateDate);
                    cmd.Parameters.AddWithValue("@Id_User", workgroup.Id_User);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                } ;
            }
        }
    }
}
