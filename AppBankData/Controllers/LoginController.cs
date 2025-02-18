
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppBankData.Models;

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
        public ActionResult  Index(LoginView loginView, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _User.AuthenticateUser(loginView.NIK, loginView.Password);
                
                
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.NIK, true);
                    if (user.Status == "1")
                    {
                        Session["NIK"] = user.NIK;
                        Session["Nama"] = user.Nama;
                        Session["Bagian"] = user.Bagian;
                        Session["Role"] = user.Role;

                        if (string.IsNullOrEmpty(returnUrl))
                        {

                            /*if (oUser.Password== passwordstandar)
                            {
                                return RedirectToAction("EditPassword", "User", new { id = Session["id"] });

                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }*/
                            return RedirectToAction("Index", "Home");
                        }
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Akun Anda tidak aktif.");
                        Console.WriteLine("User Status (from LoginController): " + user.Status);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login gagal. Periksa username dan password.");
                }
            }
            return View(loginView);
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
