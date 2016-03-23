using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    public class LazyVariationViewModelCollection : Lazy<IEnumerable<IVariationViewModel<VariationContent>>>
    {
        public LazyVariationViewModelCollection(Func<IEnumerable<IVariationViewModel<VariationContent>>> valueFactory)
            : base(valueFactory)
        {

        }
    }
}