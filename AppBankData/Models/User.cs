using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace AppBankData.Models
{
    public class User
    {
        [Display(Name = "NIK")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string NIK { get; set; }

        [Display(Name = "Nama")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(100)]
        public string Nama { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Bagian")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(100)]
        public string Bagian { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(100)]
        public string Role { get; set; }
    }

    public class UserContext
    {
        private DBContext dbCont = new DBContext();
        string password;
        public User AuthenticateUser(string NIK, string password)
        {

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {

                string query = "SELECT * FROM DataUser WHERE Id = @NIK";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@NIK", NIK);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string storedPasswordHash = reader["Password"].ToString();
                    bool status = Convert.ToBoolean(reader["Status"]); // Mengubah nilai Status menjadi boolean
                    // Verifikasi password yang dimasukkan dengan password hash yang disimpan
                    if (BCrypt.Net.BCrypt.Verify(password, storedPasswordHash))
                    {
                        return new User
                        {
                            NIK = reader["Id"].ToString(),
                            Password = reader["password"].ToString(),
                            Nama = reader["nama"].ToString(),
                            Bagian = reader["Bagian"].ToString(),
                            Role = reader["Role"].ToString(),
                            Status = status ? "1" : "0", // Mengonversi status ke string "1" atau "0"
                        };
                    }
                }
                return null;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> list = new List<User>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM DataUser ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new User
                    {
                        NIK = reader["Id"].ToString(),
                        Password = reader["password"].ToString(),
                        Nama = reader["nama"].ToString(),
                        Status = reader["Status"].ToString(),
                        Bagian = reader["bagian"].ToString()
                    });
                }

                con.Close();
            }
            return list;
        }

        public void AddUser(User user)
        {
            try
            {
                password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
                {
                    string sqlQuery = "INSERT INTO DataUser (Id, Nama, Password, Bagian, Status, Role) VALUES (@NIK, @Nama, @Password, @Bagian, @Status, @Role)";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@NIK", user.NIK);
                        cmd.Parameters.AddWithValue("@Nama", user.Nama);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Bagian", user.Bagian);
                        cmd.Parameters.AddWithValue("@Status", user.Status);
                        cmd.Parameters.AddWithValue("@Role", user.Role);


                        //string sqlQuery2 = "INSERT INTO UserRoles( UserID, RoleID) VALUES('" + user.NIK + "', '" + user.RoleID + "') ";
                        //SqlCommand cmd2 = new SqlCommand(sqlQuery2, con);
                        con.Open();

                        cmd.ExecuteNonQuery();
                        //cmd2.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }

        public User GetUserData(string id)
        {

            User user = new User();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM admin WHERE id = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user.NIK = reader["Id"].ToString();
                    user.Nama = reader["Nama"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.Bagian = reader["Bagian"].ToString();
                    user.Role = reader["Role"].ToString();
                    user.Status = reader["Status"].ToString();
                }
                con.Close();
            }
            return user;

        }

        public void UpdateUser(User User)
        {

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "UPDATE Admin SET password = '" + User.Password + "'," +
                    "nama ='" + User.Nama + "', Status = '" + User.Status + "' WHERE Id ='" + User.NIK + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteUser(string id)
        {
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "DELETE FROM Users WHERE Username=" + id + "";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdatePassword(GantiPassword gantiPassword)
        {
            password = BCrypt.Net.BCrypt.HashPassword(gantiPassword.Password);

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "UPDATE Admin SET password = '" + password + "' WHERE ID ='" + gantiPassword.NIK + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

}
