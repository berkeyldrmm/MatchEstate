using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Factory
{
    public interface IPropertyServiceFactory
    {
        IPropertyService<T> GetService<T>() where T : Property;
    }
}
