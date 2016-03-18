using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Commerce.Models.Blocks;
using EPiServer.Commerce.Models.ViewModels;
using EPiServer.ServiceLocation;

namespace EPiServer.Commerce.Controllers
{
    public class CarouselBlockController : BlockController<CarouselBlock>
    {
        private IContentLoader _contentLoader; 

        public override ActionResult Index(CarouselBlock currentBlock)
        {
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>(); 
            if (currentBlock.CarouselContentArea == null)
            {
                return PartialView("Carousel", null); 
            }

            var model = new CarouselViewModel()
            {
                Items = currentBlock.CarouselContentArea.FilteredItems.Select(i => _contentLoader.Get<CarouselItemBlock>(i.ContentLink)).ToList(),
                CurrentBlock = currentBlock
            }; 


            return PartialView("Carousel", model);
        }
    }
}
