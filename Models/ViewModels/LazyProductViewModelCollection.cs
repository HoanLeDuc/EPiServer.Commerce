using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    public class LazyProductViewModelCollection : Lazy<IEnumerable<IProductViewModel<ProductContent>>>
    {
        public LazyProductViewModelCollection(Func<IEnumerable<IProductViewModel<ProductContent>>> valueFactory) : base(valueFactory)
        {

        }
    }
}