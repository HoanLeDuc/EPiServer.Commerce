using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EPiServer.Commerce.Models.Blocks
{
    //[ContentType(DisplayName = "SiteBlockData", GUID = "8a589b0e-5251-430b-ab9e-77effa6cad08", Description = "")]
    public abstract class SiteBlockData : BlockData
    {
        /*
                [CultureSpecific]
                [Display(
                    Name = "Name",
                    Description = "Name field's description",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual string Name { get; set; }
         */
    }
}