using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using BCrypt.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;

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

        [Display(Name = "Active")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(100)]
        public string IsActive { get; set; }
    }

    public class UserContext
    {
        private DBContext dbCont = new DBContext();
        string password = "";
        public IEnumerable<User> GetAllUsers()
        {
            List<User> list = new List<User>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM Admin";
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
                        IsActive = reader["IsActive"].ToString(),
                        Bagian = reader["Bagian"].ToString()

                    });
                }

                con.Close();
            }
            return list;
        }

        public void AddUser(RegistrationView registrationView)
        {
            password = BCrypt.Net.BCrypt.HashPassword(registrationView.Password);

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "INSERT INTO users(Id, Nama, Password, Bagian, IsActive) VALUES('" + registrationView.NIK + "','" + registrationView.Nama + "', " +
                                  "'" + password + "', '" + registrationView.Bagian + "', '" + registrationView.IsActive + "')";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                //string sqlQuery2 = "INSERT INTO UserRoles( UserID, RoleID) VALUES('" + registrationView.NIK + "', '" + registrationView.RoleID + "') ";
                //SqlCommand cmd2 = new SqlCommand(sqlQuery2, con);
                con.Open();

                cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
                con.Close();
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
                    user.IsActive = reader["IsActive"].ToString();
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
                    "nama ='" + User.Nama + "', Status = '" + User.IsActive + "' WHERE Id ='" + User.NIK + "'";
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