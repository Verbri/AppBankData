using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppBankData.Utilities;

namespace AppBankData.Controllers
{
    public class TestLogController : Controller
    {
        // GET: /TestLog/Write
        public ActionResult Write()
        {
            try
            {
                Logger.Log("Test log entry from TestLogController.");
                return Content("Log berhasil ditulis ke App_Data/Logs.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return Content("Gagal menulis log. Cek App_Data/Logs untuk detail.");
            }
        }

        // GET: /TestLog/Error
        public ActionResult Error()
        {
            try
            {
                throw new InvalidOperationException("Contoh error untuk test Logger.LogError");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return Content("Error berhasil dicatat di log.");
            }
        }
    }
}
