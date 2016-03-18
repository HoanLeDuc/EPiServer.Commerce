using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    [ServiceConfiguration(typeof(ICurrencyService), Lifecycle=ServiceInstanceScope.Singleton)]
    public class CurrencyService : ICurrencyService
    {
        private readonly string currentCurrency = "CurrentCurrency"; 
        private readonly ICurrentMarket _currentMarket;
        private readonly CookieService _cookieService;

        public CurrencyService(ICurrentMarket currentMarket, CookieService cookieService)
        {
            _currentMarket = currentMarket;
            _cookieService = cookieService; 
        }

        public IEnumerable<Mediachase.Commerce.Currency> GetAvailableCurrencies()
        {
            return _currentMarket.GetCurrentMarket().Currencies; 
        }

        public Mediachase.Commerce.Currency GetCurrentCurrency()
        {
            var currentCurrencyCode = _cookieService.Get(currentCurrency); 
            if (currentCurrencyCode == null) return _currentMarket.GetCurrentMarket().DefaultCurrency;
            else {
                return GetAvailableCurrencies().Where(c => c.CurrencyCode == currentCurrencyCode).FirstOrDefault(); 
            }
        }

        public void SetCurrentCurrency(string currencyCode)
        {
            SiteContext.Current.Currency = new Currency(currencyCode); 
        }
    }
}