using EPiServer.Commerce.Models.Blocks;
using EPiServer.Commerce.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Business
{
    public interface ISiteSettingProvider
    {
        SettingsBlock GetSetting();
        HomePage GetStartPage();
    }
}
