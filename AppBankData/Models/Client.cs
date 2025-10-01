using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AppBankData.Models
{
    public class Client
    {
        [Display(Name = "Id_Komputer")]
        public string Id_Komputer { set; get; }

        [Display(Name = "Deskripsi")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Description { set; get; }

        [Display(Name = "Lokasi")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Lokasi { set; get; }
        public string ComputerName { get; set; }
        public string IP { get; set; }
        public string OS { get; set; }
        public string CPU { get; set; }
        public string Workgroup { get; set; }
        public string RAM { get; set; }
        public string Disks { get; set; }
        public string TotalDiskSize { get; set; }
        public string MotherboardManufacturer { get; set; }
        public string MotherboardProduct { get; set; }
        public string MotherboardSerialNumber { get; set; }
        public string WindowsProductKey { get; set; }
        public string VersionAgent { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public class RenameCommand
    {
        public Guid Id { get; set; }
        public string ComputerName { get; set; }
        public string NewName { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }

    public class AckCommandRequest
    {
        // Pastikan nama properti ini "commandId" (case-insensitive)
        // agar cocok dengan JSON yang dikirim agen Python: {"commandId": "..."}
        public Guid CommandId { get; set; }
    }

    public class ClientContext
    {
        private readonly DBContext dbCont = new DBContext();

        public IEnumerable<Client> GetAll()
        {
            List<Client> list = new List<Client>();
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM DataClient ORDER BY LastUpdate DESC", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Client
                        {
                            Description = reader["Description"].ToString(),
                            Lokasi = reader["Lokasi"].ToString(),
                            ComputerName = reader["ComputerName"].ToString(),
                            IP = reader["IP"].ToString(),
                            OS = reader["OS"].ToString(),
                            CPU = reader["CPU"].ToString(),
                            Workgroup = reader["Workgroup"].ToString(),
                            RAM = reader["RAM"].ToString(),
                            Disks = reader["Disks"].ToString(),
                            TotalDiskSize = reader["TotalDiskSize"].ToString(),
                            MotherboardManufacturer = reader["MotherboardManufacturer"].ToString(),
                            MotherboardProduct = reader["MotherboardProduct"].ToString(),
                            MotherboardSerialNumber = reader["MotherboardSerialNumber"].ToString(),
                            WindowsProductKey = reader["WindowsProductKey"].ToString(),
                            VersionAgent = reader["VersionAgent"].ToString(),
                            LastUpdate = Convert.ToDateTime(reader["LastUpdate"])
                        });
                    }
                }
                con.Close();
            }
            return list;
        }

        // ✅ Simpan (insert / replace) data client
        public void Save(Client client)
        {
            using (SqlConnection con = new SqlConnection(dbCont.GetConnectionString()))
            {
                con.Open();

                // Hapus data lama kalau komputer sama sudah ada
                var delCmd = new SqlCommand("DELETE FROM DataClient WHERE ComputerName = @ComputerName", con);
                delCmd.Parameters.AddWithValue("@ComputerName", client.ComputerName);
                delCmd.ExecuteNonQuery();

                // Insert baru
                var insertCmd = new SqlCommand(@"
                    INSERT INTO DataClient
                    (ComputerName,Description, Lokasi, IP, OS, CPU, Workgroup, RAM, Disks, TotalDiskSize,
                     MotherboardManufacturer, MotherboardProduct, MotherboardSerialNumber, 
                     WindowsProductKey, VersionAgent, LastUpdate)
                    VALUES
                    (@ComputerName, @Description, @Lokasi, @IP, @OS, @CPU, @Workgroup, @RAM, @Disks, @TotalDiskSize,
                     @MotherboardManufacturer, @MotherboardProduct, @MotherboardSerialNumber, 
                     @WindowsProductKey, @VersionAgent, @LastUpdate)", con);
                insertCmd.Parameters.AddWithValue("@Description", client.Description ?? "");
                insertCmd.Parameters.AddWithValue("@Lokasi", client.Lokasi ?? "");
                insertCmd.Parameters.AddWithValue("@ComputerName", client.ComputerName ?? "");
                insertCmd.Parameters.AddWithValue("@IP", client.IP ?? "");
                insertCmd.Parameters.AddWithValue("@OS", client.OS ?? "");
                insertCmd.Parameters.AddWithValue("@CPU", client.CPU ?? "");
                insertCmd.Parameters.AddWithValue("@Workgroup", client.Workgroup ?? "");
                insertCmd.Parameters.AddWithValue("@RAM", client.RAM ?? "");
                insertCmd.Parameters.AddWithValue("@Disks", client.Disks ?? "");
                insertCmd.Parameters.AddWithValue("@TotalDiskSize", client.TotalDiskSize ?? "");
                insertCmd.Parameters.AddWithValue("@MotherboardManufacturer", client.MotherboardManufacturer ?? "");
                insertCmd.Parameters.AddWithValue("@MotherboardProduct", client.MotherboardProduct ?? "");
                insertCmd.Parameters.AddWithValue("@MotherboardSerialNumber", client.MotherboardSerialNumber ?? "");
                insertCmd.Parameters.AddWithValue("@WindowsProductKey", client.WindowsProductKey ?? "");
                insertCmd.Parameters.AddWithValue("@VersionAgent", client.VersionAgent ?? "");
                insertCmd.Parameters.AddWithValue("@LastUpdate", client.LastUpdate);

                insertCmd.ExecuteNonQuery();
            }
        }

        public RenameCommand GetPendingRename(string computerName)
        {
            using (var con = new SqlConnection(dbCont.GetConnectionString()))
            {
                con.Open();
                var cmd = new SqlCommand(@"
                SELECT TOP 1 Id, ComputerName, NewName, IsProcessed, CreatedAt, ProcessedAt
                FROM RenameQueue
                WHERE ComputerName = @ComputerName AND IsProcessed = 0
                ORDER BY CreatedAt DESC", con);
                cmd.Parameters.AddWithValue("@ComputerName", computerName);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new RenameCommand
                        {
                            Id = r.GetGuid(0),
                            ComputerName = r.GetString(1),
                            NewName = r.GetString(2),
                            IsProcessed = r.GetBoolean(3),
                            CreatedAt = r.GetDateTime(4),
                            ProcessedAt = r.IsDBNull(5) ? (DateTime?)null : r.GetDateTime(5)
                        };
                    }
                }
            }
            return null;
        }

        public void EnqueueRename(string computerName, string newName)
        {
            using (var con = new SqlConnection(dbCont.GetConnectionString()))
            {
                con.Open();
                var cmd = new SqlCommand(@"
                INSERT INTO RenameQueue (ComputerName, NewName) VALUES (@ComputerName, @NewName)", con);
                cmd.Parameters.AddWithValue("@ComputerName", computerName);
                cmd.Parameters.AddWithValue("@NewName", newName);
                cmd.ExecuteNonQuery();
            }
        }

        public void AckRename(Guid commandId)
        {
            using (var con = new SqlConnection(dbCont.GetConnectionString()))
            {
                con.Open();
                var cmd = new SqlCommand(@"
            UPDATE RenameQueue 
            SET IsProcessed = 1, ProcessedAt = GETDATE() 
            WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", commandId);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    // Optional: log atau throw exception jika Id tidak ditemukan
                    throw new Exception($"Rename command with Id {commandId} not found.");
                }
            }
        }
    }
}
