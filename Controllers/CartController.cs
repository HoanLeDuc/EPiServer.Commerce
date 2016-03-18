using EPiServer.Commerce.Models.Cart;
using EPiServer.Commerce.Models.Pages;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartService _wishListCartService;
        private readonly IProductService _productService;
        private readonly IContentLoader _contentLoader;

        public CartController(IContentLoader contentLoader, IProductService productService, ICartService cartService, ICartService wishListCartService)
        {
            _contentLoader = contentLoader;
            _productService = productService;
            _cartService = cartService;
            _wishListCartService = wishListCartService; 
        }

        [AcceptVerbs("POST", "GET")]
        public ActionResult MiniCartDetails()
        {
            var viewModel = new MiniCartViewModel { 
                CartItems=_cartService.GetCartItems(),
                ItemCount = _cartService.GetLineItemsTotalQuantity() ,
                CheckoutPage = _contentLoader.Get<StartPage>(ContentReference.StartPage).CheckoutPage,
                Total = _cartService.GetTotal ()
            };

            return PartialView("_MiniCartDetails", viewModel); 
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddToCart(string code) { 
            string warningMessage;

            ModelState.Clear();


            if (_cartService.AddToCart(code, out warningMessage))
            {
                _wishListCartService.RemoveLineItem(code);
                return MiniCartDetails(); 
            }

            warningMessage = warningMessage.Length < 512 ? warningMessage : warningMessage.Substring(512);
            return new HttpStatusCodeResult(500, warningMessage); 
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}