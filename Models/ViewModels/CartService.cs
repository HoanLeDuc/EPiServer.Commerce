using EPiServer.Commerce.Models.Cart;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Infrastructure.Extensions;
using EPiServer.Commerce.Catalog.ContentTypes;
using System.Globalization;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using Mediachase.Commerce.Orders;
using EPiServer.Commerce.Models.Catalog;
using Mediachase.Commerce;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Catalog.Managers;

namespace EPiServer.Commerce.Models.ViewModels
{
    [ServiceConfiguration(typeof(ICartService), Lifecycle = ServiceInstanceScope.Unique)]
    public class CartService : ICartService
    {
        private readonly Func<string, CartHelper> _cartHelper;
        private readonly IContentLoader _contentLoader;
        private readonly ReferenceConverter _referenceConverter;
        private readonly IPricingService _pricingService;
        private readonly IProductService _productService;
        private readonly UrlResolver _urlResolver;

        private string _cartName = Mediachase.Commerce.Orders.Cart.DefaultName;

        public CartService(Func<string, CartHelper> cartHelper, IContentLoader contentLoader, ReferenceConverter refererenceConverter, IPricingService pricingService, IProductService productService, UrlResolver urlResolver)
        {
            _cartHelper = cartHelper;
            _contentLoader = contentLoader;
            _referenceConverter = refererenceConverter;
            _pricingService = pricingService;
            _productService = productService;
            _urlResolver = urlResolver;
        }

        private CartHelper CartHelper
        {
            get
            {
                return _cartHelper(_cartName);
            }
        }

        public decimal GetLineItemsTotalQuantity()
        {
            return CartHelper.Cart.GetAllLineItems().Sum(x => x.Quantity); 
        }

        public IEnumerable<Cart.CartItem> GetCartItems()
        {
            if (CartHelper.IsEmpty) return Enumerable.Empty<CartItem>();

            var lineItems = CartHelper.Cart.GetAllLineItems();

            // Get variations from line items 
            var variants = _contentLoader.GetItems(lineItems.Select(item => _referenceConverter.GetContentLink(item.Code)), CultureInfo.InvariantCulture).OfType<VariationContent>();

            List<CartItem> cartItems = new List<CartItem>();
            foreach (var lineItem in lineItems)
            {
                VariationContent variation = variants.FirstOrDefault(x => x.Code == lineItem.Code);
                ProductContent product = _contentLoader.Get<ProductContent>(variation.GetParentProducts().FirstOrDefault());

                var cartItem = new CartItem
                {
                    Code = variation.Code,
                    DisplayName = variation.DisplayName,
                    ImageUrl = variation.GetAssets<IContentImage>(_contentLoader, _urlResolver).FirstOrDefault(),
                    ExtendedPrice = lineItem.ToMoney(lineItem.ExtendedPrice + lineItem.OrderLevelDiscountAmount),
                    PlacedPrice = lineItem.ToMoney(lineItem.PlacedPrice),
                    DiscountPrice = lineItem.ToMoney(Math.Round(((lineItem.PlacedPrice * lineItem.Quantity) - lineItem.Discounts.Cast<LineItemDiscount>().Sum(x => x.DiscountValue)) / lineItem.Quantity, 2)),
                    Quantity = lineItem.Quantity,
                    Variant = variation,
                    Url = _urlResolver.GetUrl(_referenceConverter.GetContentLink(lineItem.Code))
                };

                var fashionProduct = product as FashionProduct;
                if (fashionProduct != null)
                {
                    var fashionVariant = (FashionVariant)variation; 
                    cartItem.Brand = fashionProduct.Brand;
                    var fashionVariations = _productService.GetVariations(fashionProduct);
                    cartItem.AvailableSizes = fashionVariations.Where(x => x.Color == fashionVariant.Color).Select(x => x.Size); 
                }

                cartItems.Add(cartItem); 
            }

            
            return cartItems;
        }

        public Mediachase.Commerce.Money GetSubTotal()
        {
            var totalDiscount = CartHelper.Cart.OrderForms.SelectMany(o => o.Discounts.Cast<OrderFormDiscount>()).Sum(x => x.DiscountAmount);
            return ConvertToMoney(CartHelper.Cart.SubTotal + totalDiscount); 
        }

        public Mediachase.Commerce.Money GetTotal()
        {
            if (CartHelper.IsEmpty) return ConvertToMoney(0);

            return ConvertToMoney(CartHelper.Cart.Total); 
        }

        public Mediachase.Commerce.Money GetShippingSubTotal()
        {
            var shippingSubTotal = CartHelper.Cart.OrderForms.SelectMany(o => o.Shipments).Sum(ship => ship.ShippingSubTotal);
            return ConvertToMoney(shippingSubTotal); 
        }

        public Mediachase.Commerce.Money GetShippingTotal()
        {
            return ConvertToMoney(CartHelper.Cart.ShippingTotal); 
        }

        public Mediachase.Commerce.Money GetTaxTotal()
        {
            return ConvertToMoney(CartHelper.Cart.TaxTotal); 
        }

        public Mediachase.Commerce.Money GetShippingTaxTotal()
        {
            var shippingTaxTotal = CartHelper.Cart.ShippingTotal + CartHelper.Cart.TaxTotal;
            return ConvertToMoney(shippingTaxTotal); 
        }

        public Mediachase.Commerce.Money GetTotalDiscount()
        {
            var totalDiscount = CartHelper.Cart.OrderForms.SelectMany(o => o.Discounts.Cast<OrderFormDiscount>()).Sum(discount => discount.DiscountAmount);
            return ConvertToMoney(totalDiscount); 
        }

        public Mediachase.Commerce.Money GetShippingDiscountTotal()
        {
            var shippmentDiscount = CartHelper.Cart.OrderForms.SelectMany(o => o.Shipments).Sum(s => s.ShippingDiscountAmount);
            return ConvertToMoney(shippmentDiscount); 
        }

        public Mediachase.Commerce.Money GetOrderDiscountTotal()
        {
            var orderDiscount = CartHelper.Cart.OrderForms.SelectMany(o => o.Discounts.Cast<OrderFormDiscount>()).Sum(discount => discount.DiscountValue);
            return ConvertToMoney(orderDiscount); 

        }

        public Mediachase.Commerce.Money ConvertToMoney(decimal amount)
        {
            return new Money(amount, CartHelper.Cart.BillingCurrency); 
        }

        public bool AddToCart(string code,out string warningMessage)
        {
            var entry = CatalogContext.Current.GetCatalogEntry(code);
            CartHelper.AddEntry(entry);
            CartHelper.Cart.ProviderId = "frontend"; // if this is not set explicitly, place price does not get updated by workflow
            ValidateCart(out warningMessage);

            return CartHelper.LineItems.Any(x=> x.Code == code); 
        }

        private void ValidateCart()
        {
            string warningMessage;
            ValidateCart(out warningMessage); 
        }

        private void ValidateCart(out string warningMessage)
        {
            
            if (_cartName == CartHelper.WishListName) warningMessage = null;

            var workflowResult = OrderGroupWorkflowManager.RunWorkflow(CartHelper.Cart, OrderGroupWorkflowManager.CartValidateWorkflowName);
            var warnings = OrderGroupWorkflowManager.GetWarningsFromWorkflowResult(workflowResult);

            if (warnings.Any()) warningMessage = string.Join(" ", warnings);
            else warningMessage = null; 
        }

        public void ChangeQuantity(string code, decimal quantity)
        {
            if (quantity == 0)
            {
                RemoveLineItem(code);
            }
            else
            {
                var lineItem = CartHelper.Cart.GetLineItem(code);
                if (lineItem != null)
                {
                    lineItem.Quantity = quantity;
                    ValidateCart();
                    SaveCart(); 
                }
            }
        }

        public void RemoveLineItem(string code)
        {
            var lineItem = CartHelper.Cart.GetLineItem(code);
            if (lineItem != null) {
                PurchaseOrderManager.RemoveLineItemFromOrder(CartHelper.Cart, lineItem.Id);
                ValidateCart(); 
            }
        }

        public void SaveCart()
        {
            CartHelper.Cart.AcceptChanges(); 
        }

        public void DeleteCart()
        {
            CartHelper.Cart.Delete();
            SaveCart(); 
        }

        public void InitializeAsWishList()
        {
            _cartName = CartHelper.WishListName; 
        }

        public void UpdateLineItemSku(string oldCode, string newCode, decimal quantity)
        {
            var newLineItem = CartHelper.Cart.GetLineItem(newCode);
            // Merge quality 
            if (newLineItem != null)
            {
                RemoveLineItem(oldCode); 
                newLineItem.Quantity += quantity;
                newLineItem.AcceptChanges();
                ValidateCart();
                return;
            }

            var lineItem = CartHelper.Cart.GetLineItem(oldCode);
            var entry = CatalogContext.Current.GetCatalogEntry(newCode, new CatalogEntryResponseGroup(CatalogEntryResponseGroup.ResponseGroup.Variations));
            lineItem.Code = entry.ID;
            lineItem.MinQuantity = entry.ItemAttributes.MinQuantity;
            lineItem.MaxQuantity = entry.ItemAttributes.MaxQuantity;
            lineItem.InventoryStatus = (int)entry.InventoryStatus;

            var price = _pricingService.GetCurrentPrice(newCode);
            lineItem.ListPrice = price.Amount;
            lineItem.PlacedPrice = price.Amount;

            ValidateCart();
            lineItem.AcceptChanges(); 
        }


        public IEnumerable<OrderForm> GetOrderForms()
        {
            return !CartHelper.Cart.OrderForms.Any() ? new [] {new  OrderForm() } : CartHelper.Cart.OrderForms.ToArray(); 
        }
    }
}