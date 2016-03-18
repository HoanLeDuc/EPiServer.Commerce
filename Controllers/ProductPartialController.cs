using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.ViewModels.Contracts;

namespace EPiServer.Commerce.Controllers
{
    public class ProductPartialController : PartialContentController<ProductContent>
    {
        private readonly IProductService _productService;
        public ProductPartialController(IProductService productService)
        {
            _productService = productService; 
        }

        public override ActionResult Index(ProductContent currentContent)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var viewModel = _productService.GetProductViewModel(currentContent); 
            if (viewModel != null)
                return PartialView("_Product", viewModel ); 
            else return HttpNotFound(); 
        }
    }
}