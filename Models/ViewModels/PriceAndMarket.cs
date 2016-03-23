using Mediachase.Commerce;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    public class PriceAndMarket
    {
        public string Price { get; set; }
        [JsonIgnore]
        public Money UnitPrice { get; set; }
        public string MarketId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string PriceTypeId { get; set; }
        public string PriceCode { get; set; }
    }
}