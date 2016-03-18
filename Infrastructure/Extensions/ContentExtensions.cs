using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.LinkAnalyzer.Providers;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Infrastructure.Extensions
{
    public static class ContentExtensions
    {
        public static string GetUrl(this VariationContent content, ILinksRepository linkRepository, UrlResolver urlResolver)
        {
            var productLink = content.GetParentProducts(linkRepository).FirstOrDefault(); 
            if (productLink == null || urlResolver.GetUrl(productLink) == null) return string.Empty;

            var urlBuilder = new UrlBuilder(urlResolver.GetUrl(productLink));
            if (content.Code != null) urlBuilder.QueryCollection.Add("variantCode", content.Code);
            return urlBuilder.ToString(); 
        }
    }
}