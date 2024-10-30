using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace AppBankData.Models
{
    public class ListKomputer
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Deskripsi")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Description { set; get; }

        [Display(Name = "Nama Komputer")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Computername { set; get; }

        [Display(Name = "Workgroup")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Workgroup { set; get; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Useraccount { set; get; }

        [Display(Name = "Ip Address")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Ipaddress { set; get; }

        [Display(Name = "Akses")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Access { set; get; }

        [Display(Name = "Motherboard")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Motherboard { set; get; }

        [Display(Name = "Operating System")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Os { set; get; }

        [Display(Name = "Office")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Office { set; get; }

        [Display(Name = "Harddisk")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Hardisk { set; get; }

        [Display(Name = "Processor")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Processor { set; get; }

        [Display(Name = "RAM")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Ram { set; get; }

        [Display(Name = "Printer")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Printer { set; get; }

        [Display(Name = "Lokasi")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Lokasi { set; get; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Logo { set; get; }

        [Display(Name = "Program Standar")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Programstandar { set; get; }

        [Display(Name = "Program Immanuel")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Programimmanuel { set; get; }

        [Display(Name = "Avg")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Avg { set; get; }

        [Display(Name = "Smadav")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Smadav { set; get; }

        [Display(Name = "Usb Block")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Usbblock { set; get; }

        [Display(Name = "Monitor")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Monitor { set; get; }

        [Display(Name = "Mouse")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Mouse { set; get; }

        [Display(Name = "Keyboard")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Keyboard { set; get; }

        [Display(Name = "CPU")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Cpu { set; get; }

        [Display(Name = "Ups")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Ups { set; get; }

        [Display(Name = "Maintenance CPU")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string MaintenanceCPU { set; get; }

        [Display(Name = "Maintenance Monitor")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string MaintenanceMonitor { set; get; }

        [Display(Name = "Maintenance Jaringan")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string MaintenanceJaringan { set; get; }

        [Display(Name = "Maintenance Printer")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string MaintenancePrinter { set; get; }

        [Display(Name = "Maintenance UPS")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string MaintenanceUPS { set; get; }

        [Display(Name = "Keterangan 1")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Keterangan1 { set; get; }

        [Display(Name = "Keterangan 2")]
        //[Required(ErrorMessage = "{0} harus diisi.")]
        public string Keterangan2 { set; get; }

        [Display(Name = "Nama User")]
        //[Required(ErrorMessage = "{0} harus diisi.")]
        public string NamaUser { set; get; }

        [Display(Name = "Tanggal Update")]
        //[Required(ErrorMessage = "{0} harus diisi.")]
        public string TanggalUpdate { set; get; }

        public ICollection<InstalledAppsProgram> InstalledApps { set; get; }
        public ICollection<InstalledProgram> InstalledProgram { set; get; }
    }
    public class ListKomputerContext
    {
        private DBContext dbCont = new DBContext();

        //Get all ListKomputer to list
        public IEnumerable<ListKomputer> GetAllListKomputers()
        {
            List<ListKomputer> list = new List<ListKomputer>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM datakomputer";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new ListKomputer
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Description = reader["Description"].ToString(),
                        Computername = reader["Computername"].ToString(),
                        Workgroup = reader["Workgroup"].ToString(),
                        Useraccount = reader["Useraccount"].ToString(),
                        Ipaddress = reader["Ipaddress"].ToString(),
                        Access = reader["Access"].ToString(),
                        Motherboard = reader["Motherboard"].ToString(),
                        Os = reader["Os"].ToString(),
                        Office = reader["Office"].ToString(),
                        Hardisk = reader["Hardisk"].ToString(),
                        Processor = reader["Processor"].ToString(),
                        Ram = reader["Ram"].ToString(),
                        Printer = reader["Printer"].ToString(),
                        Lokasi = reader["Lokasi"].ToString(),
                        Logo = reader["Logo"].ToString(),
                        Programstandar = reader["Programstandar"].ToString(),
                        Programimmanuel = reader["Programimmanuel"].ToString(),
                        Avg = reader["Avg"].ToString(),
                        Smadav = reader["Smadav"].ToString(),
                        Usbblock = reader["Usbblock"].ToString(),
                        Monitor = reader["Monitor"].ToString(),
                        Mouse = reader["Mouse"].ToString(),
                        Keyboard = reader["Keyboard"].ToString(),
                        Cpu = reader["Cpu"].ToString(),
                        Ups = reader["Ups"].ToString(),
                        MaintenanceCPU = reader["MaintenanceCPU"].ToString(),
                        MaintenanceMonitor = reader["MaintenanceMonitor"].ToString(),
                        MaintenanceJaringan = reader["MaintenanceJaringan"].ToString(),
                        MaintenancePrinter = reader["MaintenancePrinter"].ToString(),
                        MaintenanceUPS = reader["MaintenanceUPS"].ToString(),
                        Keterangan1 = reader["Keterangan1"].ToString(),
                        Keterangan2 = reader["Keterangan2"].ToString(),
                        NamaUser = reader["namauser"].ToString(),
                        TanggalUpdate = reader["TanggalUpdate"].ToString(),

                    });
                }

                con.Close();
            }
            return list;
        }

        //Get all ListKomputer details
        public ListKomputer GetListKomputerData(int? Id)
        {
            ListKomputer ListKomputer = new ListKomputer();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string SqlQuery = "SELECT * FROM datakomputer Where Id=" + Id;

                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListKomputer.Id = Convert.ToInt32(reader["Id"]);
                    ListKomputer.Description = reader["Description"].ToString();
                    ListKomputer.Computername = reader["Computername"].ToString();
                    ListKomputer.Workgroup = reader["Workgroup"].ToString();
                    ListKomputer.Useraccount = reader["Useraccount"].ToString();
                    ListKomputer.Ipaddress = reader["Ipaddress"].ToString();
                    ListKomputer.Access = reader["Access"].ToString();
                    ListKomputer.Motherboard = reader["Motherboard"].ToString();
                    ListKomputer.Os = reader["Os"].ToString();
                    ListKomputer.Office = reader["Office"].ToString();
                    ListKomputer.Hardisk = reader["Hardisk"].ToString();
                    ListKomputer.Processor = reader["Processor"].ToString();
                    ListKomputer.Ram = reader["Ram"].ToString();
                    ListKomputer.Printer = reader["Printer"].ToString();
                    ListKomputer.Lokasi = reader["Lokasi"].ToString();
                    ListKomputer.Logo = reader["Logo"].ToString();
                    ListKomputer.Programstandar = reader["Programstandar"].ToString();
                    ListKomputer.Programimmanuel = reader["Programimmanuel"].ToString();
                    ListKomputer.Avg = reader["Avg"].ToString();
                    ListKomputer.Smadav = reader["Smadav"].ToString();
                    ListKomputer.Usbblock = reader["Usbblock"].ToString();
                    ListKomputer.Monitor = reader["Monitor"].ToString();
                    ListKomputer.Mouse = reader["Mouse"].ToString();
                    ListKomputer.Keyboard = reader["Keyboard"].ToString();
                    ListKomputer.Cpu = reader["Cpu"].ToString();
                    ListKomputer.Ups = reader["Ups"].ToString();
                    ListKomputer.MaintenanceCPU = reader["MaintenanceCPU"].ToString();
                    ListKomputer.MaintenanceMonitor = reader["MaintenanceMonitor"].ToString();
                    ListKomputer.MaintenanceJaringan = reader["MaintenanceJaringan"].ToString();
                    ListKomputer.MaintenancePrinter = reader["MaintenancePrinter"].ToString();
                    ListKomputer.MaintenanceUPS = reader["MaintenanceUPS"].ToString();
                    ListKomputer.Keterangan1 = reader["Keterangan1"].ToString();
                    ListKomputer.Keterangan2 = reader["Keterangan2"].ToString();
                    ListKomputer.NamaUser = reader["namauser"].ToString();
                    ListKomputer.TanggalUpdate = reader["TanggalUpdate"].ToString();
                }
                con.Close();
            }
            return ListKomputer;
        }

        public void AddPesertaDidik(ListKomputer ListKomputer)
        {
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "INSERT INTO datakomputer(Description,Computername,Workgroup,Useraccount,Ipaddress,Access,Motherboard,Os,Office,Hardisk,Processor,Ram,Printer,Lokasi,Logo,Programstandar,Programimmanuel,Avg,Smadav,Usbblock,Monitor,Mouse,Keyboard,Cpu,Ups,MaintenanceCPU,MaintenanceMonitor,MaintenanceJaringan,MaintenancePrinter,MaintenanceUPS,Keterangan1,Keterangan2,namauser,TanggalUpdate) VALUES('" + ListKomputer.Description + "'," +
                    "'" + ListKomputer.Computername + "','" + ListKomputer.Workgroup + "','" + ListKomputer.Useraccount + "'," +
                    "'" + ListKomputer.Ipaddress + "','" + ListKomputer.Access + "','" + ListKomputer.Motherboard + "'," +
                    "'" + ListKomputer.Os + "','" + ListKomputer.Office + "'," +
                    "'" + ListKomputer.Hardisk + "','" + ListKomputer.Processor + "'," +
                    "'" + ListKomputer.Ram + "','" + ListKomputer.Printer + "'," +
                    "'" + ListKomputer.Lokasi + "','" + ListKomputer.Logo + "'," +
                    "'" + ListKomputer.Programstandar + "','" + ListKomputer.Programimmanuel + "'," +
                    "'" + ListKomputer.Avg + "','" + ListKomputer.Smadav + "'," +
                    "'" + ListKomputer.Usbblock + "','" + ListKomputer.Monitor + "'," +
                    "'" + ListKomputer.Mouse + "','" + ListKomputer.Keyboard + "'," +
                    "'" + ListKomputer.Cpu + "','" + ListKomputer.Ups + "'," +
                    "'" + ListKomputer.MaintenanceCPU + "','" + ListKomputer.MaintenanceMonitor + "'," +
                    "'" + ListKomputer.MaintenanceJaringan + "','" + ListKomputer.MaintenancePrinter + "'," +
                    "'" + ListKomputer.MaintenanceUPS + "','" + ListKomputer.Keterangan1 + "'," +
                    "'" + ListKomputer.Keterangan2 + "','" + ListKomputer.NamaUser + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss") + "','" + 1 + "')";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //Update List Komputer
        public void UpdateListKomputer(ListKomputer ListKomputer)
        {
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "UPDATE datakomputer SET Description = '" + ListKomputer.Description + "', Computername = '" + ListKomputer.Computername + "'," +
                    " Workgroup = '" + ListKomputer.Workgroup + "', Useraccount = '" + ListKomputer.Useraccount + "', Ipaddress = '" + ListKomputer.Ipaddress + "', Access = '" + ListKomputer.Access + "'," +
                    " Motherboard = '" + ListKomputer.Motherboard + "', Os = '" + ListKomputer.Os + "', Office = '" + ListKomputer.Office + "'," +
                    " Processor = '" + ListKomputer.Processor + "', Ram = '" + ListKomputer.Ram + "',Printer = '" + ListKomputer.Printer + "'," +
                    " Lokasi = '" + ListKomputer.Lokasi + "', Logo = '" + ListKomputer.Logo + "', Programstandar = '" + ListKomputer.Programstandar + "', Programimmanuel = '" + ListKomputer.Programimmanuel + "'," +
                    " Avg = '" + ListKomputer.Avg + "', Smadav = '" + ListKomputer.Smadav + "', Usbblock = '" + ListKomputer.Usbblock + "'," +
                    " Monitor = '" + ListKomputer.Monitor + "', Mouse = '" + ListKomputer.Mouse + "', Keyboard = '" + ListKomputer.Keyboard + "'," +
                    " Cpu = '" + ListKomputer.Cpu + "', Ups = '" + ListKomputer.Ups + "', MaintenanceCPU = '" + ListKomputer.MaintenanceCPU + "', MaintenanceMonitor = '" + ListKomputer.MaintenanceMonitor + "'," +
                    " MaintenanceJaringan = '" + ListKomputer.MaintenanceJaringan + "', MaintenancePrinter = '" + ListKomputer.MaintenancePrinter + "', MaintenanceUPS = '" + ListKomputer.MaintenanceUPS + "',Keterangan1 = '" + ListKomputer.Keterangan1 + "'," +
                    " Keterangan2 = '" + ListKomputer.Keterangan2 + "', NamaUser = '" + ListKomputer.NamaUser + "', TanggalUpdate = '" + ListKomputer.TanggalUpdate + "' WHERE Id='" + ListKomputer.Id + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteListKomputer(int? Id)
        {
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "DELETE FROM biodata WHERE Id=" + Id + "";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public IEnumerable<InstalledAppsProgram> GetApplicationList()
        {
            List<InstalledAppsProgram> list = new List<InstalledAppsProgram>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM DataAplikasi WHERE IsActive= 1";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new InstalledAppsProgram
                    {
                        Id_Aplikasi = reader["Id_Aplikasi"].ToString(),
                        Nama_Aplikasi = reader["Nama_Aplikasi"].ToString()

                    });
                }
                con.Close();
            }
            return list;
        }

        public IEnumerable<InstalledProgram> GetProgramList()
        {
            List<InstalledProgram> list = new List<InstalledProgram>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM DataProgram WHERE IsActive= 1";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new InstalledProgram
                    {
                        Id_Program = reader["Id_Program"].ToString(),
                        Nama_Program = reader["Nama_Program"].ToString()
                    });
                }
                con.Close();
            }
            return list;
        }
    }
}