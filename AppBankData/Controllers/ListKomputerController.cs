using AppBankData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AppBankData.Controllers
{
    public class ListKomputerController : Controller
    {
         private readonly ListKomputerContext _objListKomputer = new ListKomputerContext();
        // GET: ListKomputer
        public ActionResult Index()
        {
            DBContext dbContext = new DBContext();
            string connectionString = dbContext.GetConnectionString();
            var builder = new SqlConnectionStringBuilder(connectionString);
            ViewBag.Database = builder.InitialCatalog;

            ViewBag.Menu = "ListKomputer";
            return View();
        }

        public ActionResult LoadData()
        {
            // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            //List<ListKomputer> lstListKomputer = new List<ListKomputer>();
            var lstListKomputer = _objListKomputer.GetAllListKomputers().ToList();
            var list = lstListKomputer.ToList();

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            ViewBag.lstApps = _objListKomputer.GetApplicationList().ToList();
            ViewBag.lstProgram = _objListKomputer.GetProgramList().ToList();
            ViewBag.lstWorkgroup = _objListKomputer.GetWorkgroupList().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListKomputer listKomputer)
        {
            ViewBag.lstApps = _objListKomputer.GetApplicationList().ToList();
            ViewBag.lstProgram = _objListKomputer.GetProgramList().ToList();
            ViewBag.lstWorkgroup = _objListKomputer.GetWorkgroupList().ToList();
            if (ModelState.IsValid)
            {
                _objListKomputer.AddKomputer(listKomputer);
                ViewBag.Message = String.Format("Data Komputer sudah tersimpan");
               
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return Redirect("~/Home/Index");
            }

            ListKomputer listKomputer = _objListKomputer.GetListKomputerData(id);
            //listKomputer.ProgramstandarDisplay = string.Join(", ", listKomputer.Programstandar);
            //listKomputer.ProgramimmanuelDisplay = string.Join(", ", listKomputer.ProgramimmanuelDisplay);

            if (listKomputer == null)
            {
                return Redirect("~/Home/Index");
            }
            return View(listKomputer);
        }

        //--Menampilkan Data yang akan di edit
        [HttpGet]
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return Redirect("~/Home/Index");
            }
            
            ListKomputer listKomputer = _objListKomputer.GetListKomputerData(id);
            // Gabungkan List<string> menjadi string yang dipisahkan koma

            ViewBag.lstApps = _objListKomputer.GetApplicationList().ToList();
            ViewBag.lstProgram = _objListKomputer.GetProgramList().ToList();
            ViewBag.lstWorkgroup = _objListKomputer.GetWorkgroupList().ToList();
            //listKomputer.ProgramstandarDisplay = string.Join(", ", listKomputer.Programstandar);
            //listKomputer.ProgramimmanuelDisplay = string.Join(", ", listKomputer.Programimmanuel);

            //var selectedProgramStandar = listKomputer.ProgramstandarDisplay.Split(',').Select(x => x.Trim()).ToList();
            //// Mengirimkan ke ViewBag untuk di-bind di dropdown

            //var selectedProgramImmanuel= listKomputer.ProgramimmanuelDisplay.Split(',').Select(x => x.Trim()).ToList();
            //// Mengirimkan ke ViewBag untuk di-bind di dropdown
            ViewBag.selectedProgramstandar = listKomputer.Programstandar;
            ViewBag.selectedProgramImmanuel = listKomputer.Programimmanuel;
            if (listKomputer == null)
            {
                return Redirect("~/Home/Index");
            }
            Console.WriteLine("Programstandar: " + ViewBag.selectedProgramstandar);
            Console.WriteLine("Programimmanuel: " + ViewBag.selectedProgramImmanuel);
            return View(listKomputer);
        }

        //--Proses Edit data komputer

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, [Bind] ListKomputer listKomputer)
        {
            if (id != listKomputer.Id)
            {
                return RedirectToAction("Index");
            }
            ViewBag.lstApps = _objListKomputer.GetApplicationList().ToList();
            ViewBag.lstProgram = _objListKomputer.GetProgramList().ToList();
            ViewBag.lstWorkgroup = _objListKomputer.GetWorkgroupList().ToList();
            ViewBag.selectedProgramstandar = listKomputer.Programstandar;
            ViewBag.selectedProgramImmanuel = listKomputer.Programimmanuel;
            if (ModelState.IsValid)
            {
                _objListKomputer.UpdateListKomputer(listKomputer);

                return RedirectToAction("Index");
            }

            return View(listKomputer);
        }
    }
}