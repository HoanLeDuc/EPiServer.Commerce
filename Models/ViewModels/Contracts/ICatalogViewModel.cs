using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
    public interface ICatalogViewModel<out T> : IBaseCatalogViewModel<T> where T : CatalogContentBase
    {
        Lazy<IEnumerable<NodeContent>> ChildCategories { get; set; }
        LazyProductViewModelCollection Products { get; set; }
        LazyProductViewModelCollection StyleProducts { get; set; }
        IEnumerable<IProductViewModel<ProductContent>> AllProductsSameStyle { get; set; }
        IEnumerable<IVariationViewModel<VariationContent>> AllVariationSameStyle { get; set; }
        IEnumerable<IProductViewModel<ProductContent>> RelatedProducts { get; set; }
        ContentArea RelatedProductsContentArea { get; set; }
        LazyVariationViewModelCollection Variants { get; set; }
        EntryContentBase ContentWithAssets { get; set; }
    }
}
