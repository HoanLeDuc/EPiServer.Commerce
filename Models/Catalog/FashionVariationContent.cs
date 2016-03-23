using EPiServer.Commerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Catalog
{
    public class FashionVariationContent
    {
        [Id]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public string Size { get; set; }
        public List<PriceAndMarket> Prices { get; set; }
        public string Code { get; set; }
        public decimal Stock { get; set; }
    }
}