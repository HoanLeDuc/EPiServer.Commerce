using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Cart
{
    public class MiniCartViewModel :CartViewModel
    {
        public ContentReference CheckoutPage { get; set; }
    }
}