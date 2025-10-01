using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AppBankData.Utilities
{
    public static class Logger
    {
        private static readonly string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Logs");

        public static void Log(string message)
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logFile = Path.Combine(logDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log");

                using (StreamWriter sw = new StreamWriter(logFile, true))
                {
                    sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
                }
            }
            catch
            {
                // Jangan lempar error dari logger supaya tidak mengganggu proses utama
            }
        }

        public static void LogError(Exception ex)
        {
            Log($"ERROR: {ex.Message} | STACKTRACE: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Log($"INNER EXCEPTION: {ex.InnerException.Message}");
            }
        }
    }
}
