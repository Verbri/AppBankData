using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppBankData.Models;

namespace AppBankData.Controllers
{
    public class ParameterController : Controller
    {
        private readonly ParameterContext _objParameter = new ParameterContext();
        // GET: Parameter
        public ActionResult Index()
        {
            ViewBag.Menu = "AppParameter";
            return View();
        }

        public ActionResult LoadData()
        {
            // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            //List<ListKomputer> lstListKomputer = new List<ListKomputer>();
            var lstParameter = _objParameter.GetAllParameter().ToList();
            var list = lstParameter.ToList();

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);

        }

        // GET: Parameter/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: Parameter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parameter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                _objParameter.AddParameter(parameter);
                ViewBag.Message = String.Format("Parameter sudah tersimpan");

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Parameter/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return Redirect("~/Home/Index");
            }

            Parameter parameter= _objParameter.DetailParameter(id);
            if (parameter == null)
            {
                return Redirect("~/Parameter/Index");
            }
            return View(parameter);
        }

        // POST: Parameter/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, [Bind]Parameter parameter)
        {
            if (id != parameter.ParameterID)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _objParameter.UpdateParameter(parameter);

                return RedirectToAction("Index");
            }

            return View(parameter);
        }

        // GET: Parameter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Parameter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
