using EPiServer.Commerce.SpecializedProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    public class PriceModel
    {
        public Price Price { get; set; }
        public string DisplayPrice { get; set; }
        public string DiscountPrice { get; set; }

        public PriceModel(Price price)
        {
            if (price != null) Price = price;
            else Price = default(Price); 
        }
    }
}