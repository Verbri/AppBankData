using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppBankData.Models;

namespace AppBankData.Controllers
{
    [Authorize]

    public class UserController : Controller
    {
        
        private readonly UserContext _objListUser = new UserContext();
        private readonly RoleContext _objListRole = new RoleContext();

        // GET: User
        public ActionResult Index()
        {
            ViewBag.Menu = "User";
            return View();
        }

        public ActionResult LoadDataUser()
        {
            var ListUser = _objListUser.GetAllUsers().ToList();
            var list = ListUser.ToList();

            return Json(new
            { status = "Success", message = "Data Berhasil diambil", data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var bagianList = new List<SelectListItem>
    {
        new SelectListItem { Value = "SIM", Text = "SIM" },
    //    new SelectListItem { Value = "2", Text = "Bagian 2" },
    //    new SelectListItem { Value = "3", Text = "Bagian 3" }
    };
            ViewBag.BagianList = bagianList;
            ViewBag.RoleList = _objListRole.GetAllRoles(); // Ambil data dari model
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(User user)
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
                _objListUser.AddUser(user);
                // LOG: Cek apakah berhasil menyimpan
                Console.WriteLine("Data user berhasil disimpan.");
                response = new
                {
                    status = "success",
                    message = "Data User berhasil tersimpan",
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

            return Json(response);
        }

    }
}
