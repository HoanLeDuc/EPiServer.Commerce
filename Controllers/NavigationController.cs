using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServer.Commerce.Models.Navigation;
using EPiServer.Commerce.Models.Pages;
using EPiServer.SpecializedProperties;
using EPiServer.Commerce.Models.Cart;
using EPiServer.Commerce.Models.ViewModels.Contracts;

namespace EPiServer.Commerce.Controllers
{
    public class NavigationController : Controller
    {
        private readonly IContentLoader _contentLoader;
        private readonly ICartService _cartService;
        private readonly UrlHelper _urlHelper;

        public NavigationController(IContentLoader contentLoader, UrlHelper urlHelper, ICartService cartService)
        {
            _contentLoader = contentLoader;
            _urlHelper = urlHelper;
            _cartService = cartService; 
        }


        public ActionResult Index(IContent currentContent)
        {
            var startPage = _contentLoader.Get<StartPage>(ContentReference.StartPage);
            var viewModel = new NavigationViewModel()
            {
                StartPage = startPage,
                CurrentContentLink = currentContent != null ? currentContent.ContentLink : null,
                UserLinks = new LinkItemCollection(),
                MiniCart = new MiniCartViewModel { 
                    CartItems = _cartService.GetCartItems(),
                    ItemCount = _cartService.GetLineItemsTotalQuantity(),
                    Total = _cartService.GetTotal(),
                    CheckoutPage = startPage.CheckoutPage 
                                  },
                WishListMiniCart = new WishListMiniCartViewModel {
                    //TODO: Add properties from WishList service 
                    WishListPage = startPage.WishListPage 
                }
            };

            if (Request.LogonUserIdentity.IsAuthenticated)
            {
                var rightMenuItems = startPage.RightMenu;
                if (rightMenuItems != null) viewModel.UserLinks.AddRange(rightMenuItems);

                viewModel.UserLinks.Add(new LinkItem
                {
                    Href = _urlHelper.Action("SignOut", "Login"),
                    Text = "Sign out"
                });
            }
            else
            {
                viewModel.UserLinks.Add(new LinkItem { 
                    Href = _urlHelper.Action("Index", "Login"),
                    Text = "Log In"
                });
            }

            return PartialView(viewModel); 
        }
    }
}