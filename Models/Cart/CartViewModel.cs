using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Cart
{
    public abstract class CartViewModel
    {
        public decimal ItemCount { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public Money Total { get; set; }
    }
}