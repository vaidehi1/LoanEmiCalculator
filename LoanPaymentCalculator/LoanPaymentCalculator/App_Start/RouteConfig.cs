using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LoanPaymentCalculator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Loan", action = "Loan", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "loan",
                url: "loan",
                defaults: new { controller = "Loan", action = "Loan"}
            );
            
            routes.MapRoute(
                name: "calculateAllInterestData",
                url: "Loan/calculateAllInterestData",
                defaults: new { controller = "Loan", action = "CalculateAllInterestData" }
            );

            routes.MapRoute(
                name: "saveLoanData",
                url: "Loan/saveLoanData",
                defaults: new { controller = "Loan", action = "SaveLoanData" }
            );
        }
    }
}
