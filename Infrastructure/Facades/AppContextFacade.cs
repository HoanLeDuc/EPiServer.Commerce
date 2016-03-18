using EPiServer.ServiceLocation;
using Mediachase.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Infrastructure.Facades
{
    [ServiceConfiguration(typeof(AppContextFacade), Lifecycle=ServiceInstanceScope.Singleton)]
    public class AppContextFacade
    {
        public virtual Guid ApplicationId
        {
            get {
                return Mediachase.Commerce.Core.AppContext.Current.ApplicationId; 
            }
        }
    }
}