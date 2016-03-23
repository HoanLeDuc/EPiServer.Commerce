using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Commerce.Models.Catalog.Base;
using EPiServer.Commerce.Catalog.DataAnnotations;

namespace EPiServer.Commerce.Models.Catalog
{
     [CatalogContentType(DisplayName = "SiteCategoryContent", GUID = "135390f6-aa8d-4959-ad64-8fd6c41e6fb8", Description = "", MetaClassName = "DepartmentStoreLandingNode")]
    public class DepartmentStoreLandingNodeContent : SiteCategoryContent 
    {
        /*
                [CultureSpecific]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         */
    }
}