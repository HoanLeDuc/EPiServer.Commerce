using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Cart
{
    public class WishListMiniCartViewModel : CartViewModel
    {
        public ContentReference WishListPage { get; set; }
    }
}