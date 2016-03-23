using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using Mediachase.Commerce.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Infrastructure.Extensions; 

namespace EPiServer.Commerce.Models.ViewModels
{
    public class ProductListViewModel
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string NewItemText { get; set; }
        public string Description { get; set; }
        public ContentReference ContentLink { get; set; }
        public string PriceString { get; set; }
        public decimal PriceAmount { get; set; }
        public string DiscountPriceString { get; set; }
        public decimal DiscountPriceAmount { get; set; }
        public string CustomerClubMemberPriceString { get; set; }
        public double CustomerClubMemberPriceAmount { get; set; }
        public bool CustomerPriceAvailable { get; set; }
        public bool DiscountPriceAvailable { get; set; }


        public string BrandName { get; set; }
        public Dictionary<string, ContentReference> Images { get; set; }
        public Dictionary<string, string> Variants { get; set; }
        public string Country { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string ContentType { get; set; }

        public double AverageRating { get; set; }
        public List<string> AllImageUrls { get; set; }
        public string Overview { get; set; }
        public bool IsVariation { get; set; }
        public bool CurrentContactIsCustomerClubMember { get; set; }
        public bool InStock { get; set; }


        private readonly UrlResolver _urlResolver; 

        protected void PopulateCommonData(EntryContentBase content, IMarket market, CustomerContact contact) {
            Code = content.Code;
            ContentLink = content.ContentLink;
            DisplayName = content.DisplayName ?? content.Name;
            ProductUrl = _urlResolver.GetUrl(ContentLink);
            Overview = content.GetPropertyValue("Overview");
            Description = content.GetPropertyValue("Description");

            InStock = content.GetStock() > 0;
            ContentType = content.ContentType.GetType().Name;

            if (string.IsNullOrEmpty(Overview)) Overview = Description; 

        }

        protected void PopulatePrices(VariationContent content, IMarket market)
        {
            PriceString = content.GetDisplayPrice(market);
            PriceAmount = content.GetDefaultPriceAmount(market);

            DiscountPriceAmount = content.GetDiscountPrice(market);
            DiscountPriceAvailable = DiscountPriceAmount > 0; 
        }

        public ProductListViewModel()
        {
            _urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>(); 
        }

        public ProductListViewModel(ProductContent productContent, IMarket currentMarket, CustomerContact customerContact)
            : this() 
        {
            ImageUrl = productContent.GetDefaultImage();
            IsVariation = false;
            AllImageUrls = productContent.AssetsUrl();


            PopulateCommonData(productContent, currentMarket, customerContact); 
        }

        public ProductListViewModel(VariationContent variationContent, IMarket currentMarket, CustomerContact customerContact) : this() {
            IsVariation = true;
            ImageUrl = variationContent.GetDefaultImage();
            AllImageUrls = variationContent.AssetsUrl(); 

            PopulateCommonData(variationContent, currentMarket, customerContact); 
            PopulatePrices(variationContent, currentMarket); 
        }
    }
}