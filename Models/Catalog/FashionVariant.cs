using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;

namespace EPiServer.Commerce.Models.Catalog
{
    [CatalogContentType(DisplayName = "FashionVariant", GUID = "5c282d05-6e75-4285-b60b-7730b573a285", Description = "", MetaClassName = "Fashion_Variant")]
    public class FashionVariant : VariationContent
    {

        [CultureSpecific]
        [Display(
            Name = "Main body",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        [Tokenize]
        public virtual XhtmlString MainBody { get; set; }

        public virtual string Size { get; set; }
        public virtual string Color { get; set; }
        public virtual bool CanBeMonogrammed { get; set; }
    }
}