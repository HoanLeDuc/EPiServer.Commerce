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
using EPiServer.Commerce.Models.Pages;
using EPiServer.Web;
using EPiServer.Web.Routing;


namespace EPiServer.Commerce.Controllers
{
    public class PageBaseController<T> : PageController<T> where T : PageData
    {
        private static Injected<IContentLoader> _contentLoaderService;
        private  UrlResolver _urlResolver; 

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
            var model = activator.Activate(typeof(PageViewModel<>), pageData);

            InitializePageViewModel(model);
            
            return model; 
        }

        protected void InitializePageViewModel<TViewModel>(TViewModel model) where TViewModel : IPageViewModel<PageData>
        {
            _urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>(); 
            // TODO: Set other  values to TViewModel properties
            if (!ContentReference.IsNullOrEmpty(ContentReference.StartPage))
            {
                var startPage = ContentLoader.Get<HomePage>(ContentReference.StartPage);

                model.TopLeftMenu = model.TopLeftMenu ?? startPage.TopLeftMenu;
                model.TopRightMenu = model.TopRightMenu ?? startPage.TopRightMenu;
                model.SocialMediaIcons = model.SocialMediaIcons ?? startPage.SocialMediaIcons;
                if (model.CurrentPage != null)
                    model.Section = model.Section ?? GetSection(model.CurrentPage.ContentLink);
                else model.Section = model.Section ?? GetSection(startPage.ContentLink);

                model.LogoImage = model.LogoImage ?? _urlResolver.GetUrl(startPage.LogoImage); 
            }
        }

        protected IContent GetSection(ContentReference contentLink)
        {
            var currentContent = ContentLoader.Get<IContent>(contentLink);

            if (currentContent.ParentLink != null && currentContent.ParentLink.CompareToIgnoreWorkID(ContentReference.StartPage)) return currentContent;

            return ContentLoader.GetAncestors(contentLink).OfType<PageData>().SkipWhile(x => x.ParentLink == null || !x.ParentLink.CompareToIgnoreWorkID(ContentReference.StartPage)).FirstOrDefault(); 
        }
    }
}