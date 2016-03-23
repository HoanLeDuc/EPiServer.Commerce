using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
    public interface IVariationViewModel<out TVariationContent>: IBaseCatalogViewModel<TVariationContent> where TVariationContent:VariationContent
    {
        Lazy<Inventory> Inventory { get; set; }
        Price Price { get; set; }
        EntryContentBase ParentEntry { get; set; }
        EntryContentBase ContentWithAssets { get; set; }

        Lazy<IEnumerable<WarehouseInventoryViewModel>> AllWarehouseInventory { get; set; }

        PriceModel PriceViewModel { get; set; }
        IEnumerable<IVariationViewModel<VariationContent>> AllVariationSameStyle { get; set; }
        bool IsSellable { get; set; }
        List<MediaData> Media { get; set; }

        //TODO: Add Cart Item 
        ContentArea RelatedProductsContentArea { get; set; }
    }
}
