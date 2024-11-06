using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppBankData.Models
{
    public class InstalledApps
    {
        [Display(Name = "Id Aplikasi")]
        public string Id_Aplikasi { set; get; }

        [Display(Name = "Nama Aplikasi")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Nama_Aplikasi { set; get; }

        [Display(Name = "Deskripsi")]
        public string Deskripsi { set; get; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public bool IsActive { set; get; }
    }

    public class InstalledProgram
    {
        [Display(Name = "Id Program")]
        public string Id_Program { set; get; }

        [Display(Name = "Nama Program")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Nama_Program { set; get; }

        [Display(Name = "Deskripsi")]
        public string Deskripsi { set; get; }

        [Display(Name = "IsActive")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public bool IsActive { set; get; }
    }

    public class AppsProgramContext
        {
        DBContext dbCont = new DBContext();

        private string GenerateNewIdApps()
        {
            string lastId = null;
            int newIdNumber = 1;

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                string query = "SELECT TOP 1 Id_Aplikasi FROM DataAplikasi ORDER BY Id_Aplikasi DESC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lastId = reader["Id_Aplikasi"].ToString();
                }

                reader.Close();
            }

            // If there is a previous ID, extract the numeric part and increment it
            if (!string.IsNullOrEmpty(lastId))
            {
                // Assuming the ID is in format "App-0001", extract the numeric part
                lastId = lastId.Replace("" +
                    "App-", "");
                if (int.TryParse(lastId, out int lastNumber))
                {
                    newIdNumber = lastNumber + 1;
                }
            }

            // Create the new ID with the prefix "App-" and a formatted number (e.g., App-0002)
            return $"App-{newIdNumber:D5}";
        }

        private string GenerateNewIdProgram()
        {
            string lastId = null;
            int newIdNumber = 1;

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                string query = "SELECT TOP 1 Id_Program FROM DataProgram ORDER BY Id_Program DESC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lastId = reader["Id_Program"].ToString();
                }

                reader.Close();
            }

            // If there is a previous ID, extract the numeric part and increment it
            if (!string.IsNullOrEmpty(lastId))
            {
                // Assuming the ID is in format "App-0001", extract the numeric part
                lastId = lastId.Replace("" +
                    "Prog-", "");
                if (int.TryParse(lastId, out int lastNumber))
                {
                    newIdNumber = lastNumber + 1;
                }
            }

            // Create the new ID with the prefix "App-" and a formatted number (e.g., App-0002)
            return $"Prog-{newIdNumber:D5}";
        }

        public IEnumerable<InstalledApps> GetAllInstalledApps()
        {
            List<InstalledApps> list = new List<InstalledApps>();

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * From DataAplikasi", con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new InstalledApps
                    { 
                        Id_Aplikasi = reader["Id_Aplikasi"].ToString(),
                        Nama_Aplikasi = reader["Nama_Aplikasi"].ToString(),
                        Deskripsi = reader["Deskripsi"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                    });
                }

                con.Close();
            }
            return list;
        }

        public IEnumerable<InstalledProgram> GetAllInstalledProgram()
        {
            List<InstalledProgram> list = new List<InstalledProgram>();

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * From DataProgram", con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new InstalledProgram
                    {
                        Id_Program = reader["Id_Program"].ToString(),
                        Nama_Program = reader["Nama_Program"].ToString(),
                        Deskripsi = reader["Deskripsi"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                    });
                }

                con.Close();
            }
            return list;
        }

        public void AddInstalledApps(InstalledApps installedApps)
        {
            installedApps.Id_Aplikasi = GenerateNewIdApps();
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                int status;
                if (installedApps.IsActive == true)
                { status = 1; }
                else { status = 0; }
                string sqlQuery = "INSERT INTO DataAplikasi(Id_Aplikasi,Nama_Aplikasi,Deskripsi,IsActive) VALUES('" + installedApps.Id_Aplikasi + "','" + installedApps.Nama_Aplikasi + "','" + installedApps.Deskripsi + "','" + status + "')";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddInstalledProgram(InstalledProgram installedProgram)
        {
            installedProgram.Id_Program = GenerateNewIdProgram();
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                int status;
                if (installedProgram.IsActive == true)
                { status = 1; }
                else { status = 0; }
                string sqlQuery = "INSERT INTO DataProgram(Id_Program,Nama_Program,Deskripsi,IsActive) VALUES('" + installedProgram.Id_Program + "','" + installedProgram.Nama_Program + "','" + installedProgram.Deskripsi + "','" + status + "')";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}