using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using Castle.Core.Internal;
using EPiServer.DataAbstraction;
using System.ComponentModel; 

namespace EPiServer.Commerce.Models.Catalog.Base
{
    public abstract class ProductBase : ProductContent
    {
        [Display(
            Name = "Description",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual XhtmlString Description { get; set; }
        
        public override void SetDefaultValues(DataAbstraction.ContentType contentType)
        {
            PropertyInfo[] properties = this.GetType().BaseType.GetProperties();
            foreach (var property in properties)
            {
                var defaultValue = property.GetAttribute<DefaultValueAttribute>();
                if (defaultValue != null)
                    this[property.Name] = defaultValue.Value; 
            }
            base.SetDefaultValues(contentType);
        }
    }
}