using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    [ServiceConfiguration(typeof(IPromotionService), Lifecycle=ServiceInstanceScope.Singleton)]
    public class PromotionService : IPromotionService
    {
        private readonly IPricingService _pricingService;
        private readonly ICurrencyService _currencyService; 
        public PromotionService(IPricingService pricingService, ICurrencyService currencyService)
        {
            _pricingService = pricingService;
            _currencyService = currencyService; 
        }

        public IEnumerable<Mediachase.Commerce.Pricing.IPriceValue> GetDiscountPriceList(IEnumerable<Mediachase.Commerce.Catalog.CatalogKey> catalogKeys, Mediachase.Commerce.MarketId marketId, Mediachase.Commerce.Currency currency)
        {
            if (currency == null || currency == Currency.Empty) currency = _currencyService.GetAvailableCurrencies().First(); 
            var priceFilter = new PriceFilter() {
                CustomerPricing = new [] { CustomerPricing.AllCustomers }, 
                ReturnCustomerPricing = true, 
                Quantity=1,
                Currencies = new[] { currency}
            };
            return catalogKeys.SelectMany(p => _pricingService.GetPriceList(p.CatalogEntryCode, marketId, priceFilter)); 

        }

        public Mediachase.Commerce.Pricing.IPriceValue GetDiscountPrice(Mediachase.Commerce.Catalog.CatalogKey catalogKey, Mediachase.Commerce.MarketId marketId, Mediachase.Commerce.Currency currency)
        {
            var priceList =  GetDiscountPriceList(new[] { catalogKey }, marketId, currency);
            var zezoPrice = new PriceValue();
            zezoPrice.UnitPrice = new Money(0, currency); 

            return priceList.Any() ? priceList.FirstOrDefault() : zezoPrice; 
        }
    }
}