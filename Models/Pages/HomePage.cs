using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServer.Commerce.Models.Pages
{
    [ContentType(DisplayName = "Home Page", GUID = "ed7edb97-0884-49f2-9c4f-36d431e5ba56", Description = "")]
    public class HomePage : PageData
    {
    
                [CultureSpecific]
                [Display(
                    Name = "Main content",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual ContentArea MainContent { get; set; }
      
    }
}