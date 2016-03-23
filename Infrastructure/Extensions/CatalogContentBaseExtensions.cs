using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Inventory;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Infrastructure.Extensions
{
    public static class CatalogContentBaseExtensions
    {
        public static Injected<ILinksRepository> LinkRepository { get; set; }
        public static Injected<ReferenceConverter> ReferenceConverter { get; set; }
        public static Injected<IContentLoader> ContentLoader { get; set; }

        public static List<int> GetProductCategoryIds(this CatalogContentBase productContent, string lang)
        {
            return GetProductCategories(productContent, lang).Select(p => p.CatalogId).ToList(); 
        }

        public static List<string> GetProductCategoryNames(this CatalogContentBase productContent, string lang)
        {
            return productContent.GetProductCategories(lang).Select(c => c.Name).ToList(); 
        }

        public static List<CatalogContentBase> GetProductCategories(this CatalogContentBase productContent, string lang)
        {
            var allRelations = LinkRepository.Service.GetRelationsBySource(productContent.ContentLink);
            var categories = allRelations.OfType<NodeRelation>();

            List<CatalogContentBase> parentCategories = new List<CatalogContentBase>();

            try {
                if (categories.Any())
                {
                    foreach (var nodeRelation in categories)
                    {
                        if (nodeRelation.Target != ReferenceConverter.Service.GetRootLink())
                        {
                            var parentCategory = ContentLoader.Service.Get<CatalogContentBase>(nodeRelation.Target, new LanguageSelector(lang));
                            if (parentCategory != null && parentCategory.ContentType != CatalogContentType.Catalog) {
                                parentCategories.Add(parentCategory); 
                            }
                        }
                    }
                }

                var content = productContent;
                while (content.ParentLink != null && content.ParentLink != ReferenceConverter.Service.GetRootLink())
                {
                    var parentCategory = ContentLoader.Service.Get<CatalogContentBase>(content.ParentLink, new LanguageSelector(lang));
                    if (parentCategory != null && parentCategory.ContentType != CatalogContentType.Catalog)
                    {
                        parentCategories.Add(parentCategory); 
                    }
                }
            }
            catch (Exception ex) { }

            return parentCategories; 
        }


        public static IEnumerable<VariationContent> GetVariations(this ProductContent productContent)
        {
            CultureInfo lang = productContent.Language;

            var relationsBySource = LinkRepository.Service.GetRelationsBySource(productContent.ContentLink).OfType<ProductVariation>();
            var productVariants = relationsBySource.Select(x => ContentLoader.Service.Get<VariationContent>(x.Target, new LanguageSelector(lang.Name)));
            return productVariants; 
        }

        public static decimal GetStock(this EntryContentBase productContent) {
            ProductContent product = ContentLoader.Service.Get<ProductContent>(ReferenceConverter.Service.GetContentLink(productContent.Code, CatalogContentType.CatalogEntry));
            
            if (product == null) return 0;
            
            return product.GetStock(); 
            
        }

        public static decimal GetStock(this ProductContent productContent)
        {
            if (productContent == null) return 0;

            var variations = productContent.GetVariations(); 
            if (variations == null) return 0;

            decimal stock = 0; 
            foreach (VariationContent variation in variations)
            {
                stock += GetStock(variation); 
            }

            return stock; 
        }

        public static decimal GetStock(this VariationContent content) {
            if (content == null) return 0;

            var inventoryService = ServiceLocator.Current.GetInstance<IWarehouseInventoryService>();
            var totalInventory = inventoryService.GetTotal(new CatalogKey(AppContext.Current.ApplicationId, content.Code));

            if (totalInventory != null)
                return totalInventory.InStockQuantity - totalInventory.ReservedQuantity;
            return 0; 
        }

        public static string GetDefaultImage(this EntryContentBase productContent, string preset = null, string group=null) {
            var defaultImage = GetImage(productContent, preset, group);
            if (defaultImage != null) return defaultImage;

            var noImage = "/globalassets/system/no-image.png";
            if (preset != null) noImage = noImage + "?preset=" + preset;

            return noImage; 
        }

        public static string GetImage(this EntryContentBase productContent, string preset = null, string groupName = null)
        {
            UrlResolver urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();

            CommerceMedia commerceMedia;
            {
                if (groupName == null)
                {
                    commerceMedia = productContent.CommerceMediaCollection.OrderBy(m => m.SortOrder).FirstOrDefault(m => m.GroupName == null || m.GroupName == "default");

                }
                else
                {
                    commerceMedia = productContent.CommerceMediaCollection.OrderBy(m => m.SortOrder).FirstOrDefault(m => m.GroupName.ToLower() == groupName);
                }
            }

            if (commerceMedia != null)
            {
                var contentReference = commerceMedia.AssetLink;
                var defaultImageUrl = urlResolver.GetUrl(contentReference, null, new VirtualPathArguments() { ContextMode = ContextMode.Default });
                if (preset != null) defaultImageUrl = defaultImageUrl + "?preset=" + preset;
                return defaultImageUrl; 
            }
            return null; 
        }

        public static List<string> AssetsUrl(this EntryContentBase content) {
            var imageUrls = new List<string>();

            var commerceMedias = content.CommerceMediaCollection;
            if (commerceMedias != null)
            {
                foreach (var commerceMedia in commerceMedias) {
                    if (commerceMedia.GroupName != null && commerceMedia.GroupName == "default")
                    {
                        var mediaUrl = UrlResolver.Current.GetUrl(commerceMedia.AssetLink);
                        imageUrls.Add(mediaUrl); 
                    }
                }
            }
            return imageUrls; 
        }
       
    }
}