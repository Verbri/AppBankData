using AppBankData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBankData.Controllers
{
    public class InstalledAppsProgramController : Controller
    {
        private readonly AppsProgramContext _objListApps = new AppsProgramContext();
        // GET: InstalledAppsProgram
        public ActionResult Index()
        {
            ViewBag.Menu = "Index";
            return View();
        }

        public ActionResult LoadDataApps()
        {
            // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            //List<ListKomputer> lstListKomputer = new List<ListKomputer>();
            var ListApps = _objListApps.GetAllInstalledApps().ToList();
            var list = ListApps.ToList();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult LoadDataProgram()
        {
            // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            //List<ListKomputer> lstListKomputer = new List<ListKomputer>();
            var ListProgram = _objListApps.GetAllInstalledApps().ToList();
            var list = ListProgram.ToList();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);

        }
    }
}