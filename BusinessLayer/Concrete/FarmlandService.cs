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
    public class FarmlandService : IFarmlandService
    {
        private readonly IFarmlandDal _farmlandRepository;

        public FarmlandService(IFarmlandDal farmlandRepository)
        {
            _farmlandRepository = farmlandRepository;
        }

        public bool DeleteAsync(Farmland item)
        {
            return _farmlandRepository.Delete(item);
        }

        public Task<IEnumerable<Farmland>> GetAll()
        {
            return _farmlandRepository.ReadAll();
        }

        public Task<Farmland> GetOne(string id)
        {
            return _farmlandRepository.Read(id);
        }

        public async Task<bool> Insert(Farmland item)
        {
            return await _farmlandRepository.Insert(item);
        }

        public Task<bool> Update(Farmland item)
        {
            return _farmlandRepository.Update(item);
        }
    }
}
