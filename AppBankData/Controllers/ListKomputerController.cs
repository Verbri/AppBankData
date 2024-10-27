using AppBankData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBankData.Controllers
{
    public class ListKomputerController : Controller
    {
         private ListKomputerContext _objListKomputer = new ListKomputerContext();
        // GET: ListKomputer
        public ActionResult Index()
        {
            DBContext dbContext = new DBContext();
            string connectionString = dbContext.GetConnectionString();
            var builder = new SqlConnectionStringBuilder(connectionString);
            ViewBag.Database = builder.InitialCatalog;

            ViewBag.Menu = "Index";
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

            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("~/Home/Index");
            }

            ListKomputer listKomputer = _objListKomputer.GetListKomputerData(id);

            if (listKomputer == null)
            {
                return Redirect("~/Home/Index");
            }
            return View(listKomputer);
        }

        //--Menampilkan Data yang akan di edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return Redirect("~/Home/Index");
            }

            ListKomputer listKomputer = _objListKomputer.GetListKomputerData(id);

            if (listKomputer == null)
            {
                return Redirect("~/Home/Index");
            }

            return View(listKomputer);
        }

        //--Proses Edit data komputer

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] ListKomputer ListKomputer)
        {
            if (id != ListKomputer.Id)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {

                _objListKomputer.UpdateListKomputer(ListKomputer);

                return RedirectToAction("Index");
            }

            return View(ListKomputer);
        }
    }
}