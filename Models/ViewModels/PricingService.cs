using EPiServer.Commerce.Infrastructure.Facades;
using EPiServer.Commerce.Models.Catalog;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Markets;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    [ServiceConfiguration(typeof(IPricingService), Lifecycle=ServiceInstanceScope.Singleton)]
    public class PricingService : IPricingService
    {
        private readonly IPriceService _priceService;
        private readonly ICurrentMarket _marketService;
        private readonly ICurrencyService _currencyService; 
        private readonly AppContextFacade _appContext; 

        public PricingService(IPriceService priceService, ICurrentMarket marketService, ICurrencyService currencyService, AppContextFacade appContext)
        {
            _priceService = priceService;
            _marketService = marketService;
            _currencyService = currencyService;
            _appContext = appContext; 
        }

        public IEnumerable<Mediachase.Commerce.Pricing.IPriceValue> GetPriceList(string code, Mediachase.Commerce.MarketId marketId, Mediachase.Commerce.Pricing.PriceFilter priceFilter)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException(code);
            var catalogKey = new CatalogKey(_appContext.ApplicationId, code); 
            return _priceService.GetPrices(marketId, DateTime.Now, catalogKey, priceFilter)
                .OrderBy(x => x.UnitPrice.Amount); 
        }

        public IEnumerable<Mediachase.Commerce.Pricing.IPriceValue> GetPriceList(IEnumerable<Mediachase.Commerce.Catalog.CatalogKey> catalogKeys, Mediachase.Commerce.MarketId marketId, Mediachase.Commerce.Pricing.PriceFilter priceFilter)
        {
            if (catalogKeys == null) throw new ArgumentNullException("Catalog keys");

            if (!catalogKeys.Any())
            {
                return Enumerable.Empty<IPriceValue>(); 
            }

            return _priceService.GetPrices(marketId, DateTime.Now, catalogKeys, priceFilter).OrderBy(p => p.UnitPrice.Amount); 
        }

        public Mediachase.Commerce.Money GetCurrentPrice(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException(code); 
            var currentCurrency = _currencyService.GetCurrentCurrency(); 

            var catalogKey = new CatalogKey(_appContext.ApplicationId, code);
            var prices = _priceService.GetPrices(_marketService.GetCurrentMarket().MarketId, DateTime.Now, catalogKey, new PriceFilter() { Currencies = new[] { currentCurrency } });
            return prices.Any() ? prices.FirstOrDefault().UnitPrice : new Money(0, currentCurrency); 
            
        }


        public IPriceValue GetdefaultPrice(FashionVariant variation, IMarket market, Currency currency)
        {
            return _priceService.GetDefaultPrice(market.MarketId, DateTime.Now, new CatalogKey(_appContext.ApplicationId, variation.Code), currency); 
        }
    }
}