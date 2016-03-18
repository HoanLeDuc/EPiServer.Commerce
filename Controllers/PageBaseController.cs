using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServer.Commerce.Models.ViewModels;
using EPiServer.ServiceLocation;
using EPiServer.Commerce.Business;


namespace EPiServer.Commerce.Controllers
{
    public class PageBaseController<T> : PageController<T> where T : PageData
    {
        private static Injected<IContentLoader> _contentLoaderService;

        protected IContentLoader ContentLoader
        {
            get { return _contentLoaderService.Service; }
        }

        protected T CurrentPage
        {
            get
            {
                return PageContext.Page as T;
            }
        }

        protected ContentReference StartPage
        {
            get {
                return ContentLoader.Get<IContent>(ContentReference.StartPage) as ContentReference; 
            }
        }

        public virtual IPageViewModel<PageData> CreatePageViewModel(PageData pageData)
        {
            var activator = new Activator<IPageViewModel<PageData>>();
            var model = activator.Activate(typeof(IPageViewModel<>), pageData);

            InitializePageViewModel(model);
            
            return model; 
        }

        protected void InitializePageViewModel<TViewModel>(TViewModel model) where TViewModel : IPageViewModel<PageData>
        {
            // TODO: Set other  values to TViewModel properties
        }
    }
}