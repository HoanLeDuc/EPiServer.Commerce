using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Commerce.Models.Blocks;

namespace EPiServer.Commerce.Models.Pages
{
    [ContentType(DisplayName = "Home Page", GUID = "ed7edb97-0884-49f2-9c4f-36d431e5ba56", Description = "The start page of the site")]
    public class HomePage : SitePage
    {

        [CultureSpecific]
        [Display(
            Name = "Main content",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentArea MainContent { get; set; }

        [Display(
            Name = "TopLeftMenu",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        public virtual LinkItemCollection TopLeftMenu { get; set; }
        [Display(
            Name = "TopRightMenu",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        public virtual LinkItemCollection TopRightMenu { get; set; }

        [Display(
            Name = "Logo Image",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        [UIHint(EPiServer.Web.UIHint.Image)]
        public virtual ContentReference LogoImage { get; set; }

        [Display(
            Name = "Global footer content",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        public virtual ContentArea GlobalFooterContent { get; set; }
        [Display(
            Name = "FooterMenuFolder",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        public virtual PageReference FooterMenuFolder { get; set; }
        [Display(
            Name = "Social media",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        public virtual ContentArea SocialMediaIcons { get; set; }
        [Display(
            Name = "Setting",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        public virtual SettingsBlock Settings{ get; set; }
    }
}