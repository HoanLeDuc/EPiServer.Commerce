using EPiServer.Commerce.Models.Cart;
using Mediachase.Commerce;
using Mediachase.Commerce.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
   public  interface ICartService
    {
       decimal GetLineItemsTotalQuantity();
       IEnumerable<CartItem> GetCartItems();
       Money GetSubTotal();
       Money GetTotal();
       Money GetShippingSubTotal();
       Money GetShippingTotal();
       Money GetTaxTotal();
       Money GetShippingTaxTotal();
       Money GetTotalDiscount();
       Money GetShippingDiscountTotal();
       Money GetOrderDiscountTotal();

       Money ConvertToMoney(decimal amount);
       IEnumerable<OrderForm> GetOrderForms(); 
       bool AddToCart(string code, out string warningMessage);
       void ChangeQuantity(string code, decimal quantity);
       void RemoveLineItem(string code); 
       // RunWorkflow
       void SaveCart();
       void DeleteCart();
       void InitializeAsWishList();
       void UpdateLineItemSku(string oldCode, string newCode, decimal quantity);


    }
}
