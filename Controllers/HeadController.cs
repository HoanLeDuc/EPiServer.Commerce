using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.Pages;

namespace EPiServer.Commerce.Controllers
{
    public class HeadController : ActionControllerBase
    {
        private readonly IContentLoader _contentLoader;
        private readonly ContentRouteHelper _contentRouteHelper;
        private const string FormatPlaceHolder = "{title}";

        public HeadController(IContentLoader contentLoader, ContentRouteHelper contentRouteHelper)
        {
            _contentLoader = contentLoader;
            _contentRouteHelper = contentRouteHelper; 
        }

        public ActionResult Title() {
           
            var content = _contentRouteHelper.Content; 

            if(content == null) return Content(string.Empty);

            var product = content as EntryContentBase;
            if (product != null)
            {
                var title = string.Empty; 
                // Get the catalog of the EntryContent if any 
                var parentContent = _contentLoader.Get<CatalogContentBase>(product.ParentLink);
                var node = parentContent as NodeContent; // Cast to NodeContent 

                if (node != null)
                {
                    title = !string.IsNullOrEmpty(node.SeoInformation.Title) ? node.SeoInformation.Title : node.DisplayName;
                }
                else
                {
                    title = parentContent.Name; 
                }
                return Content(FormatTitle(string.Format("{0} - {1}", !string.IsNullOrEmpty(product.SeoInformation.Title) ? product.SeoInformation.Title : product.DisplayName, title))); 

            }

            var category = content as NodeContent;
            if (category != null) return Content(FormatTitle(!string.IsNullOrEmpty(category.SeoInformation.Title) ? category.SeoInformation.Title : category.DisplayName));

            var startPage = content as StartPage;
            if (startPage != null) return Content(FormatTitle(!string.IsNullOrEmpty(startPage.Heading) ? startPage.Heading : startPage.Name));

            return Content(content.Name); 
        }

        private string FormatTitle(string title)
        {
            var format = _contentLoader.Get<StartPage>(ContentReference.StartPage).TitleFormat;
            if (string.IsNullOrEmpty(format) || !format.Contains(FormatPlaceHolder)) return title;
            return title.Replace(FormatPlaceHolder, format); 
        }
    }
}
