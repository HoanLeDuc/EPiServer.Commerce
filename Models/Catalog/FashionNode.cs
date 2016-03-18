using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;

namespace EPiServer.Commerce.Models.Catalog
{
    [CatalogContentType(DisplayName = "FashionNode", GUID = "3e73761e-35b7-4284-ab4f-987fe9b299d2", Description = "", MetaClassName = "Fashion_Node")]
    public class FashionNode : NodeContent
    {

        [CultureSpecific]
        [Display(
            Name = "Main body",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        [Tokenize]
        public virtual XhtmlString MainBody { get; set; }

    }
}