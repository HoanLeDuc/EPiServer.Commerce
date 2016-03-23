using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Models.Catalog.Base;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Infrastructure.Extensions;
using EPiServer.Commerce.Models.FindModels; 

namespace EPiServer.Commerce.Models.Catalog
{
        [CatalogContentType(DisplayName = "Fashion Product",
        GUID = "18EA436F-3B3B-464E-A526-564E9AC454C7",
        MetaClassName = "Fashion_Product_Class")]
    public class FashionProductContent : ProductBase, IIndexableContent, IProductListViewModelInitializer
    {
        [CultureSpecific]
        [Display(Name = "Facet Color",
            Order = 10)]
        [Editable(true)]
        [SelectMany(SelectionFactoryType = typeof(ColorSelectionFactory))]
        public virtual string FacetColor { get; set; }

        [Display(Name = "Size Type",
           Order = 30)]
        public virtual string SizeType { get; set; }

        [Display(Name = "Size Unit", Order = 36)]
        public virtual string SizeUnit { get; set; }

        [Display(Name = "Fit",
           Order = 40)]
        [Editable(false)]
        public virtual string Fit { get; set; }

        [CultureSpecific]
        [Display(Name = "New item",
            Order = 55,
            Description = "Text describing new products (based on publish start date)")]
        public virtual string NewItemText { get; set; }

        [CultureSpecific]
        [Display(Name = "Size and fit",
            Order = 60)]
        public virtual XhtmlString SizeAndFit { get; set; }

        [CultureSpecific]
        [Display(Name = "Details and maintenance",
            Order = 70)]
        public virtual XhtmlString DetailsAndMaintenance { get; set; }


        [Display(Name = "Size guide", Order = 80)]
        // [AllowedTypes(new[] { typeof(ArticlePage) })]
        /// TODO: Convert to ContentReference
        public virtual Url SizeGuide { get; set; }

        public ViewModels.ProductListViewModel Populate(Mediachase.Commerce.IMarket currentMarket)
        {
            throw new NotImplementedException();
        }

        public FindProduct GetFindProduct(IMarket currentMarket)
        {
            var findProduct = new FindProduct(this, currentMarket.DefaultLanguage.Name);

            findProduct.Variants = GetFashionVariations(this.GetVariations().ToList()); 


            return findProduct; 
        }


        public List<VariationContent> GetVariations(ProductContent product) {
            var linksRepository = ServiceLocator.Current.GetInstance<ILinksRepository>();
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

            var variants = linksRepository.GetRelationsBySource(product.ContentLink).OfType<ProductVariation>();
            List<VariationContent> productVariations = new List<VariationContent>(); 
            if (variants != null) {
                productVariations.AddRange(variants.Select(x => contentLoader.Get<VariationContent>(x.Target, product.Language))); 
            }
            return productVariations; 
        }

        public List<FashionVariationContent> GetFashionVariations(List<VariationContent> variations)
        {
            var fashionVariations = new List<FashionVariationContent>();

            foreach (VariationContent variation in variations)
            {
                if (variation is FashionItemContent) {
                    var fashionItemContent = variation as FashionItemContent;
                    var fashionVariationContent = new FashionVariationContent()
                    {
                        Id = fashionItemContent.ContentLink.ID,
                        Code = fashionItemContent.Code,
                        Color = fashionItemContent.Color,
                        Size = fashionItemContent.Facet_Size,
                        Prices = variation.GetPricesWithMarket(), 
                        Stock = fashionItemContent.GetStock()
                    };

                    fashionVariations.Add(fashionVariationContent); 
                }
            }
            return fashionVariations; 
        }

        public bool ShouldIndex()
        {
            throw new NotImplementedException();
        }
    }
}