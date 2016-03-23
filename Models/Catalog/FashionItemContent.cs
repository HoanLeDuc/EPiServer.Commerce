using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAbstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Catalog
{
        [CatalogContentType(DisplayName = "Fashion Variant",
        GUID = "BE40A3E0-49BC-48DD-9C1D-819C2661C9BD",
        MetaClassName = "Fashion_Item_Class")]
    public class FashionItemContent : VariationContent 
    {
        [Display(
        Name = "Size",
        GroupName = SystemTabNames.Content,
        Order = 10)]
        public virtual string Facet_Size { get; set; }

        [Display(
        Name = "Color",
        GroupName = SystemTabNames.Content,
        Order = 10)]
        public virtual string Color { get; set; }
    }
}