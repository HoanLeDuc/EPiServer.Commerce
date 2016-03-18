using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Cart
{
    public class CartItem : IProductModel
    {
        public string Brand
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public Mediachase.Commerce.Money ExtendedPrice
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public Mediachase.Commerce.Money PlacedPrice
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public VariationContent Variant { get; set; }
        public decimal Quantity { get; set; }
        public Money DiscountPrice { get; set; }
        public IEnumerable<string> AvailableSizes { get; set; }
    }
}