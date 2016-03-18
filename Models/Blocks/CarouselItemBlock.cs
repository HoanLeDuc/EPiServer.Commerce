using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace EPiServer.Commerce.Models.Blocks
{
    [ContentType(DisplayName = "CarouselItemBlock", GUID = "96dcc59a-2cc2-4344-9d7f-3e0061b786b9", Description = "Carousel item block")]
    public class CarouselItemBlock : SiteBlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Image Url",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        //[UIHint(EPiServer.Web.UIHint.Image)]
        public virtual Url Image { get; set; }

        [Display(
            Name = "Description",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string ImageDescription
        {
            get
            {
                var propertyValue = this["ImageDescription"] as string;
                return string.IsNullOrEmpty(propertyValue) ? Heading : propertyValue;
            }
            set { this.SetPropertyValue(x => x.ImageDescription, value); }
        }

        [Display(
            Name = "Heading",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Heading { get; set; }

        [Display(
      Name = "Sub Heading",
      GroupName = SystemTabNames.Content,
      Order = 10)]
        public virtual string SubHeading { get; set; }

    }
}