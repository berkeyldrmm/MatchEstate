﻿
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPropertyService
    {
        public Task<bool> AddProperty(Property property);
        public Task<bool> UpdateProperty(Property property);
    }
}
