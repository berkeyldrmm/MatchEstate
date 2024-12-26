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
    public class LandService : ILandService
    {
        private readonly ILandDal _landRepository;

        public LandService(ILandDal landRepository)
        {
            _landRepository = landRepository;
        }

        public bool DeleteAsync(Land item)
        {
            return _landRepository.Delete(item);
        }


        public Task<IEnumerable<Land>> GetAll()
        {
            return _landRepository.ReadAll();
        }

        public Task<Land> GetOne(string id)
        {
            return _landRepository.Read(id);
        }

        public async Task<bool> Insert(Land item)
        {
            return await _landRepository.Insert(item);
        }

        public Task<bool> Update(Land item)
        {
            return _landRepository.Update(item);
        }
    }
}
