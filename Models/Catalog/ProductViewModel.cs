using EPiServer.Commerce.Models.ViewModels.Contracts;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.Catalog
{
    public class ProductViewModel : IProductModel
    {
        public string Brand
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public Money ExtendedPrice
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public Money PlacedPrice
        {
            get;
            set;
        }


        public string Url
        {
            get;
            set;
        }
    }
}