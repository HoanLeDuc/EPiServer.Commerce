using EPiServer.Commerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.FindModels
{
    public class GenericFindVariant
    {
        public string Size { get; set; }
        public List<PriceAndMarket> Prices { get; set; }

        public decimal Stock { get; set; }
    }
}