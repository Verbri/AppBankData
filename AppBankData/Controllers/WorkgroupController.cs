using AppBankData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBankData.Controllers
{
    [Authorize]
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
            
            return Json(new
           {status ="Success", message ="Data Berhasil diambil", data = list }, JsonRequestBehavior.AllowGet);

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
                try
                {
                    // Simpan data ke database
                    _objListWorkgroup.AddWorkgroup(workgroup);

                    // Cek apakah request berasal dari AJAX
                    if (Request.IsAjaxRequest())
                    {
                        // Respons JSON untuk permintaan AJAX
                        return Json(new
                        {
                            status = "success",
                            message = "Data Workgroup sudah tersimpan"
                        });
                    }

                    // Respons View untuk permintaan non-AJAX
                    TempData["Message"] = "Data Workgroup sudah tersimpan";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new
                        {
                            status = "error",
                            message = "Terjadi kesalahan saat menyimpan data",
                            error = ex.Message
                        });
                    }

                    ViewBag.ErrorMessage = "Terjadi kesalahan saat menyimpan data: " + ex.Message;
                }
            }
        
                
            return View();
        }
    }
}
