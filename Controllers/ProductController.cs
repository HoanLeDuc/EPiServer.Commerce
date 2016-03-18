using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServer.Commerce.Models.Catalog;
using EPiServer.Commerce.Models.ViewModels.Contracts;
using EPiServer.Commerce.Extensions;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using System.Globalization;
using EPiServer.Filters;
using EPiServer.Commerce.Models.ViewModels;
using EPiServer.Commerce.Infrastructure.Extensions;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Catalog;
using EPiServer.Commerce.Infrastructure.Facades;
using Mediachase.Commerce;
using System;

namespace EPiServer.Commerce.Controllers
{
    public class ProductController : ContentController<FashionProduct>
    {
        private IContentLoader _contentLoader;
        private IPricingService _pricingService;
        private IRelationRepository _relationRepository;
        private IProductService _productService;
        private ICurrencyService _currencyService;
        private IPromotionService _promotionService;

        private AppContextFacade _appContext;
        private UrlResolver _urlResolver;
        //private CultureInfo _preferredCulture;
        private Mediachase.Commerce.ICurrentMarket _currentMarket;
        private FilterPublished _filterPublished;

        public ProductController(
            IContentLoader contentLoader,
            IPricingService pricingService,
            IRelationRepository relationRepository,
            IProductService productService,
            ICurrencyService currencyServices,
            IPromotionService promotionService,
            UrlResolver urlResolver,
            AppContextFacade appContext,
            //Func<CultureInfo> cultureInfo,
            ICurrentMarket currentMarket,
            FilterPublished filterPublished
            )
        {
            _contentLoader = contentLoader;
            _pricingService = pricingService;
            _relationRepository = relationRepository;
            _productService = productService;
            _currencyService = currencyServices;
            _promotionService = promotionService;
            _urlResolver = urlResolver;
            //_preferredCulture = cultureInfo();
            _currentMarket = currentMarket;
            _filterPublished = filterPublished;
            _appContext = appContext;
        }

        public ActionResult Index(FashionProduct currentPage, string variantCode = "", bool quickView = false)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            IEnumerable<FashionVariant> variations = GetVariations(currentPage);
            if (!variations.Any())
            {
                var productWithoutVariation = new FashionProductViewModel
                {
                    Product = currentPage,
                    Images = currentPage.GetAssets<IContentImage>(_contentLoader, _urlResolver)
                };
                return Request.IsAjaxRequest() ? PartialView("ProductWithoutVariation", productWithoutVariation) : (ActionResult)View("ProductWithoutVariation", productWithoutVariation);
            }

            FashionVariant variation;
            if (!TryGetFashionVariant(variations, variantCode, out variation))
            {
                return new HttpNotFoundResult();
            }

            var market = _currentMarket.GetCurrentMarket();
            var currency = _currencyService.GetCurrentCurrency();

            var defaultPrice = _pricingService.GetCurrentPrice(variation.Code); //_pricingService.GetdefaultPrice(variation, market, currency);
            var discountPrice = _promotionService.GetDiscountPrice(new CatalogKey(_appContext.ApplicationId, variation.Code), market.MarketId, currency);
            var viewModel = new FashionProductViewModel()
            {
                Product = currentPage,
                Variation = variation,
                OriginalPrice = defaultPrice != null ? defaultPrice : new Money(0, currency),
                Price = discountPrice != null ? discountPrice.UnitPrice : new Money(0, currency),
                Colors = variations.Where(x => x.Size != null && x.Size == variation.Size).Select(x => new SelectListItem()
                {
                    Selected = false,
                    Text = x.Color,
                    Value = x.Color
                }).ToList(),
                Sizes = variations.Where(x => x.Color != null && x.Color == variation.Color).Select(x => new SelectListItem()
                {
                    Selected = false,
                    Text = x.Size,
                    Value = x.Size
                }).ToList(),
                Color = variation.Color,
                Size = variation.Size,
                Images = variation.GetAssets<IContentImage>(_contentLoader, _urlResolver)
            };


            return Request.IsAjaxRequest() ? PartialView(viewModel) : (ActionResult)View(viewModel);
        }

        private bool TryGetFashionVariant(IEnumerable<FashionVariant> variations, string variationCode, out FashionVariant variation)
        {
            variation = !string.IsNullOrEmpty(variationCode) ? variations.FirstOrDefault(v => v.Code == variationCode) : variations.FirstOrDefault();

            return variation != null;
        }

        private IEnumerable<FashionVariant> GetVariations(FashionProduct currentPage)
        {
            return _contentLoader.GetItems(currentPage.GetVariants(_relationRepository), CultureInfo.InvariantCulture)
                .Cast<FashionVariant>()
                .Where(v => v.IsAvailableInCurrentMarket(_currentMarket) && !_filterPublished.ShouldFilter(v));
        }


    }
}