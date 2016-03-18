using EPiServer.Commerce.Models.Catalog;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Commerce.Models.ViewModels
{
    public class FashionProductViewModel
    {
        public FashionProduct Product { get; set; }
        public Money Price { get; set; }
        public Money OriginalPrice { get; set; }
        public FashionVariant Variation { get; set; }
        public IList<SelectListItem> Colors { get; set; }
        public IList<SelectListItem> Sizes { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public IList<string> Images { get; set; }
    }
}