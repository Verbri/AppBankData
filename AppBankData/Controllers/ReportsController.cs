namespace AppBankData.Views.Reports.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using AppBankData.Models;
    using Telerik.Reporting.Cache.File;
    using Telerik.Reporting.Services;
    using Telerik.Reporting.Services.WebApi;

	//The class name determines the service URL. 
	//ReportsController class name defines /api/reports/ service URL.

    public class ReportsController : ReportsControllerBase
    {
        static ReportServiceConfiguration configurationInstance;

        static ReportsController()
        {
            //This is the folder that contains the report definitions
            //In this case this is the Reports folder
            var appPath = HttpContext.Current.Server.MapPath("~/");
            //var reportsPath = Path.Combine(appPath, "Reports");
            var reportsPath = ConfigurationHelper.GetConfigurationValue("ReportFolder");

            if (string.IsNullOrEmpty(reportsPath) || !Directory.Exists(reportsPath))
            {
                throw new Exception($"Reports folder path '{reportsPath}' is invalid or does not exist.");
            }
            //Add resolver for trdx/trdp report definitions, 
            //then add resolver for class report definitions as fallback resolver; 
            //finally create the resolver and use it in the ReportServiceConfiguration instance.
            var resolver = new UriReportSourceResolver(reportsPath)
                .AddFallbackResolver(new TypeReportSourceResolver());

            //Setup the ReportServiceConfiguration
            configurationInstance = new ReportServiceConfiguration
                {
                    HostAppId = "Html5App",
                    Storage = new FileStorage(),
                    ReportSourceResolver = resolver,
                    // ReportSharingTimeout = 0,
                    // ClientSessionTimeout = 15,
                };
        }

        public ReportsController()
        {
			//Initialize the service configuration
            this.ReportServiceConfiguration = configurationInstance;
        }
    }
}
