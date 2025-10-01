using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppBankData
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            Telerik.Reporting.Services.WebApi.ReportsControllerConfiguration.RegisterRoutes(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            // Biarkan agent mengakses tanpa session
            if (url.Contains("/client/receivedata"))
            {
                return;
            }

            if (url.Contains("/client/downloadagent"))
            {
                return;
            }
            // Code that runs when a new session is started  
            if (((string)Session["NIK"] != "") && (Session["Nama"] != null))
            {
                //Redirect to Welcome Page if Session is not null  
                Response.Redirect("Home");

            }
            else
            {       //Redirect to Login Page if Session is null & Expires   
                Response.Redirect("~/Login");
            }

        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void Application_PreSendRequestHeaders()
        {
            var context = new HttpContextWrapper(Context);

            // Jika session habis & user diarahkan ke login saat request AJAX
            if (Context.Response.StatusCode == 302 && context.Request.IsAjaxRequest())
            {
                Context.Response.Clear();
                Context.Response.TrySkipIisCustomErrors = true;
                Context.Response.StatusCode = 401; // Unauthorized
            }
        }

    }
}
