using System;
using System.Linq;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Commerce.Routing;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Http;
using Mediachase.Commerce;
using Mediachase.Commerce.Website.Helpers;
using EPiServer.Security;
using Mediachase.Commerce.Security;


namespace EPiServer.Commerce.Infrastructure
{
    //[InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class SiteInitialization : IConfigurableModule 
    {
        public void Initialize(InitializationEngine context)
        {
            //Add initialization logic, this method is called once after CMS has been initialized
            //CatalogRouteHelper.MapDefaultHierarchialRouter(RouteTable.Routes, false);

            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            AreaRegistration.RegisterAllAreas();

            //BundleConfig.RegisterBundles(BundleTable.Bundles); 
        }


        public void ConfigureContainer(ServiceConfigurationContext context) {

            context.Container.Configure(c => {
                c.For<Func<string, CartHelper>>()
                    .Use(() => new Func<string, CartHelper>((cartName) => new CartHelper(cartName, PrincipalInfo.CurrentPrincipal.GetContactId()))); 
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container)); 

            
        }
        
        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}