using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppBankData.Models;

namespace AppBankData.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleContext _objListRole = new RoleContext();
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadDataRole()
        {
            var ListRole = _objListRole.GetAllRoles().ToList();
            var list = ListRole.ToList();

            return Json(new
            { status = "Success", message = "Data Berhasil diambil", data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            var response = new { status = "", message = "", errors = new string[] { } };

            try
            {
                if (!ModelState.IsValid)
                {
                    response = new
                    {
                        status = "validation_error",
                        message = "Validasi gagal",
                        errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToArray()
                    };
                    return Json(response);
                }

                // Simpan data ke database
                _objListRole.AddRole(role);
                Console.WriteLine("Data Role berhasil disimpan.");

                response = new
                {
                    status = "success",
                    message = "Data Role sudah tersimpan",
                    errors = new string[] { } // Tetap tambahkan errors agar tipe selalu konsisten
                };
            }
            catch (Exception ex)
            {
                response = new
                {
                    status = "error",
                    message = "Terjadi kesalahan saat menyimpan data",
                    errors = new string[] { ex.Message }
                };
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
