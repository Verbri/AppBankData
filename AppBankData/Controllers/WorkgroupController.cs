using AppBankData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBankData.Controllers
{
    public class WorkgroupController : Controller
    {
       
        private readonly WorkgroupContext _objListWorkgroup = new WorkgroupContext();

        public ActionResult Index()
        {
            ViewBag.Menu = "Workgroup";
            return View();
        }

        public ActionResult LoadDataWorkgroup()
        {
            var ListWorkgroup = _objListWorkgroup.GetAllWorkgroup().ToList();
            var list = ListWorkgroup.ToList();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workgroup workgroup)
        {

            if (ModelState.IsValid)
            {
                _objListWorkgroup.AddWorkgroup(workgroup);
                ViewBag.Message = String.Format("Data Workgroup sudah tersimpan");
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}