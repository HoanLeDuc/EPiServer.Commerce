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
    public interface IPromotionService
    {
        IEnumerable<IPriceValue> GetDiscountPriceList(IEnumerable<CatalogKey> catalogKeys, MarketId marketId, Currency currency);
        IPriceValue GetDiscountPrice(CatalogKey catalogKey, MarketId marketId, Currency currency); 
    }
}
