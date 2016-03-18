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
        public ActionResult Index(HomePage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var pageViewModel = new PageViewModel<HomePage>(currentPage); 

            return View(pageViewModel);
        }
    }
}