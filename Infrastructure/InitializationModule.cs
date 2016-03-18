using System.Web.Routing;
using EPiServer.Commerce.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Core;

namespace EPiServer.Commerce.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            CatalogRouteHelper.MapDefaultHierarchialRouter(RouteTable.Routes, false);
            //MapRoutes(RouteTable.Routes); 
        }

        private static void MapRoutes(RouteCollection routes)
        {
            var referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var partialRouteHandler = ServiceLocator.Current.GetInstance<PartialRouteHandler>(); 

            var commerceRootContent = contentLoader.Get<CatalogContentBase>(referenceConverter.GetRootLink());
            var hierarchicalCatalogPartialRouter = new HierarchicalCatalogPartialRouter(() => SiteDefinition.Current.StartPage, commerceRootContent, false);
            var partialRoute = new PartialRouter<PageData, CatalogContentBase>(hierarchicalCatalogPartialRouter);
            partialRouteHandler.RegisterPartialRouter(partialRoute); 
        }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
