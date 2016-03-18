using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPiServer.Commerce.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver 
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container; 
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract) return GetInterfaceService(serviceType);
            else return GetConcreteClass(serviceType); 
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<Object>(); 
        }

        private object GetInterfaceService(Type serviceType) {
            return _container.TryGetInstance(serviceType); 
        }

        private object GetConcreteClass(Type serviceType)
        {
            return _container.GetInstance(serviceType); 
        }
    }
}