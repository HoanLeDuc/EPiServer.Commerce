using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.Catalog;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Infrastructure.Extensions;
using EPiServer.Web.Routing;
using EPiServer.ServiceLocation; 

namespace EPiServer.Commerce.Models.FindModels
{
    public class FindProduct
    {
        private readonly Injected<UrlResolver> _urlResolver; 

        public string IndexId { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
     //   [StringFilter(DisplayName = "Display Name")]
        public string DisplayName { get; set; }
     //   [StringFilter]
        public string Language { get; set; }
        public XhtmlString Description { get; set; }
        public XhtmlString Overview { get; set; }
        public List<string> Color { get; set; }
        public List<string> Sizes { get; set; }
      //  [StringFilter]
        public string SizeUnit { get; set; }
      //  [StringFilter]
        public string SizeType { get; set; }
      //  [StringFilter]
        public string Fit { get; set; }
        public List<string> SizesList { get; set; }
        public List<string> ParentCategoryName { get; set; }
        public List<int> ParentCategoryId { get; set; }
      //  [StringFilter(DisplayName = "Main Category")]
        public string MainCategoryName { get; set; }
       // [StringFilter(DisplayName = "Category")]
        public string CategoryName { get; set; }
        public string DefaultImageUrl { get; set; }
        public string ProductUrl { get; set; }
       // [NumericFilter(DisplayName = "Default Inventory")]
        public decimal DefaultInventory { get; set; }

        /// <summary>
        /// Prices - used for filtering on range, is rounded to int
        /// </summary>
        public string DefaultPrice { get; set; }
       // [NumericFilter(DisplayName = "Defalt Price")]
        public int DefaultPriceAmount { get; set; }

        public string DiscountedPrice { get; set; }
       // [NumericFilter(DisplayName = "Discounted Price")]
        public double DiscountedPriceAmount { get; set; }

        public string CustomerClubPrice { get; set; }
       // [NumericFilter(DisplayName = "Customer Club Price")]
        public double CustomerClubPriceAmount { get; set; }

        public List<FashionVariationContent> Variants { get; set; }
        public List<GenericFindVariant> GenericVariants { get; set; }
        public string NewItemText { get; set; }

        public int SalesCounter { get; set; }

        public FindProduct()
        {

        }

        public FindProduct(EntryContentBase entryContentBase, string lang)
        {
            IndexId = entryContentBase.ContentLink.ID + "_" + lang;
            Id = entryContentBase.ContentLink.ID;
            Code = entryContentBase.Code;
            Name = entryContentBase.Name;
            DisplayName = entryContentBase.DisplayName;
            Language = lang;
            Description = Description ?? null;
            Overview = Overview ?? null;
            ParentCategoryId = entryContentBase.GetProductCategoryIds(lang);
            ParentCategoryName = entryContentBase.GetProductCategoryNames(lang);
            ProductUrl = _urlResolver.Service.GetUrl(entryContentBase.ContentLink, lang);
            DefaultImageUrl = entryContentBase.GetDefaultImage();
            DefaultInventory = entryContentBase.GetStock(); 
            
        }
    }
}