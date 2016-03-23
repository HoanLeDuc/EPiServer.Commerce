using Mediachase.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
   public  interface IIndexableContent
    {
       FindProduct GetFindProduct(IMarket currentMarket);
       bool ShouldIndex();
       string Name { get; set; }
    }
}
