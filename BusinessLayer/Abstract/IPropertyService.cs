
using EntityLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPropertyService<TProperty> where TProperty : IProperty
    {
        public Task<bool> AddProperty(TProperty property);
        public Task<bool> UpdateProperty(TProperty property);
    }
}
