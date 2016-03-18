using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Infrastructure.Extensions
{
    public static class AssetContainerExtensions
    {
        private static Injected<AssetUrlResolver> _assetUrlResolver;

        public static string GetDefaultAsset<TContentMedia>(this IAssetContainer assetContainer) where TContentMedia : IContentMedia
        {
            var url = _assetUrlResolver.Service.GetAssetUrl<TContentMedia>(assetContainer);
            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri)) {
                return uri.PathAndQuery; 
            }
            return url; 
        }

        public static IList<string> GetAssets<TContentMedia>(this IAssetContainer assetContainer, IContentLoader contentLoader, UrlResolver urlResolver) where TContentMedia : IContentMedia
        {
            var assets = new List<string>();
            if (assetContainer.CommerceMediaCollection != null)
            {
                assets.AddRange(assetContainer.CommerceMediaCollection.Select(media => urlResolver.GetVirtualPath(media.AssetLink).GetUrl()));
            }

            if (!assets.Any())
            {
                assets.Add(string.Empty);
            }

            return assets; 
        }

        private static string GetVirtualPath(string assetKey, UrlResolver urlResolver) {
            var contentReference = PermanentLinkUtility.FindContentReference(new Guid(assetKey));
            return urlResolver.GetVirtualPath(contentReference).GetUrl(); 
        }

        private static bool ValidateCorrectType<TContentMedia>(ContentReference contentLink, IContentLoader contentLoader)
            where TContentMedia : IContentMedia
        {
            if (typeof(TContentMedia) == typeof(IContentMedia))
            {
                return true;
            }

            if (ContentReference.IsNullOrEmpty(contentLink))
            {
                return false;
            }

            TContentMedia content;
            return contentLoader.TryGet(contentLink, out content);
        }
    }
}