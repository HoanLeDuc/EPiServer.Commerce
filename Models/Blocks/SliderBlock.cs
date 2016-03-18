using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace EPiServer.Commerce.Models.Blocks
{
    [ContentType(DisplayName = "SliderBlock", GUID = "7021627a-b596-43e4-8b90-b4220822fabf", Description = "")]
    public class SliderBlock : SiteBlockData
    {
        [Display(Name="Carousel images", GroupName=SystemTabNames.Content)]
        [Searchable(false)]
        public virtual LinkItemCollection Images { get; set; }
        
        [ScaffoldColumn(false)]
        public string Tag { get { return string.Empty; } }
    }
}