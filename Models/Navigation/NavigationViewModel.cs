using EPiServer.Commerce.Models.Cart;
using EPiServer.Commerce.Models.Pages;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Navigation
{
    public class NavigationViewModel 
    {
        public ContentReference CurrentContentLink { get; set; }
        public StartPage StartPage { get; set; }
        public LinkItemCollection UserLinks { get; set; }
        public MiniCartViewModel MiniCart { get; set; }
        public WishListMiniCartViewModel WishListMiniCart { get; set; }
    }
}