using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EPiServer.Commerce.Models.Blocks
{
    [ContentType(DisplayName = "CarouselBlock", GUID = "2acc85db-f268-494d-85b0-764216ea1987", Description = "Carousel block")]
    public class CarouselBlock : SiteBlockData
    {
       
                [CultureSpecific]
                [Display(
                    Name = "Carousel Content Area",
                    Description = "Name field's description",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual ContentArea CarouselContentArea { get; set; }
        
    }
}