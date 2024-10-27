using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppBankData.Models
{
    public class DBContext
    {

        public string GetConnectionString()
        {
            string dbConn = ConfigurationManager.ConnectionStrings["SERVERDATA"].ConnectionString;
            return dbConn;
        }
    }
}