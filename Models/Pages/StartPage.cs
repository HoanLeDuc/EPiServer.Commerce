using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServer.Commerce.Models.Pages
{
    [ContentType(DisplayName = "Start Page", GUID = "c7e9a9d0-2535-4fcc-b89f-f94e91e03550", Description = "")]
    public class StartPage : PageData
    {
        [Display(
            Name = "Heading",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Heading { get; set; }

        [Display(
            Name = "Title format",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string TitleFormat { get; set; }
        [CultureSpecific]
        [Display(
            Name = "Main body",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString MainBody { get; set; }

        [Display(
            Name = "Main menu",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual LinkItemCollection MainMenu { get; set; }
        [Display(
            Name = "Right menu",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual LinkItemCollection RightMenu { get; set; }

        [Display(
            Name = "Product area",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual ContentArea ProductArea { get; set; }

        public virtual ContentReference CheckoutPage { get; set; }

        public virtual ContentReference WishListPage { get; set; }
        public virtual ContentReference SearchPage { get; set; }
    }
}