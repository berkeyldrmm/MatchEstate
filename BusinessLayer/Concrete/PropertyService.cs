using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly ILandService _landService;
        private readonly IFarmlandService _farmlandService;
        private readonly IApartmentService _apartmentService;
        private readonly IShopService _shopService;
        private readonly ICommercialUnitService _commercialUnitService;

        public PropertyService(ILandService landService, IFarmlandService farmlandService, IApartmentService apartmentService, IShopService shopService, ICommercialUnitService commercialUnitService)
        {
            _landService = landService;
            _farmlandService = farmlandService;
            _apartmentService = apartmentService;
            _shopService = shopService;
            _commercialUnitService = commercialUnitService;
        }

        [HttpPost]
        public async Task<bool> AddProperty(Property property)
        {
            bool result = false;
            if (property as Shop != null)
            {
                Shop dukkan = property as Shop;
                result = await _shopService.Insert(dukkan);
            }
            if (property as Land != null)
            {
                Land arsa = property as Land;
                result = await _landService.Insert(arsa);
            }
            if (property as CommercialUnit != null)
            {
                CommercialUnit depo = property as CommercialUnit;
                result = await _commercialUnitService.Insert(depo);
            }
            if (property as Apartment != null)
            {
                Apartment daire = property as Apartment;
                result = await _apartmentService.Insert(daire);
            }
            if (property as Farmland != null)
            {
                Farmland tarla = property as Farmland;
                result = await _farmlandService.Insert(tarla);
            }

            return result;
        }
    }
}
