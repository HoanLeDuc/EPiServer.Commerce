using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetVariationsAndPricesForProducts(IEnumerable<ProductContent> products);
        ProductViewModel GetProductViewModel(ProductContent product);
        ProductViewModel GetProductViewModel(VariationContent varation);
        IEnumerable<FashionVariant> GetVariations(FashionProduct currentContent); 

    }
}
