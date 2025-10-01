
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppBankData.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AppBankData.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserContext _User = new UserContext();
        // GET: Account

        public ActionResult Logout()
        {
            // Menghapus session
            Session.Clear();
            Session.Abandon();
            Session.Contents.RemoveAll();

            // Menghapus cookie autentikasi
            FormsAuthentication.SignOut();

            // Menonaktifkan cache pada browser
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetValidUntilExpires(false);

            // Menetapkan header untuk menonaktifkan caching
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, proxy-revalidate");

            // Mengarahkan user ke halaman login dan menghapus history browser
            Response.Write("<script language='javascript'>");
            Response.Write("history.pushState(null, null, window.location.href);");
            Response.Write("history.back();");
            Response.Write("history.forward();");
            Response.Write("window.location = '/Login/Index';");
            Response.Write("</script>");

            return new EmptyResult(); // Menghindari pengembalian tampilan
        }



        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Index(LoginView loginView, string returnUrl)
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


                var user = _User.AuthenticateUser(loginView.NIK, loginView.Password);


                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.NIK, false);
                    //var ticket = new FormsAuthenticationTicket(1, user.NIK, DateTime.Now, DateTime.Now.AddMinutes(20), false, "UserData");
                    //string encTicket = FormsAuthentication.Encrypt(ticket);
                    //HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    //Response.Cookies.Add(faCookie);
                    if (user.Status == "1")
                    {
                        Session["NIK"] = user.NIK;
                        Session["Nama"] = user.Nama;
                        Session["Bagian"] = user.Bagian;
                        Session["Role"] = user.Role;

                        string redirectUrl = string.IsNullOrEmpty(returnUrl)
                    ? Url.Action("Index", "Home")
                    : Url.IsLocalUrl(returnUrl) ? returnUrl : Url.Action("Index", "Home");

                        return Json(new { success = true, redirectUrl });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Akun Anda tidak aktif." });
                    }
                }
                else
                {
                    return Json(new { status = "error", message = "Username atau password salah." });
                }
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
