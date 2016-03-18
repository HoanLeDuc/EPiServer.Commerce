using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
    public interface ICookieService
    {
        string GetCookie(string key);
        void SetCookie(string key, string value); 
    }
}
