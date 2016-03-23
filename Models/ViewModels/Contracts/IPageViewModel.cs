using EPiServer.Core;
using EPiServer.SpecializedProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels
{
    public interface IPageViewModel<out T> where T: PageData
    {
        T CurrentPage { get; }

        LinkItemCollection TopLeftMenu { get; set; }
        LinkItemCollection TopRightMenu { get; set; }
        Url LogoImage { get; set; }
        IEnumerable<PageData> FooterMenu { get; set; }
        ContentArea SocialMediaIcons { get; set; }
        IContent Section { get; set; }
        ContentReference LoginPage { get; set; }
        ContentReference AccountPage { get; set; }
        ContentReference CheckoutPage { get; set; }
        ContentReference SearchPage { get; set; }
    }
}
