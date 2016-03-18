using EPiServer.Commerce.Models.Catalog;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
    public interface IPricingService
    {
        IEnumerable<IPriceValue> GetPriceList(string code, MarketId marketId, PriceFilter priceFilter);
        IEnumerable<IPriceValue> GetPriceList(IEnumerable<CatalogKey> catalogKeys, MarketId marketId, PriceFilter priceFilter);
        IPriceValue GetdefaultPrice(FashionVariant variant, IMarket market, Currency currency); 
        Money GetCurrentPrice(string code); 

    }
}
