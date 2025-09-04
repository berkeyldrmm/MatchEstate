using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;

namespace BusinessLayer.Concrete.PropertyServices
{
    public class CommercialUnitService : IPropertyService<CommercialUnit>
    {
        private readonly ICommercialUnitRepository _commercialUnitRepository;

        public CommercialUnitService(ICommercialUnitRepository commercialUnitRepository)
        {
            _commercialUnitRepository = commercialUnitRepository;
        }

        public async Task<bool> AddProperty(CommercialUnit commercialUnit)
        {
            return await _commercialUnitRepository.Insert(commercialUnit);
        }

        public async Task<bool> UpdateProperty(CommercialUnit commercialUnit)
        {
            return await _commercialUnitRepository.Update(commercialUnit);
        }
    }
}
