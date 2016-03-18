using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Commerce.SpecializedProperties;

namespace EPiServer.Commerce.Models.Media
{
    [ContentType(DisplayName = "ImageMediaType", GUID = "7b690f48-a823-4d6f-87e8-d57335560928", Description = "")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageMediaType : CommerceImage
    {
       
                [CultureSpecific]
               // [Editable(true)]
                [Display(
                    Name = "Description",
                    Description = "Description field's description",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual string Description { get; set; }

        [CultureSpecific]
                [Display(
                    Name = "Link",
                    GroupName = SystemTabNames.Content,
                    Order = 10)]
        [UIHint(UIHint.AllContent)]
                public virtual ContentReference Link { get; set; }
    }
}