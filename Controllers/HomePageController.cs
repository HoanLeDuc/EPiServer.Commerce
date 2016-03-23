using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServer.Commerce.Models.Pages;
using EPiServer.Commerce.Models.ViewModels;

namespace EPiServer.Commerce.Controllers
{
    public class HomePageController : PageBaseController<HomePage>
    {
        private readonly IContentLoader _contentLoader;

        public HomePageController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader; 
        }

        public ActionResult Index(PageData currentPage)
        {
            var virtualPath = string.Format("~/Views/{0}/Index.cshtml", currentPage.GetOriginalType().Name);
            if (!System.IO.File.Exists(Request.MapPath(virtualPath))) virtualPath = "Index"; 
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var pageViewModel = CreatePageViewModel(currentPage); 

            return View(virtualPath, pageViewModel);
        }
    }
}