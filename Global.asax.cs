
using EPiServer.Commerce.Infrastructure;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.UI;

namespace EPiServer.Commerce
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
           // AreaRegistration.RegisterAllAreas();

            
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
           {
               Path = "~/Scripts/jquery-1.11.1.js",
           });

           BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Tip: Want to call the EPiServer API on startup? Add an initialization module instead (Add -> New Item.. -> EPiServer -> Initialization Module)
        }

        protected override void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            routes.MapRoute(
         name: "Default",
         url: "{controller}/{action}/{id}",
         defaults: new { action = "Index", id = UrlParameter.Optional });
        }
    }
}