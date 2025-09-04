using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.PropertyServices
{
    public class FarmlandService : IPropertyService<Farmland>
    {
        private readonly IFarmlandRepository _farmlandRepository;

        public FarmlandService(IFarmlandRepository farmlandRepository)
        {
            _farmlandRepository = farmlandRepository;
        }

        public async Task<bool> AddProperty(Farmland farmland)
        {
            return await _farmlandRepository.Insert(farmland);
        }

        public async Task<bool> UpdateProperty(Farmland property)
        {
            return await _farmlandRepository.Update(property);
        }
    }
}
