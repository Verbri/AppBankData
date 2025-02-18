using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppBankData.Models
{
    public class Role
    {
        public string Id_Role { get; set; }

        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Nama_Role { get; set; }
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Deskripsi { get; set; }


    }

    public class RoleContext
    {
        private readonly DBContext dbCont = new DBContext();

        private string GenerateNewIdRole()
        {
            string lastId = null;
            int newIdNumber = 1;

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                string query = "SELECT TOP 1 Id_Role FROM Role ORDER BY Id_Role DESC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lastId = reader["Id_Role"].ToString();
                }

                reader.Close();
            }

            // If there is a previous ID, extract the numeric part and increment it
            if (!string.IsNullOrEmpty(lastId))
            {
                // Assuming the ID is in format "App-0001", extract the numeric part
                lastId = lastId.Replace("" +
                    "Role-", "");
                if (int.TryParse(lastId, out int lastNumber))
                {
                    newIdNumber = lastNumber + 1;
                }
            }

            // Create the new ID with the prefix "App-" and a formatted number (e.g., App-0002)
            return $"Role-{newIdNumber:D2}";
        }

        public IEnumerable<Role> GetAllRoles()
        {
            List<Role> list = new List<Role>();

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * From Role", con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Role
                    {
                        Id_Role = reader["Id_Role"].ToString(),
                        Nama_Role = reader["Nama_Role"].ToString(),
                        Deskripsi = reader["Deskripsi"].ToString()
                    });
                }
                con.Close();
            }
            return list;
        }

        public void AddRole(Role role)
        {
            try
            {
                string Id_Role = GenerateNewIdRole();
                using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
                {

                    string sqlQuery = "INSERT INTO Role (Id_Role,Nama_Role,Deskripsi) VALUES(@Id_Role,@Nama_Role,@Deskripsi)";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Id_Role", Id_Role);
                        cmd.Parameters.AddWithValue("@Nama_Role", role.Nama_Role ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Deskripsi", role.Deskripsi ?? (object)DBNull.Value);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }
    }
}
