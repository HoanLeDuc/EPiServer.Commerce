using EPiServer.Commerce.Models.Pages;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Business
{
    [ServiceConfiguration(typeof(ISiteSettingProvider), Lifecycle=ServiceInstanceScope.Singleton)]
    public class SiteConfiguration : ISiteSettingProvider
    {
        public Models.Blocks.SettingsBlock GetSetting()
        {
            if (GetStartPage() != null) return GetStartPage().Settings;
            return null; 
        }

        public Models.Pages.HomePage GetStartPage()
        {
            if (ContentReference.IsNullOrEmpty(ContentReference.StartPage)) return null; 

            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            return contentLoader.Get<HomePage>(ContentReference.StartPage); 

        }
    }
}