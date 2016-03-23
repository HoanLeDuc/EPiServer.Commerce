using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    public class PageViewModel<T> : IPageViewModel<T> where T:PageData 
    {
        public PageViewModel(T currentPage)
        {
            CurrentPage = currentPage; 
        }

        public PageViewModel()
        {

        }

        public T CurrentPage { get; set; }

        public Url LogoImage { get; set; }

        public EPiServer.SpecializedProperties.LinkItemCollection TopLeftMenu
        { get; set; }

        public EPiServer.SpecializedProperties.LinkItemCollection TopRightMenu
        {
            get;
            set;
        }

        
        public IEnumerable<PageData> FooterMenu
        {
            get;
            set;
        }

        public ContentArea SocialMediaIcons
        {
            get;
            set;
        }

        public IContent Section
        {
            get;
            set;
        }

        public ContentReference LoginPage
        {
            get;
            set;
        }

        public ContentReference AccountPage
        {
            get;
            set;
        }

        public ContentReference CheckoutPage
        {
            get;
            set;
        }


        public ContentReference SearchPage
        {
            get;
            set;
        }
    }
}