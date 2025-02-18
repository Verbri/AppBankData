using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AppBankData.Models
{
    public class ListKomputer
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id_Komputer")]
        public string Id_Komputer { set; get; }

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

        [Display(Name = "Aplikasi Standar")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public List<string> Programstandar { set; get; }

        [Display(Name = "Program Immanuel")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public List<string> Programimmanuel { set; get; } = new List<string>();

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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceCPU { set; get; }

        [Display(Name = "Maintenance Monitor")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceMonitor { set; get; }

        [Display(Name = "Maintenance Jaringan")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceJaringan { set; get; }

        [Display(Name = "Maintenance Printer")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenancePrinter { set; get; }

        [Display(Name = "Maintenance UPS")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceUPS { set; get; }

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

        //public ICollection<InstalledApps> InstalledAppss { set; get; }
        //public ICollection<InstalledProgram> InstalledProgram { set; get; }
        //public ICollection<Workgroup> ListWorkgroups { set; get; }
        public string ProgramstandarDisplay { set; get; }
        public string ProgramimmanuelDisplay { set; get; }
        public string NamaWorkgroup { set; get; }
    }
    public class ListKomputerContext
    {
        private readonly DBContext dbCont = new DBContext();


        private string GenerateNewIdKomputer()
        {
            string lastId = null;
            int newIdNumber = 1;

            using (SqlConnection con = new SqlConnection(connectionString: dbCont.GetConnectionString()))
            {
                string query = "SELECT TOP 1 Id_Komputer FROM DataKomputer ORDER BY Id_Komputer DESC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lastId = reader["Id_Komputer"].ToString();
                }

                reader.Close();
            }

            // If there is a previous ID, extract the numeric part and increment it
            if (!string.IsNullOrEmpty(lastId))
            {
                // Assuming the ID is in format "App-0001", extract the numeric part
                lastId = lastId.Replace("" +
                    "Komp-", "");
                if (int.TryParse(lastId, out int lastNumber))
                {
                    newIdNumber = lastNumber + 1;
                }
            }

            // Create the new ID with the prefix "App-" and a formatted number (e.g., App-0002)
            return $"Komp-{newIdNumber:D5}";
        }
        //Get all ListKomputer to lists
        public IEnumerable<ListKomputer> GetAllListKomputers()
        {
            List<ListKomputer> list = new List<ListKomputer>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT k.Id_Komputer,k.Description,k.Computername,k.Workgroup,k.Useraccount,k.Ipaddress,k.Access,k.Motherboard,k.Os,k.Office,k.Hardisk,k.Processor,k.Ram,k.Printer,k.Lokasi,k.Logo,k.programimmanuel,k.programstandar,wg.Nama_Workgroup,(SELECT STRING_AGG(a.Nama_Aplikasi, ',')  FROM DataAplikasi a  WHERE a.Id_Aplikasi IN (SELECT value FROM STRING_SPLIT(k.Programstandar, ','))) AS Nama_Aplikasi,(SELECT STRING_AGG(p.Nama_Program, ',')  FROM DataProgram p  WHERE p.Id_Program IN (SELECT value FROM STRING_SPLIT(k.Programimmanuel, ','))) AS Nama_Program,k.Avg,k.Smadav,k.Usbblock,k.Monitor,k.Mouse,k.Keyboard,k.Cpu,k.Ups,k.MaintenanceCPU,k.MaintenanceMonitor,k.MaintenanceJaringan,k.MaintenancePrinter,k.MaintenanceUPS,k.Keterangan1,k.Keterangan2,k.NamaUser,k.TanggalUpdate FROM DataKomputer k LEFT JOIN DataWorkgroup wg ON k.Workgroup = wg.Id_Workgroup";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new ListKomputer
                    {
                        Id_Komputer = reader["Id_Komputer"].ToString(),
                        Description = reader["Description"].ToString(),
                        Computername = reader["Computername"].ToString(),
                        Workgroup = reader["Workgroup"].ToString(),
                        NamaWorkgroup = reader["Nama_Workgroup"].ToString(),
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
                        Programstandar = reader["Programstandar"].ToString().Split(',').ToList(),
                        Programimmanuel = reader["Programimmanuel"].ToString().Split(',').ToList(),
                        Avg = reader["Avg"].ToString(),
                        Smadav = reader["Smadav"].ToString(),
                        Usbblock = reader["Usbblock"].ToString(),
                        Monitor = reader["Monitor"].ToString(),
                        Mouse = reader["Mouse"].ToString(),
                        Keyboard = reader["Keyboard"].ToString(),
                        Cpu = reader["Cpu"].ToString(),
                        Ups = reader["Ups"].ToString(),
                        MaintenanceCPU = reader["MaintenanceCPU"] as DateTime?,
                        MaintenanceMonitor = reader["MaintenanceMonitor"] as DateTime?,
                        MaintenanceJaringan = reader["MaintenanceJaringan"] as DateTime?,
                        MaintenancePrinter = reader["MaintenancePrinter"] as DateTime?,
                        MaintenanceUPS = reader["MaintenanceUPS"] as DateTime?,
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
        public ListKomputer GetListKomputerData(string Id_Komputer)
        {
            ListKomputer ListKomputer = new ListKomputer();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string SqlQuery = "SELECT k.Id_Komputer,k.Description,k.Computername,k.Workgroup,k.Useraccount,k.Ipaddress,k.Access,k.Motherboard,k.Os,k.Office,k.Hardisk,k.Processor,k.Ram,k.Printer,k.Lokasi,k.Logo,k.programimmanuel,k.programstandar,wg.Nama_Workgroup,(SELECT STRING_AGG(a.Nama_Aplikasi, ',')  FROM DataAplikasi a  WHERE a.Id_Aplikasi IN (SELECT value FROM STRING_SPLIT(k.Programstandar, ','))) AS Nama_Aplikasi,(SELECT STRING_AGG(p.Nama_Program, ',')  FROM DataProgram p  WHERE p.Id_Program IN (SELECT value FROM STRING_SPLIT(k.Programimmanuel, ','))) AS Nama_Program,k.Avg,k.Smadav,k.Usbblock,k.Monitor,k.Mouse,k.Keyboard,k.Cpu,k.Ups,k.MaintenanceCPU,k.MaintenanceMonitor,k.MaintenanceJaringan,k.MaintenancePrinter,k.MaintenanceUPS,k.Keterangan1,k.Keterangan2,k.NamaUser,k.TanggalUpdate FROM DataKomputer k JOIN DataWorkgroup wg ON k.Workgroup = wg.Id_Workgroup WHERE k.Id_Komputer = @Id_Komputer";

                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.Parameters.AddWithValue("@Id_Komputer", Id_Komputer);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListKomputer.Id_Komputer = reader["Id_Komputer"].ToString();
                    ListKomputer.Description = reader["Description"].ToString();
                    ListKomputer.Computername = reader["Computername"].ToString();
                    ListKomputer.Workgroup = reader["Workgroup"].ToString();
                    ListKomputer.NamaWorkgroup = reader["Nama_Workgroup"].ToString();
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
                    ListKomputer.Programstandar = reader["Programstandar"].ToString().Split(',').ToList();
                    ListKomputer.Programimmanuel = reader["Programimmanuel"].ToString().Split(',').ToList();
                    ListKomputer.ProgramstandarDisplay = reader["Nama_Aplikasi"].ToString();
                    ListKomputer.ProgramimmanuelDisplay = reader["Nama_Program"].ToString();
                    ListKomputer.Avg = reader["Avg"].ToString();
                    ListKomputer.Smadav = reader["Smadav"].ToString();
                    ListKomputer.Usbblock = reader["Usbblock"].ToString();
                    ListKomputer.Monitor = reader["Monitor"].ToString();
                    ListKomputer.Mouse = reader["Mouse"].ToString();
                    ListKomputer.Keyboard = reader["Keyboard"].ToString();
                    ListKomputer.Cpu = reader["Cpu"].ToString();
                    ListKomputer.Ups = reader["Ups"].ToString();
                    ListKomputer.MaintenanceCPU = reader["MaintenanceCPU"] as DateTime?;
                    ListKomputer.MaintenanceMonitor = reader["MaintenanceMonitor"] as DateTime?;
                    ListKomputer.MaintenanceJaringan = reader["MaintenanceJaringan"] as DateTime?;
                    ListKomputer.MaintenancePrinter = reader["MaintenancePrinter"] as DateTime?;
                    ListKomputer.MaintenanceUPS = reader["MaintenanceUPS"] as DateTime?;
                    ListKomputer.Keterangan1 = reader["Keterangan1"].ToString();
                    ListKomputer.Keterangan2 = reader["Keterangan2"].ToString();
                    ListKomputer.NamaUser = reader["namauser"].ToString();
                    ListKomputer.TanggalUpdate = reader["TanggalUpdate"].ToString();
                }
                con.Close();
            }
            return ListKomputer;
        }

        public void AddKomputer(ListKomputer listKomputer)
        {
            listKomputer.Id_Komputer = GenerateNewIdKomputer();
            string Programstandar = string.Join(",", listKomputer.Programstandar);
            string Programimmanuel = string.Join(",", listKomputer.Programimmanuel);
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = @"INSERT INTO datakomputer(Id_Komputer,Description,Computername,Workgroup,Useraccount,Ipaddress,Access,Motherboard,Os,Office,Hardisk,Processor,Ram,Printer,Lokasi,Logo,Programstandar,Programimmanuel,Avg,Smadav,Usbblock,Monitor,Mouse,Keyboard,Cpu,Ups,MaintenanceCPU,MaintenanceMonitor,MaintenanceJaringan,MaintenancePrinter,MaintenanceUPS,Keterangan1,Keterangan2,namauser,TanggalUpdate) VALUES (@Id_Komputer,@Description,@Computername,@Workgroup,@Useraccount,@Ipaddress,@Access,@Motherboard,@Os,@Office,@Hardisk,@Processor,@Ram,@Printer,@Lokasi,@Logo,@Programstandar,@Programimmanuel,@Avg,@Smadav,@Usbblock,@Monitor,@Mouse,@Keyboard,@Cpu,@Ups,@MaintenanceCPU,@MaintenanceMonitor,@MaintenanceJaringan,@MaintenancePrinter,@MaintenanceUPS,@Keterangan1,@Keterangan2,@Namauser,@TanggalUpdate)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Id_Komputer", listKomputer.Id_Komputer);
                    cmd.Parameters.AddWithValue("@Description", listKomputer.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Computername", listKomputer.Computername ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Workgroup", listKomputer.Workgroup ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Useraccount", listKomputer.Useraccount ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ipaddress", listKomputer.Ipaddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Access", listKomputer.Access ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Motherboard", listKomputer.Motherboard ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Os", listKomputer.Os ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Office", listKomputer.Office ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Hardisk", listKomputer.Hardisk ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Processor", listKomputer.Processor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ram", listKomputer.Ram ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Printer", listKomputer.Printer ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Lokasi", listKomputer.Lokasi ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Logo", listKomputer.Logo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Programstandar", Programstandar ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Programimmanuel", Programimmanuel ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Avg", listKomputer.Avg ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Smadav", listKomputer.Smadav ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Usbblock", listKomputer.Usbblock ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Monitor", listKomputer.Monitor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mouse", listKomputer.Mouse ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Keyboard", listKomputer.Keyboard ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cpu", listKomputer.Cpu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ups", listKomputer.Ups ?? (object)DBNull.Value);
                    //cmd.Parameters.Add("@MaintenanceCPU", SqlDbType.DateTime).Value = listKomputer.MaintenanceCPU ?? (object)DBNull.Value;
                    cmd.Parameters.AddWithValue("@MaintenanceCPU", listKomputer.MaintenanceCPU ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenanceMonitor", listKomputer.MaintenanceMonitor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenanceJaringan", listKomputer.MaintenanceJaringan ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenancePrinter", listKomputer.MaintenancePrinter ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenanceUPS", listKomputer.MaintenanceUPS ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Keterangan1", listKomputer.Keterangan1 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Keterangan2", listKomputer.Keterangan2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@namauser", listKomputer.NamaUser ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TanggalUpdate", DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss"));


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }
            }
        }
        //Update List Komputer
        public void UpdateListKomputer(ListKomputer listKomputer)
        {
            string Programstandar = string.Join(",", listKomputer.Programstandar);
            string Programimmanuel = string.Join(",", listKomputer.Programimmanuel);
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = @"UPDATE datakomputer 
                            SET Description = @Description, 
                                Computername = @Computername, 
                                Workgroup = @Workgroup, 
                                Useraccount = @Useraccount, 
                                Ipaddress = @Ipaddress, 
                                Access = @Access, 
                                Motherboard = @Motherboard, 
                                Os = @Os, 
                                Office = @Office, 
                                Hardisk = @Hardisk, 
                                Processor = @Processor, 
                                Ram = @Ram, 
                                Printer = @Printer, 
                                Lokasi = @Lokasi, 
                                Logo = @Logo, 
                                Programstandar = @Programstandar, 
                                Programimmanuel = @Programimmanuel, 
                                Avg = @Avg, 
                                Smadav = @Smadav, 
                                Usbblock = @Usbblock, 
                                Monitor = @Monitor, 
                                Mouse = @Mouse, 
                                Keyboard = @Keyboard, 
                                Cpu = @Cpu, 
                                Ups = @Ups, 
                                MaintenanceCPU = @MaintenanceCPU, 
                                MaintenanceMonitor = @MaintenanceMonitor, 
                                MaintenanceJaringan = @MaintenanceJaringan, 
                                MaintenancePrinter = @MaintenancePrinter, 
                                MaintenanceUPS = @MaintenanceUPS, 
                                Keterangan1 = @Keterangan1, 
                                Keterangan2 = @Keterangan2, 
                                namauser = @NamaUser, 
                                TanggalUpdate = @TanggalUpdate 
                            WHERE Id_Komputer = @Id_Komputer";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Id_Komputer", listKomputer.Id_Komputer);
                    cmd.Parameters.AddWithValue("@Description", listKomputer.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Computername", listKomputer.Computername ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Workgroup", listKomputer.Workgroup ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Useraccount", listKomputer.Useraccount ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ipaddress", listKomputer.Ipaddress ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Access", listKomputer.Access ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Motherboard", listKomputer.Motherboard ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Os", listKomputer.Os ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Office", listKomputer.Office ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Hardisk", listKomputer.Hardisk ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Processor", listKomputer.Processor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ram", listKomputer.Ram ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Printer", listKomputer.Printer ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Lokasi", listKomputer.Lokasi ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Logo", listKomputer.Logo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Programstandar", Programstandar ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Programimmanuel", Programimmanuel ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Avg", listKomputer.Avg ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Smadav", listKomputer.Smadav ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Usbblock", listKomputer.Usbblock ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Monitor", listKomputer.Monitor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mouse", listKomputer.Mouse ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Keyboard", listKomputer.Keyboard ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cpu", listKomputer.Cpu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ups", listKomputer.Ups ?? (object)DBNull.Value);
                    //cmd.Parameters.Add("@MaintenanceCPU", SqlDbType.DateTime).Value = listKomputer.MaintenanceCPU ?? (object)DBNull.Value;
                    cmd.Parameters.AddWithValue("@MaintenanceCPU", listKomputer.MaintenanceCPU ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenanceMonitor", listKomputer.MaintenanceMonitor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenanceJaringan", listKomputer.MaintenanceJaringan ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenancePrinter", listKomputer.MaintenancePrinter ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaintenanceUPS", listKomputer.MaintenanceUPS ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Keterangan1", listKomputer.Keterangan1 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Keterangan2", listKomputer.Keterangan2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@namauser", listKomputer.NamaUser ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TanggalUpdate", DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss"));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void DeleteListKomputer(String Id_Komputer)
        {
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "DELETE FROM DataKomputer WHERE Id_Komputer=" + Id_Komputer + "";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public IEnumerable<InstalledApps> GetApplicationList()
        {
            List<InstalledApps> list = new List<InstalledApps>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM DataAplikasi WHERE IsActive= 1";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new InstalledApps
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

        public IEnumerable<Workgroup> GetWorkgroupList()

        {
            List<Workgroup> list = new List<Workgroup>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT * FROM DataWorkgroup";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Workgroup
                    {
                        Id_Workgroup = reader["Id_Workgroup"].ToString(),
                        Nama_Workgroup = reader["Nama_Workgroup"].ToString()
                    });
                }
                con.Close();
            }
            return list;
        }
    }

    //========================Untuk mengakses History Data Komputer=================================

    public class ListHistoryKomputer
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id_Komputer")]
        public string Id_Komputer { set; get; }

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

        [Display(Name = "Aplikasi Standar")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public List<string> Programstandar { set; get; }

        [Display(Name = "Program Immanuel")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public List<string> Programimmanuel { set; get; }

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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceCPU { set; get; }

        [Display(Name = "Maintenance Monitor")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceMonitor { set; get; }

        [Display(Name = "Maintenance Jaringan")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceJaringan { set; get; }

        [Display(Name = "Maintenance Printer")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenancePrinter { set; get; }

        [Display(Name = "Maintenance UPS")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MaintenanceUPS { set; get; }

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
        public string ProgramstandarDisplay { set; get; }
        public string ProgramimmanuelDisplay { set; get; }
        public string NamaWorkgroup { set; get; }
    }

    public class ListHistoryKomputerContext
    {
        private readonly DBContext dbCont = new DBContext();



        //Get all ListHistoryKomputer to lists
        public IEnumerable<ListKomputer> GetAllListKomputers()
        {
            List<ListKomputer> list = new List<ListKomputer>();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string sqlQuery = "SELECT k.Id_Komputer,k.Description,k.Computername,k.Workgroup,k.Useraccount,k.Ipaddress,k.Access,k.Motherboard,k.Os,k.Office,k.Hardisk,k.Processor,k.Ram,k.Printer,k.Lokasi,k.Logo,k.programimmanuel,k.programstandar,wg.Nama_Workgroup,(SELECT STRING_AGG(a.Nama_Aplikasi, ',') FROM DataAplikasi a  WHERE a.Id_Aplikasi IN (SELECT value FROM STRING_SPLIT(k.Programstandar, ','))) AS Nama_Aplikasi,(SELECT STRING_AGG(p.Nama_Program, ',') FROM DataProgram p WHERE p.Id_Program IN (SELECT value FROM STRING_SPLIT(k.Programimmanuel, ','))) AS Nama_Program,k.Avg,k.Smadav,k.Usbblock,k.Monitor,k.Mouse,k.Keyboard,k.Cpu,k.Ups,k.MaintenanceCPU,k.MaintenanceMonitor,k.MaintenanceJaringan,k.MaintenancePrinter,k.MaintenanceUPS,k.Keterangan1,k.Keterangan2,k.NamaUser,k.TanggalUpdate FROM log_DataKomputer k LEFT JOIN DataWorkgroup wg ON k.Workgroup = wg.Id_Workgroup";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new ListKomputer
                    {
                        Id_Komputer = reader["Id_Komputer"].ToString(),
                        Description = reader["Description"].ToString(),
                        Computername = reader["Computername"].ToString(),
                        Workgroup = reader["Workgroup"].ToString(),
                        NamaWorkgroup = reader["Nama_Workgroup"].ToString(),
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
                        Programstandar = reader["Programstandar"].ToString().Split(',').ToList(),
                        Programimmanuel = reader["Programimmanuel"].ToString().Split(',').ToList(),
                        Avg = reader["Avg"].ToString(),
                        Smadav = reader["Smadav"].ToString(),
                        Usbblock = reader["Usbblock"].ToString(),
                        Monitor = reader["Monitor"].ToString(),
                        Mouse = reader["Mouse"].ToString(),
                        Keyboard = reader["Keyboard"].ToString(),
                        Cpu = reader["Cpu"].ToString(),
                        Ups = reader["Ups"].ToString(),
                        MaintenanceCPU = reader["MaintenanceCPU"] as DateTime?,
                        MaintenanceMonitor = reader["MaintenanceMonitor"] as DateTime?,
                        MaintenanceJaringan = reader["MaintenanceJaringan"] as DateTime?,
                        MaintenancePrinter = reader["MaintenancePrinter"] as DateTime?,
                        MaintenanceUPS = reader["MaintenanceUPS"] as DateTime?,
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
        public ListKomputer GetListKomputerData(string Id_Komputer)
        {
            ListKomputer ListKomputer = new ListKomputer();

            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                string SqlQuery = "SELECT k.Id_Komputer,k.Description,k.Computername,k.Workgroup,k.Useraccount,k.Ipaddress,k.Access,k.Motherboard,k.Os,k.Office,k.Hardisk,k.Processor,k.Ram,k.Printer,k.Lokasi,k.Logo,k.programimmanuel,k.programstandar,wg.Nama_Workgroup,(SELECT STRING_AGG(a.Nama_Aplikasi, ',')  FROM DataAplikasi a  WHERE a.Id_Aplikasi IN (SELECT value FROM STRING_SPLIT(k.Programstandar, ','))) AS Nama_Aplikasi,(SELECT STRING_AGG(p.Nama_Program, ',')  FROM DataProgram p  WHERE p.Id_Program IN (SELECT value FROM STRING_SPLIT(k.Programimmanuel, ','))) AS Nama_Program,k.Avg,k.Smadav,k.Usbblock,k.Monitor,k.Mouse,k.Keyboard,k.Cpu,k.Ups,k.MaintenanceCPU,k.MaintenanceMonitor,k.MaintenanceJaringan,k.MaintenancePrinter,k.MaintenanceUPS,k.Keterangan1,k.Keterangan2,k.NamaUser,k.TanggalUpdate FROM log_DataKomputer k JOIN DataWorkgroup wg ON k.Workgroup = wg.Id_Workgroup WHERE k.Id_Komputer = @Id_Komputer";

                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.Parameters.AddWithValue("@Id_Komputer", Id_Komputer);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListKomputer.Id_Komputer = reader["Id_Komputer"].ToString();  
                    ListKomputer.Description = reader["Description"].ToString();
                    ListKomputer.Computername = reader["Computername"].ToString();
                    ListKomputer.Workgroup = reader["Workgroup"].ToString();
                    ListKomputer.NamaWorkgroup = reader["Nama_Workgroup"].ToString();
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
                    ListKomputer.Programstandar = reader["Programstandar"].ToString().Split(',').ToList();
                    ListKomputer.Programimmanuel = reader["Programimmanuel"].ToString().Split(',').ToList();
                    ListKomputer.ProgramstandarDisplay = reader["Nama_Aplikasi"].ToString();
                    ListKomputer.ProgramimmanuelDisplay = reader["Nama_Program"].ToString();
                    ListKomputer.Avg = reader["Avg"].ToString();
                    ListKomputer.Smadav = reader["Smadav"].ToString();
                    ListKomputer.Usbblock = reader["Usbblock"].ToString();
                    ListKomputer.Monitor = reader["Monitor"].ToString();
                    ListKomputer.Mouse = reader["Mouse"].ToString();
                    ListKomputer.Keyboard = reader["Keyboard"].ToString();
                    ListKomputer.Cpu = reader["Cpu"].ToString();
                    ListKomputer.Ups = reader["Ups"].ToString();
                    ListKomputer.MaintenanceCPU = reader["MaintenanceCPU"] as DateTime?;
                    ListKomputer.MaintenanceMonitor = reader["MaintenanceMonitor"] as DateTime?;
                    ListKomputer.MaintenanceJaringan = reader["MaintenanceJaringan"] as DateTime?;
                    ListKomputer.MaintenancePrinter = reader["MaintenancePrinter"] as DateTime?;
                    ListKomputer.MaintenanceUPS = reader["MaintenanceUPS"] as DateTime?;
                    ListKomputer.Keterangan1 = reader["Keterangan1"].ToString();
                    ListKomputer.Keterangan2 = reader["Keterangan2"].ToString();
                    ListKomputer.NamaUser = reader["namauser"].ToString();
                    ListKomputer.TanggalUpdate = reader["TanggalUpdate"].ToString();
                }
                con.Close();
            }
            return ListKomputer;
        }
    }
}
