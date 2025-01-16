using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly IShopDal _shopDal;
        private readonly ILandDal _landDal;
        private readonly IApartmentDal _apartmentDal;
        private readonly ICommercialUnitDal _commercialUnitDal;
        private readonly IFarmlandDal _farmlandDal;

        public PropertyService(IShopDal shopDal, ILandDal landDal, IApartmentDal apartmentDal, ICommercialUnitDal commercialUnitDal, IFarmlandDal farmlandDal)
        {
            _shopDal = shopDal;
            _landDal = landDal;
            _apartmentDal = apartmentDal;
            _commercialUnitDal = commercialUnitDal;
            _farmlandDal = farmlandDal;
        }

        [HttpPost]
        public async Task<bool> AddProperty(Property property)
        {
            bool result = false;
            if (property as Shop != null)
            {
                Shop dukkan = property as Shop;
                result = await _shopDal.Insert(dukkan);
            }
            if (property as Land != null)
            {
                Land arsa = property as Land;
                result = await _landDal.Insert(arsa);
            }
            if (property as CommercialUnit != null)
            {
                CommercialUnit depo = property as CommercialUnit;
                result = await _commercialUnitDal.Insert(depo);
            }
            if (property as Apartment != null)
            {
                Apartment daire = property as Apartment;
                result = await _apartmentDal.Insert(daire);
            }
            if (property as Farmland != null)
            {
                Farmland tarla = property as Farmland;
                result = await _farmlandDal.Insert(tarla);
            }

            return result;
        }

        public async Task<bool> UpdateProperty(Property property)
        {
            bool result = false;
            if (property as Shop != null)
            {
                Shop dukkan = property as Shop;
                result = await _shopDal.Update(dukkan);
            }
            if (property as Land != null)
            {
                Land arsa = property as Land;
                result = await _landDal.Update(arsa);
            }
            if (property as CommercialUnit != null)
            {
                CommercialUnit depo = property as CommercialUnit;
                result = await _commercialUnitDal.Update(depo);
            }
            if (property as Apartment != null)
            {
                Apartment daire = property as Apartment;
                result = await _apartmentDal.Update(daire);
            }
            if (property as Farmland != null)
            {
                Farmland tarla = property as Farmland;
                result = await _farmlandDal.Update(tarla);
            }

            return result;
        }
    }
}
