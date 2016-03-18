using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.SpecializedProperties;

namespace EPiServer.Commerce.Models.Catalog
{
    [CatalogContentType(DisplayName = "Fashion products", GUID = "664a6830-ef2f-41a6-9a2a-2d81385e2302", Description = "", MetaClassName = "Fashion_Product")]
    public class FashionProduct : ProductContent
    {

        [CultureSpecific]
        [Display(
            Name = "Main body",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        [Tokenize]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            Name = "Description",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [CultureSpecific]
        [Tokenize]
        [Searchable]
        public virtual XhtmlString Description { get; set; }

        [Searchable]
        public virtual string ClothesType { get; set; }
        [Searchable]
        public virtual string Brand { get; set; }

        [Display(
            Name = "Sizing",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [Tokenize]
        public virtual XhtmlString Sizing { get; set; }

        [Display(
            Name = "Product teaser",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual XhtmlString ProductTeaser { get; set; }
        [Display(
            Name = "Available sizes",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [BackingType(typeof(PropertyDictionaryMultiple))]
        public virtual ItemCollection<string> AvailableSizes { get; set; }

        [Display(
            Name = "Available in colors",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [BackingType(typeof(PropertyDictionaryMultiple))]
        public virtual ItemCollection<string> AvailableColors { get; set; }
    }
}