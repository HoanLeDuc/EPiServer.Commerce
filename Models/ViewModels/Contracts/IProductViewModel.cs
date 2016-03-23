using EPiServer.Commerce.Catalog.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Commerce.Models.ViewModels.Contracts
{
    public interface IProductViewModel<out T>:ICatalogViewModel<T> where T:ProductContent 
    {
    }
}
