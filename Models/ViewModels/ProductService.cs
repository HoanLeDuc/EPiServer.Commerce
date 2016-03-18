using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Infrastructure.Facades;
using EPiServer.Commerce.Models.Catalog;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.Core;
using EPiServer.LinkAnalyzer.Providers;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Commerce.Infrastructure.Extensions;
using EPiServer.ServiceLocation;
using System.Globalization; 

namespace EPiServer.Commerce.Models.ViewModels
{
    [ServiceConfiguration(typeof(IProductService), Lifecycle=ServiceInstanceScope.Singleton)]
    public class ProductService : IProductService
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;
        private readonly ILinksRepository _linkRepository;
        private readonly IRelationRepository _relationRepository;
        private readonly ICurrentMarket _currentMarket;
        private readonly ICurrencyService _currencyService;
        private readonly IPricingService _pricingService;
        private readonly IPromotionService _promotionService;
        private readonly AppContextFacade _appContext;

        public ProductService(IContentLoader contentLoader, UrlResolver urlResolver, ILinksRepository linkRepository, IRelationRepository relationRepository, ICurrentMarket currentMarket, ICurrencyService currencyService, IPricingService pricingService, IPromotionService promotionService, AppContextFacade appContext)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _linkRepository = linkRepository;
            _relationRepository = relationRepository;
            _currentMarket = currentMarket;
            _currencyService = currencyService;
            _pricingService = pricingService;
            _promotionService = promotionService;
            _appContext = appContext;
        }

        public IEnumerable<Catalog.ProductViewModel> GetVariationsAndPricesForProducts(IEnumerable<ProductContent> products)
        {
            throw new NotImplementedException();
        }

        public Catalog.ProductViewModel GetProductViewModel(ProductContent product)
        {
            var variants = _contentLoader.GetItems(product.GetVariants(), CultureInfo.InvariantCulture).Cast<VariationContent>();

            return CreateProductViewModel(product, variants.FirstOrDefault()); 
        }

        public ProductViewModel GetProductViewModel(VariationContent varation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FashionVariant> GetVariations(FashionProduct currentContent)
        {
            return _contentLoader.GetItems(currentContent.GetVariants(_relationRepository), CultureInfo.InvariantCulture).Cast<FashionVariant>().Where(x => x.IsAvailableInCurrentMarket()); 
        }

        private ProductViewModel CreateProductViewModel(ProductContent product, VariationContent variation)
        {
            if (variation == null) return null;

            ContentReference contentReference;
            if (product != null)
                contentReference = product.ContentLink;
            else
            {
                contentReference = product.GetParentProducts(_relationRepository).FirstOrDefault();
                if (ContentReference.IsNullOrEmpty(contentReference)) return null; 
            }

            var market = _currentMarket.GetCurrentMarket();
            var currency = _currencyService.GetCurrentCurrency();
            var originalPrice = _pricingService.GetCurrentPrice(variation.Code);
            var discountPrice = _promotionService.GetDiscountPrice(new CatalogKey(_appContext.ApplicationId, variation.Code), market.MarketId, currency);

            var productImage = product.GetAssets<IContentImage>(_contentLoader, _urlResolver).FirstOrDefault() ?? string.Empty;

            var image = !string.IsNullOrEmpty(productImage) ? productImage : variation.GetAssets<IContentImage>(_contentLoader, _urlResolver).FirstOrDefault() ?? "";
            var brand = product is FashionProduct ? ((FashionProduct)product).Brand : string.Empty;

            return new ProductViewModel { 
                DisplayName = product != null ? product.DisplayName : variation.DisplayName, 
                Brand = brand,
                ImageUrl = image, 
                Url = variation.GetUrl(_linkRepository, _urlResolver), 
                PlacedPrice = originalPrice ,
                ExtendedPrice = discountPrice != null ? discountPrice.UnitPrice : new Money(0, currency)
            };
        }


    }
}