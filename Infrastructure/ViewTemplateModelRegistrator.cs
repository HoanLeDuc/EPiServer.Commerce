using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Infrastructure
{
    [ServiceConfiguration(typeof(IViewTemplateModelRegistrator))]
    public class ViewTemplateModelRegistrator : IViewTemplateModelRegistrator 
    {

        public void Register(TemplateModelCollection viewTemplateModelRegistrator)
        {
            viewTemplateModelRegistrator.Add(typeof(PageData), new TemplateModel { 
                Name="PartialPage", 
                Inherit = true,
                AvailableWithoutTag=true,
                TemplateTypeCategory=Framework.Web.TemplateTypeCategories.MvcPartialView,
                Path="~/Views/Shared/_Page.cshtml"
            });
        }
    }
}