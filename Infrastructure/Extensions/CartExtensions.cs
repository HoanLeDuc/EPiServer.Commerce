using Mediachase.Commerce.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCart = Mediachase.Commerce.Orders;

namespace EPiServer.Commerce.Infrastructure.Extensions
{
    public static class CartExtensions
    {
        public static IReadOnlyCollection<LineItem> GetAllLineItems(this MCart.Cart cart)
        {
            return cart.OrderForms.Any() ? cart.OrderForms.First().LineItems.ToList() : new List<LineItem>() { new LineItem() };
        }

        public static LineItem GetLineItem(this MCart.Cart cart, string code)
        {
            return cart.GetAllLineItems().Where(item => item.Code == code).FirstOrDefault(); 
        }

    }
}