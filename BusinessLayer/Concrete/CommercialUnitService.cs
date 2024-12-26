using BusinessLayer.Abstract;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommercialUnitService : ICommercialUnitService
    {
        private readonly ICommercialUnitDal _commercialUnitRepository;

        public CommercialUnitService(ICommercialUnitDal commercialUnitRepository)
        {
            _commercialUnitRepository = commercialUnitRepository;
        }

        public bool DeleteAsync(CommercialUnit item)
        {
            return _commercialUnitRepository.Delete(item);
        }


        public Task<IEnumerable<CommercialUnit>> GetAll()
        {
            return _commercialUnitRepository.ReadAll();
        }

        public Task<CommercialUnit> GetOne(string id)
        {
            return _commercialUnitRepository.Read(id);
        }

        public async Task<bool> Insert(CommercialUnit item)
        {
            return await _commercialUnitRepository.Insert(item);
        }

        public Task<bool> Update(CommercialUnit item)
        {
            return _commercialUnitRepository.Update(item);
        }
    }
}
