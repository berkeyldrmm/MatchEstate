using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Factory;
using EntityLayer.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Factory
{
    public class PropertyServiceFactory : IPropertyServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public PropertyServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPropertyService<T> GetService<T>() where T : Property
        {
            return _serviceProvider.GetRequiredService<IPropertyService<T>>();
        }
    }
}
