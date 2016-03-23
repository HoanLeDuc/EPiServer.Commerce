using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.ViewModels;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Managers;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Infrastructure.Extensions
{
    public static class PriceExtensions
    {
        public static Injected<IPriceService> PriceService { get; set; }

        public static Price GetDefaultPriceMoney(this VariationContent content, IMarket market = null)
        {
            var prices = content.GetPrices();
            if (prices != null)
            {
                if (market != null)
                    return prices.FirstOrDefault(p => p.MarketId == market.MarketId && p.UnitPrice.Currency == market.DefaultCurrency && p.CustomerPricing.PriceTypeId == CustomerPricing.PriceType.AllCustomers);
                return prices.FirstOrDefault(p => p.CustomerPricing.PriceTypeId == CustomerPricing.PriceType.AllCustomers); 
            }
            return null; 
        }

        public static string GetDisplayPrice(this VariationContent content, IMarket market = null) {
            var price = content.GetDefaultPriceMoney(market);
            if (price != null) return price.UnitPrice.ToString();
            else return string.Empty; 
        }

        public static decimal GetDefaultPriceAmount(this VariationContent content, IMarket market = null) {
            var price = content.GetDefaultPriceMoney(market);
            if (price != null) return price.UnitPrice.Amount;
            return 0; 
        }

        public static decimal GetDiscountPrice(this VariationContent content, IMarket market = null) {
            var discount = StoreHelper.GetDiscountPrice(content.LoadEntry(CatalogEntryResponseGroup.ResponseGroup.CatalogEntryFull));
            if (discount != null) return discount.Money.Amount;
            return 0; 
        }

        public static List<PriceAndMarket> GetPricesWithMarket(this VariationContent content)
        {
            List<PriceAndMarket> priceAndMarkets = new List<PriceAndMarket>();

            var prices = content.GetPrices();
            if (prices == null) return priceAndMarkets; // Should log 

            foreach (var price in prices)
            {
                priceAndMarkets.Add(new PriceAndMarket()
                {
                    CurrencyCode = price.UnitPrice.Currency.CurrencyCode,
                    CurrencySymbol = price.UnitPrice.Currency.Format.CurrencySymbol,
                    MarketId = price.MarketId,
                    Price = price.ToString(),
                    PriceCode = price.CustomerPricing.PriceCode,
                    PriceTypeId = price.CustomerPricing.PriceTypeId.ToString(),
                    UnitPrice = price.UnitPrice
                }); 
            }

            return priceAndMarkets; 
        }
        //public static decimal GetDisplayPrice(this VariationContent content, IMarket market = null) {
            //var priceFilter = new PriceFilter(); 
            //priceFilter.CustomerPricing = new [] {new CustomerPricing().}
            //var price = PriceService.Service.GetPrices(market.MarketId, DateTime.Now, new CatalogKey(AppContext.Current.ApplicationId, content.Code)); 

        //}


    }
}