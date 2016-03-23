using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EPiServer.Commerce.Models.Blocks
{
    [ContentType(DisplayName = "Settings", GUID = "9715c1ea-b55b-4ff0-b7ef-43e83f6d3225", Description = "Block for settings")]
    public class SettingsBlock : BlockData
    {
       
                [CultureSpecific]
                [Display(
                    Name = "Cart page",
                    Description = "Shopping cart page",
                    GroupName = SystemTabNames.Settings,
                    Order = 1)]
                public virtual PageReference CartPage { get; set; }

        [Display(
            Name = "Wishlist Page",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
            public virtual PageReference WishlistPage { get; set; }

        [Display(
            Name = "Checkout page",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual PageReference CheckoutPage { get; set; }
        [Display(
            Name = "Payment container page",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual PageReference PaymentContainerPage { get; set; }
        [Display(
            Name = "Account page",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual PageReference AccountPage { get; set; }
        [Display(
            Name = "YourOrdersPage",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual PageReference YourOrdersPage { get; set; }
        [Display(
            Name = "Receipt page",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual PageReference ReceiptPage { get; set; }
        [Display(
            Name = "NewsletterUnsubscribePage",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual PageReference    NewsletterUnsubscribePage { get; set; }

        [Display(
            Name = "DeliveryAndReturns",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual XhtmlString DeliveryAndReturns { get; set; }
        [Display(
            Name = "Login page",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual ContentReference LoginPage { get; set; }


        [Display(
            Name = "Header scripts",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [UIHint(EPiServer.Web.UIHint.Textarea)]
        public virtual string HeaderScripts { get; set; }

    }
}