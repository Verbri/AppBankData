using AppBankData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace AppBankData.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        private readonly DBContext dbContext = new DBContext();

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetOperatingSystemData()
        {
            // List untuk menampung data yang akan dikembalikan
            List<ChartOperatingSystem> osData = new List<ChartOperatingSystem>();

            // Koneksi ke database menggunakan connection string dari Web.config

            string connectionString = dbContext.GetConnectionString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT OS_Group, COUNT(*) AS Count " +
                    "FROM(SELECT CASE " +
                    "WHEN OS LIKE '%10%' THEN 'Windows 10'" +
                    "WHEN OS LIKE '%7%' THEN 'Windows 7'" +
                    "ELSE 'Other'" +
                    "END AS OS_Group FROM datakomputer) AS Query GROUP BY OS_Group";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    osData.Add(new ChartOperatingSystem
                    {

                        OS = reader["OS_Group"].ToString(),
                        Count = Convert.ToInt32(reader["Count"]),
                        });
                }

                con.Close();
            }

            // Mengembalikan data dalam format JSON
            return Json(new { data = osData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCapacityDriveData()
        {
            // List untuk menampung data yang akan dikembalikan
            List<ChartHardisk> HardiskData = new List<ChartHardisk>();

            // Koneksi ke database menggunakan connection string dari Web.config

            string connectionString = dbContext.GetConnectionString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Hardisk_Group, COUNT(*) AS Count " +
                    "FROM(SELECT CASE " +
                    "WHEN Hardisk LIKE '%128%' THEN '128 GB'" +
                    "WHEN Hardisk LIKE '%240%' THEN '240 GB'" +
                    "WHEN Hardisk LIKE '%250%' THEN '250 GB'" + 
                    "WHEN Hardisk LIKE '%320%' THEN '320 GB'" +
                    "WHEN Hardisk LIKE '%500%' THEN '500 GB'" +
                    "ELSE 'Other' END AS Hardisk_Group FROM datakomputer) AS Query GROUP BY Hardisk_Group";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    HardiskData.Add(new ChartHardisk
                    {

                        Hardisk = reader["Hardisk_Group"].ToString(),
                        Count = Convert.ToInt32(reader["Count"]),
                    });
                }

                con.Close();
            }

            // Mengembalikan data dalam format JSON
            return Json(new { data = HardiskData }, JsonRequestBehavior.AllowGet);
        }
    }
}
