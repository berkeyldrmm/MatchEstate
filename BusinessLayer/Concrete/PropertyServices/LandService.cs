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
    public class LandService : IPropertyService<Land>
    {
        private readonly ILandRepository _landRepository;

        public LandService(ILandRepository landRepository)
        {
            _landRepository = landRepository;
        }

        public async Task<bool> AddProperty(Land land)
        {
            return await _landRepository.Insert(land);
        }

        public async Task<bool> UpdateProperty(Land property)
        {
            return await _landRepository.Update(property);
        }
    }
}
