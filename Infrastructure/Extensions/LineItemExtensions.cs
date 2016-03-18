using Mediachase.Commerce;
using Mediachase.Commerce.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Infrastructure.Extensions
{
    public static class LineItemExtensions
    {
        public static Money ToMoney(this LineItem lineItem, decimal amount)
        {
            return lineItem.Parent.Parent.ToMoney(amount); 
        }
    }

    public static class OrderGroupExtensions
    {
        public static Money ToMoney(this OrderGroup orderGroup, decimal amount)
        {
            return new Money(amount, orderGroup.BillingCurrency); 
        }
    }
}